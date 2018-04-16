using GPdotNet.BasicTypes;
using GPdotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GPdotNet.Core
{
    public static class NodeDecoding
    {

        public static string Decode(Node root, IParameters param, EncodeType expOption)
        {
            var argColl = new Stack<string>();

            //we need to enumerate in reverse order
            foreach (var n in root.TraverseDFT().Reverse())
            {
                if (Globals.IsFunction(n.Value))
                {
                    string function = "";
                    Function f = Globals.GetFunction(n.Value);
                    var arity = Globals.FunctionArity(n.Value);
                    //in case of root node arity is stored in variable
                    if (arity == -1 && param != null && param.RootFunctionNode != null)
                        arity = param.RootFunctionNode.Arity;

                    if (expOption == EncodeType.Mathematica)
                        function = f.MathematicaDefinition;

                    else if (expOption == EncodeType.Excel)
                    {
                        var ef = f.ExcelDefinition.Split('|');
                        if (param.IsProtectedOperation && ef.Length > 1)
                            function = ef[1];
                        else
                        function = ef[0];
                    }

                    else if (expOption == EncodeType.RLanguage)
                        function = f.RDefinition;
                    else//default encode
                        function = f.Definition;
                    string tempStr = "";
                    //extract variable
                    for (int i = 1; i <= arity; i++)
                    {
                        if (arity > 12)
                            throw new Exception("Maximum number of classes exceeded for Softmax function!");
                        //Softmax function
                        if(n.Value == 2051)
                        {
                            if (i == 1)
                                tempStr = "";

                            tempStr += argColl.Pop();

                            if (i == arity)//last argument
                            {
                                if (expOption == EncodeType.Mathematica)
                                    function = $"Softmax{arity}({tempStr})";
                                else if (expOption == EncodeType.Excel)
                                    function = $"Softmax{arity}({tempStr})";
                                else if (expOption == EncodeType.RLanguage)
                                    function = $"Softmax(c({tempStr}))";
                                else//default
                                    function = $"Softmax({tempStr})";
                            }
                            else
                                tempStr += ",";
                        }
                        //Scaled Sigmoid function
                        else if(n.Value == 2050 || n.Value == 2049)
                        {
                            string oldStr = "v" + (i).ToString();
                            string newStr = argColl.Pop();
                            function = function.Replace(oldStr, newStr);
                            //replace parameter
                            function = function.Replace("p1",param.RootFunctionNode.Parameter.ToString());
                        }
                        else
                        {
                            string oldStr = "v" + (i).ToString();
                            string newStr = argColl.Pop();
                            function = function.Replace(oldStr, newStr);
                        }
                        
                    }

                    //
                    argColl.Push("(" + function + ")");
                }
                else
                {
                    //extract data value from terminal id
                    string varName = Globals.TerminalNameFromId(n.Value, param, false);

                    argColl.Push(varName);
                }
            }
            // return the only value from stack
            return argColl.Pop();
        }
    }
}
