//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See license section of  https://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Linq;
using System.Collections.Generic;
using GPdotNet.BasicTypes;
using GPdotNet.Interfaces;

namespace GPdotNet.Core
{
    public static class Globals
    {
        public static ThreadSafeRandom radn = new ThreadSafeRandom();
        private static Dictionary<int, Function> functions;
        public static Dictionary<int, Function> functionSet
        {
            get
            {
                if (functions == null)
                    functions = loadFunctions();
                return functions;
            }
        }
            

        //node values are split in two range. 1000-1999 - terminal index
        //                                    2000-2999 - function index
        public static bool IsFunction(int id) => id > 1999 ? true : false;
        
        internal static int FunctionIndexFromNode(int id)
        {
            //first check for special functions Sigmoid, softmax, ... 
            //which are not part of function set
            if (id == 2048)
                return 48;
            else if (id == 2049)
                return 49;
            else if (IsFunction(id))
                return id - 2000;
            else
              throw new Exception("Wrong Function Id");
        }

        public static Function GetFunction(int funId)
        {
            if (functionSet.ContainsKey(funId))
                return functionSet[funId];
            else
                throw new Exception("Function is unknown!");
        }
        public static int FunctionArity(int funId)
        {
            if (functionSet.ContainsKey(funId))
                return functionSet[funId].Arity;
            else
                return 0;
        }

        public static string FunctionFromId(int funId, IParameters param = null, bool subscript = false)
        {
            if (functionSet.ContainsKey(funId))
                return functionSet[funId].Name;
            else
                return TerminalNameFromId(funId, param, subscript);
                //return "T" + (funId % 1000).ToString();
        }

        public static string TerminalNameFromId(int id, IParameters param = null, bool subscript = false)
        {
            if(param == null)
            {
                //add extra space to the end in order to differentiate 
                //index of variable and number in formula 
                return "X" + (id % 1000).ToString() + " ";
            }
            else
            {
                string varName = "";

                //extract data value from terminal id
                var terInd = (id % 1000);
                if (terInd < param.FeatureCount)
                {
                    terInd++;//zero index in  to "one" index
                    var strInd = subscript? makeIndex(terInd) : terInd.ToString();
                    varName = "x" + strInd + " "; //add extra space to the end in order to differentiate 
                }
                else
                {
                    var constInd = terInd - param.FeatureCount;

                    constInd++;//zero index in to "one" index
                    var strInd = subscript ? makeIndex(constInd) : constInd.ToString();
                    varName = $"r" + strInd + " ";//add extra space to the end in order to differentiate 
                  
                }

                //return terminal name
                return varName;
            }        
        }

        private static string makeIndex(int index)
        {
            // holder temporarily holds the last digit of the number   
            int holder = 0;
            string strIndex = "";
            while (index > 0)
            {
                holder = index % 10;
                index = index / 10;
                strIndex = makeSubscript(holder) + strIndex;
            }
            return strIndex;
        }

        private static string makeSubscript(int subscript)
        {
            if(subscript==0)
                return "\x2080";
            else if(subscript == 1)
                return "\x2081";
            else if (subscript == 2)
                return "\x2082";
            else if (subscript == 3)
                return "\x2083";
            else if (subscript == 4)
                return "\x2084";
            else if (subscript == 5)
                return "\x2085";
            else if (subscript == 6)
                return "\x2086";
            else if (subscript == 7)
                return "\x2087";
            else if (subscript == 8)
                return "\x2088";
            else if (subscript == 9)
                return "\x2089";
            else
                return "\x2080";
        }

        public static int TerminalIndexFromNode(int id)
        {
            if (IsFunction(id))
                throw new Exception("Wrong Terminal Id");
            else
                return id - 1000;
        }
        private static Dictionary<int, Function> loadFunctions()
        {
            var retVal = new Dictionary<int, Function>();
            retVal.Add(2000, new Function() {Id=2000, Name = "+",       Arity = 2 , Weight = 1 , Selected = true,  Definition = "v1+v2", MathematicaDefinition = "v1+v2", ExcelDefinition = "v1+v2", RDefinition = "v1+v2" });
            retVal.Add(2001, new Function() {Id=2001, Name = "-",       Arity = 2 , Weight = 1 , Selected = true,  Definition = "v1-v2", MathematicaDefinition = "v1-v2", ExcelDefinition = "v1-v2", RDefinition = "v1-v2" });
            retVal.Add(2002, new Function() {Id=2002, Name = "*",       Arity = 2,  Weight = 1 , Selected = true,  Definition = "v1*v2", MathematicaDefinition = "v1*v2", ExcelDefinition = "v1*v2", RDefinition = "v1*v2" });
            retVal.Add(2003, new Function() {Id=2003, Name = "/",       Arity = 2,  Weight = 1 , Selected = true,  Definition = "v1/v2", MathematicaDefinition = "v1/v2", ExcelDefinition = "v1/v2|IF(v2=0;0;v1/v2)", RDefinition = "v1/v2" });
            retVal.Add(2004, new Function() {Id=2004, Name = "add3",    Arity = 3,  Weight = 1 , Selected = false, Definition = "v1+v2+v3", MathematicaDefinition = "v1+v2+v3", ExcelDefinition = "v1+v2+v3", RDefinition = "v1+v2+v3" });
            retVal.Add(2005, new Function() {Id=2005, Name = "sub3",    Arity = 3,  Weight = 1 , Selected = false, Definition = "v1-v2-v3", MathematicaDefinition = "v1-v2-v3", ExcelDefinition = "v1-v2-v3", RDefinition = "v1-v2-v3" });
            retVal.Add(2006, new Function() {Id=2006, Name = "mul3",    Arity = 3,  Weight = 1 , Selected = false, Definition = "v1*v2*v3", MathematicaDefinition = "v1*v2*v3", ExcelDefinition = "v1*v2*v3", RDefinition = "v1*v2*v3" });
            retVal.Add(2007, new Function() {Id=2007, Name = "div3",    Arity = 3,  Weight = 1 , Selected = false, Definition = "v1/v2/v3", MathematicaDefinition = "v1/v2/v3", ExcelDefinition = "(v1/v2/v3)|IF(ISNUMBER(v1/v2/v3);v1/v2/v3;0)", RDefinition = "v1+v2" });
            retVal.Add(2008, new Function() {Id=2008, Name = "add4",    Arity = 4,  Weight = 1 , Selected = false, Definition = "v1+v2+v3+v4", MathematicaDefinition = "v1+v2+v3+v4", ExcelDefinition = "v1+v2+v3+v4", RDefinition = "v1+v2+v3+v4" });
            retVal.Add(2009, new Function() {Id=2009, Name = "sub4",    Arity = 4,  Weight = 1 , Selected = false, Definition = "v1-v2-v3-v4", MathematicaDefinition = "v1-v2-v3-v4", ExcelDefinition = "v1-v2-v3-v4", RDefinition = "v1-v2-v3-v4" });
            retVal.Add(2010, new Function() {Id=2010, Name = "mul4",    Arity = 4,  Weight = 1 , Selected = false, Definition = "v1*v2*v3*v4", MathematicaDefinition = "v1*v2*v3*v4", ExcelDefinition = "v1*v2*v3*v4", RDefinition = "v1*v2*v3*v4" });
            retVal.Add(2011, new Function() {Id=2011, Name = "div4",    Arity = 4,  Weight = 1 , Selected = false, Definition = "v1/v2/v3/v4", MathematicaDefinition = "v1/v2/v3/v4", ExcelDefinition = "(v1/v2/v3/v4)|IF(ISNUMBER(v1/v2/v3/v4);v1/v2/v3/v4;0)", RDefinition = "x1/x2/x3/x4" });

            retVal.Add(2012, new Function() {Id=2012, Name = "x^2",     Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^2", MathematicaDefinition = "v1^2", ExcelDefinition = "v1^2", RDefinition = "v1^2" });
            retVal.Add(2013, new Function() {Id=2013, Name = "x^3",     Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^3", MathematicaDefinition = "v1^3", ExcelDefinition = "v1^3", RDefinition = "v1^3" });
            retVal.Add(2014, new Function() {Id=2014, Name = "x^4",     Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^4", MathematicaDefinition = "v1^4", ExcelDefinition = "v1^4", RDefinition = "v1^4" });
            retVal.Add(2015, new Function() {Id=2015, Name = "x^5",     Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^5", MathematicaDefinition = "v1^5", ExcelDefinition = "v1^5", RDefinition = "x1^5" });
            retVal.Add(2016, new Function() {Id=2016, Name = "x^1/3",   Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^(1/3)", MathematicaDefinition = "v1^(1/3)", ExcelDefinition = "v1^(1/3)", RDefinition = "v1^(1/3)" });
            retVal.Add(2017, new Function() {Id=2017, Name = "x^1/4",   Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^(1/4)", MathematicaDefinition = "v1^(1/4)", ExcelDefinition = "v1^(1/4)", RDefinition = "v1^(1/4)" });
            retVal.Add(2018, new Function() {Id=2018, Name = "x^1/5",   Arity = 1,  Weight = 1 , Selected = false, Definition = "v1^(1/5)", MathematicaDefinition = "v1^(1/5)", ExcelDefinition = "v1^(1/5)", RDefinition = "v1^(1/5)" });
            retVal.Add(2019, new Function() {Id=2019, Name = "1/x",     Arity = 1,  Weight = 1 , Selected = false, Definition = "1/v1", MathematicaDefinition = "1/v1", ExcelDefinition = "(1/v1)|IF(v1=0;0;1/v1)", RDefinition = "1/v1" });

            retVal.Add(2020, new Function() {Id=2020, Name = "abs",     Arity = 1,  Weight = 1,  Selected = false, Definition = "abs", MathematicaDefinition = "Abs[v1]", ExcelDefinition = "abs(v1)", RDefinition = "abs(v1)" });
            retVal.Add(2021, new Function() {Id=2021, Name = "floor",   Arity = 1,  Weight = 1 , Selected = false, Definition = "floor", MathematicaDefinition = "Floor[v1]", ExcelDefinition = "floor(v1)", RDefinition = "floor(v1)" });
            retVal.Add(2022, new Function() {Id=2022, Name = "ceiling", Arity = 1,  Weight = 1 , Selected = false, Definition = "ceiling", MathematicaDefinition = "Ceiling[v1]", ExcelDefinition = "ceiling(v1)", RDefinition = "ceiling(v1)" });
            retVal.Add(2023, new Function() {Id=2023, Name = "truncate",Arity = 1,  Weight = 1 , Selected = false, Definition = "truncate", MathematicaDefinition = "Trunc[v1]", ExcelDefinition = "trunc(v1)", RDefinition = "trunc(v1)" });
            retVal.Add(2024, new Function() {Id=2024, Name = "round",   Arity = 1,  Weight = 1 , Selected = false, Definition = "round(v1;p1)", MathematicaDefinition = "Round[v1,p1]", ExcelDefinition = "round(x1;p1)", RDefinition = "round(x1,p1)" });
            retVal.Add(2025, new Function() {Id=2025, Name = "sin",     Arity = 1,  Weight = 1 , Selected = false, Definition = "sin(v1)", MathematicaDefinition = "Sin[v1]", ExcelDefinition = "sin(v1)", RDefinition = "sin(v1)" });
            retVal.Add(2026, new Function() {Id=2026, Name = "cos",     Arity = 1,  Weight = 1 , Selected = false, Definition = "cos(v1)", MathematicaDefinition = "Cos[v1]", ExcelDefinition = "cos(v1)", RDefinition = "cos(v1)" });
            retVal.Add(2027, new Function() {Id=2027, Name = "tan",     Arity = 1,  Weight = 1 , Selected = false, Definition = "tan(v1)", MathematicaDefinition = "Tan[v1]", ExcelDefinition = "tan(v1)", RDefinition = "tan(v1)" });
            retVal.Add(2028, new Function() {Id=2028, Name = "asin",    Arity = 1,  Weight = 1 , Selected = false, Definition = "asin(v1)", MathematicaDefinition = "ASin[v1]", ExcelDefinition = "ASIN(v1)|IF(ISNUMBER(ASIN(v1));ASIN(v1);0)", RDefinition = "asin(v1)" });
            retVal.Add(2029, new Function() {Id=2029, Name = "acos",    Arity = 1,  Weight = 1 , Selected = false, Definition = "acos(v1)", MathematicaDefinition = "ACos[v1]", ExcelDefinition = "ACOS(v1)|IF(ISNUMBER(ACOS(v1));ACOS(v1);0)", RDefinition = "acos(v1)" });
            retVal.Add(2030, new Function() {Id=2030, Name = "atan",    Arity = 1,  Weight = 1 , Selected = false, Definition = "atan(v1)", MathematicaDefinition = "ATan[v1]", ExcelDefinition = "ATAN(v1)|IF(ISNUMBER(ATAN(v1));ATAN(v1);0)", RDefinition = "atan(v1)" });

            retVal.Add(2031, new Function() {Id=2031, Name = "sinh",    Arity = 1,  Weight = 1 , Selected = false, Definition = "sinh(v1)", MathematicaDefinition = "Sinh[v1]", ExcelDefinition = "sinh(v1)", RDefinition = "sinh(v1)" });
            retVal.Add(2032, new Function() {Id=2032, Name = "cosh",    Arity = 1,  Weight = 1 , Selected = false, Definition = "cosh(v1)", MathematicaDefinition = "Cosh[v1]", ExcelDefinition = "cosh(v1)", RDefinition = "cosh(v1)" });
            retVal.Add(2033, new Function() {Id=2033, Name = "tanh",    Arity = 1,  Weight = 1 , Selected = false, Definition = "tanh(v1)", MathematicaDefinition = "Tanh[v1]", ExcelDefinition = "tanh(v1)", RDefinition = "tanh(v1)" });
            retVal.Add(2034, new Function() {Id=2034, Name = "sqrt",    Arity = 1,  Weight = 1 , Selected = false, Definition = "sqrt(v1)", MathematicaDefinition = "Sqrt[v1]", ExcelDefinition = "SQRT(v1)|IF(ISNUMBER(SQRT(v1));SQRT(v1);0)", RDefinition = "sqrt(v1)" });
            retVal.Add(2035, new Function() {Id=2035, Name = "exp",     Arity = 1,  Weight = 1 , Selected = false, Definition = "exp(v1)", MathematicaDefinition = "Exp[v1]", ExcelDefinition = "exp(v1)", RDefinition = "exp(v1)" });
            retVal.Add(2036, new Function() {Id=2036, Name = "log10",   Arity = 1,  Weight = 1 , Selected = false, Definition = "Log10(v1)", MathematicaDefinition = "Log[v1,10]", ExcelDefinition = "LOG10(v1)|IF(ISNUMBER(LOG10(v1));LOG10(v1);0)", RDefinition = "log10(v1)" });
            retVal.Add(2037, new Function() {Id=2037, Name = "log",     Arity = 1,  Weight = 1 , Selected = false, Definition = "Log(v1)", MathematicaDefinition = "Log[v1]", ExcelDefinition = "LN(v1)|IF(ISNUMBER(LN(v1));LN(v1);0)", RDefinition = "log(v1)" });

            retVal.Add(2038, new Function() {Id=2038, Name = "p(x,2)",  Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^2+v1*v2+v2^2", MathematicaDefinition = "v1^2+v1*v2+v2^2", ExcelDefinition = "v1^2+v1*v2+v2^2", RDefinition = "v1^2+v1*v2+v2^2" });
            retVal.Add(2039, new Function() {Id=2039, Name = "p(x,3)",  Arity = 3,  Weight = 1 , Selected = false, Definition = "v1^3+v2^3+v3^3+v1*v2*v3+v1*v2+v1*v3+v2*v3", MathematicaDefinition = "v1^3+v2^3+v3^3+v1*v2*v3+v1*v2+v1*v3+v2*v3", ExcelDefinition = "v1^3+v2^3+v3^3+v1*v2*v3+v1*v2+v1*v3+v2*v3", RDefinition = "v1^3+v2^3+v3^3+v1*v2*v3+v1*v2+v1*v3+v2*v3" });
            retVal.Add(2040, new Function() {Id=2040, Name = "x^2y",    Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^2*v2", MathematicaDefinition = "v1^2*v2", ExcelDefinition = "v1^2*v2", RDefinition = "v1^2*v2" });
            retVal.Add(2041, new Function() {Id=2041, Name = "x^2y^2",  Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^2*v2^2", MathematicaDefinition = "v1^2*v2^2", ExcelDefinition = "v1^2*v2^2", RDefinition = "v1^2*v2^2" });
            retVal.Add(2042, new Function() {Id=2042, Name = "x^3y",    Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^3*v2", MathematicaDefinition = "v1^3*v2", ExcelDefinition = "v1^3*v2", RDefinition = "v1^3*v2" });
            retVal.Add(2043, new Function() {Id=2043, Name = "x^3y^2",  Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^3*v2^2", MathematicaDefinition = "v1^3*v2^2", ExcelDefinition = "v1^3*v2^2", RDefinition = "v1^3*v2^2" });
            retVal.Add(2044, new Function() {Id=2044, Name = "x^3y^3",  Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^3*v2^3", MathematicaDefinition = "v1^3*v2^3", ExcelDefinition = "v1^3*v2^3", RDefinition = "v1^3*v2^3" });
            retVal.Add(2045, new Function() {Id=2045, Name = "x^2y^3",  Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^2*v2^3", MathematicaDefinition = "x1^2*x2^3", ExcelDefinition = "x1^2*x2^3", RDefinition = "x1^2*x2^3" });
            retVal.Add(2046, new Function() {Id=2046, Name = "xy^3",    Arity = 2,  Weight = 1 , Selected = false, Definition = "v1*x2v3", MathematicaDefinition = "v1*x2v3", ExcelDefinition = "v1*x2v3", RDefinition = "v1*x2v3" });
            retVal.Add(2047, new Function() {Id=2047, Name = "x^4y^4",  Arity = 2,  Weight = 1 , Selected = false, Definition = "v1^4*v2^4", MathematicaDefinition = "v1^4*v2^4", ExcelDefinition = "v1^4*v2^4", RDefinition = "v1^4*v2^4" });


            //special functions for root nodes
            retVal.Add(2048, new Function() {Id=2048, Name ="sigmoid", HasParameter=true, Parameter=0.5, Arity = 1, Weight = 0 , Selected = false, Definition = "Sigmoid(v1)", MathematicaDefinition = "1/(1+Exp[-v1])", ExcelDefinition = "Sigmoid(v1)", RDefinition = "1/(1+ exp(-v1))" });
            retVal.Add(2049, new Function() {Id=2049, Name ="step", HasParameter = true, Parameter = 0, Arity = 1,  Weight = 0, Selected = false, Definition = "Step(v1)", MathematicaDefinition = "If[v1<0,0,1]", ExcelDefinition = "Gestep(v1,p1)", RDefinition = "if(v1<0) 0 1 " });
            retVal.Add(2050, new Function() {Id=2050, Name ="ssigmoid",Arity = -1,/*custom defined arity*/ Weight = 0, Selected = false, Definition = "SSigmoid(v1,p1)", MathematicaDefinition = "p1/(1 + Exp[-v1])", ExcelDefinition = "SSigmoid(v1,p1)", RDefinition = "p1/(1+ exp(-v1))" });
            retVal.Add(2051, new Function() {Id=2051, Name ="softmax",Arity = -1,/*custom defined arity*/Weight = 0, Selected = false, Definition = "Softmax({v1})", MathematicaDefinition = "Softmax({v1})", ExcelDefinition = "Softmax({v1})", RDefinition = "Softmax({v1})" });

            //retVal.Add(2050, new Function() { Name = "function name" });
            return retVal;
        }

        public static double[] GenerateConstants(float from, float to, int number)
        {
            if (number == 0)
                return null;

            if (from >= to)
            {
                // MessageBox.Show("'From' has to be less than 'To' variable.");
                return null;
            }

            var con = new double[number];

            for (int i = 0; i < number; i++)
                con[i] = System.Math.Round((Globals.radn.Next((int)from, (int)to) + Globals.radn.NextDouble()), 5);


            return con;
        }

    }
}
