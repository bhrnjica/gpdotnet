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
using System.Text;
using GPdotNet.Interfaces;
using GPdotNet.BasicTypes;
namespace GPdotNet.Core
{
    public enum Generation
    {
        strict,
        random,
    };
    /// <summary>
    /// Node class represent tree structure used as representation of Genetic programming individuals (chromosome)
    /// 
    /// </summary>
    public class Node : INode
    {
        //additional member for store additional info to Node.
        public bool marked;

        //main member
        private int val;

        #region Properties

        //node children
        public Node[] Children  { get;  set; }

        //Node level
        public short Level { get;  set; }

        //public version of value variable
        public int Value { get => val; set => val = value; }

        #endregion

        #region Public Methods        
        /// <summary>
        /// Make deep copy of the Node. Used Stack collection for traversing depth first
        /// </summary>
        /// <returns></returns>
        public Node Clone()
        {
            var rootclone = new Node();

            //Collection holds tree nodes
            var dataTree = new Stack<Node>();
            var cloneTree = new Stack<Node>();

            //current node
            Node node = null;
            Node clone = null;

            //Add tail recursion
            dataTree.Push(this);
            cloneTree.Push(rootclone);

            //
            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();
                clone = cloneTree.Pop();

                //assign node properties
                setNodeValues(node, clone);

                //iterate children
                if (node.Children != null)
                {
                    clone.Children = new Node[node.Children.Length];

                    for (int i = 0; i < node.Children.Length; i++)
                    {
                        clone.Children[i] = new Node();
                        dataTree.Push(node.Children[i]);
                        cloneTree.Push(clone.Children[i]);
                    }
                }
            }

            return rootclone;

            void setNodeValues(Node locnode, Node locclone)
            {
                locclone.Level  = locnode.Level;
                locclone.val  = locnode.val;
                locclone.marked = locnode.marked;

            }
        }

        /// <summary>
        /// Generates random tree structure with maximum level from available function and terminal set
        /// </summary>
        /// <param name="maxLevels">maximum level of tree</param>
        /// <param name="funSet">available function set</param>
        /// <param name="terSet">available terminal set</param>
        public void Generate(int maxLevels, Generation genType, Function[] funSet, int[] terSet, Function rootFunction = null)
        {
            //queue collection for iteration through the tree structure
            Queue<Node> dataTree = new Queue<Node>();
            Node node = null;

            if(rootFunction!=null)
            {
                //initialize root with predefined function
                val = rootFunction.Id;
                var arity = Globals.FunctionArity(val);
                if (arity == -1)
                    arity = rootFunction.Arity;

                initializeNodeOffspring(arity);

                //add generated offspring in to queue
                for (int i = 0; i < arity; i++)
                    dataTree.Enqueue(Children[i]);
            }
            else//Add first node to queue
                dataTree.Enqueue(this);

            while (dataTree.Count > 0)
            {
                //retrieve first node from the queue
                node = dataTree.Dequeue();

                int numOfspring = node.initializeNode(maxLevels, genType, funSet, terSet);


                if (numOfspring > 0)
                {
                    //generate offspring
                    node.initializeNodeOffspring(numOfspring);

                    //add generated offspring in to queue
                    for (int i = 0; i < numOfspring; i++)
                        dataTree.Enqueue(node.Children[i]);
                }
                else
                    node.Children = null;

            }
        }

        /// <summary>
        /// We need to have trees with proper levels, so every subtree which has level greater than maxOperationLevel should be trimmed.
        /// First  change functionNode to Terminal at maximum level, then remove all node which are grater than maxOparationLevel
        /// </summary>
        /// <param name="maxLevel"></param>
        internal void Trim(int maxLevels, int[] terSet)
        {
            //
            var dataTree = new Stack<Node>();
            //start with 0 level
            Level = 0;
            dataTree.Push(this);

            //
            Node node = null;
            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                //when the node level is equal to maximum value make terminal
                if (node.Level >= maxLevels)
                {
                    if (node.Children != null)
                        node.Children = null;

                    node.Value = node.GenerateTerminal(terSet);
                }

                if (node.Children != null)
                {
                    for (int i = node.Children.Length - 1; i >= 0; i--)
                    {
                        short l = (short)(node.Level + 1);
                        node.Children[i].Level = l;
                        dataTree.Push(node.Children[i]);
                    }
                }
            }
        }

        internal int GenerateTerminal(int[] terSet)
        {
            if (terSet.Length < 1)
                throw new Exception("Terminal set is empty.");
            var randIndex = Globals.radn.Next(0, terSet.Length);
            return terSet[randIndex];
        }

        internal int GenerateFunction(Function[] funSet)
        {
            if (funSet.Length < 1)
                throw new Exception("Function set is empty.");
            var randIndex = Globals.radn.Next(0, funSet.Length);
            return funSet[randIndex].Id;
        }

        /// <summary>
        /// Returns zero-based index of the ith Node from the tree based on Depth-First search method
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node NodeAt(int index, bool deptFirstTree = true)
        {
            if (deptFirstTree)
                return TraverseDFT().ElementAt(index);
            else
                return TraverseBFT().ElementAt(index);
        }
        /// <summary>
        /// Count nodes in Tree
        /// </summary>
        /// <returns></returns>
        public int NodeCount()
        {
            return TraverseDFT().Count();
        }
        #endregion

        #region string conversion
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var n in TraverseDFT())
            {
                var fId = n.Value;
                var arity = n.Children != null ? n.Children.Count() : -1;
                //
                sb.Append(fId.ToString() + "&" +arity.ToString()+":");
            }
               
            return sb.ToString();
        }
        public bool FromString(string strItems)
        {
            if (string.IsNullOrEmpty(strItems))
                throw new Exception("Empty string.");

            //Collection holds tree nodes
            Stack<Node> dataTree = new Stack<Node>();
            Node node = null;

            //Add tail recursion
            dataTree.Push(this);

            //  
            var strChs = strItems.Split(':');
            //Extract value from string
            foreach (var sn in strChs/*strItems.Length >= index + 4*/)
            {
                //check if string is valid
                if (string.IsNullOrEmpty(sn))
                    continue;
                if (dataTree.Count==0)
                    continue;
                //get next node
                node = dataTree.Pop();

                //pasrse string for id and arity
                var s = sn.Split('&');
                var str = s[0];
                var art = s[1];

                //parse node value
                if (!int.TryParse(str, out int value))
                    value = 0;
                //parse arity
                if (!int.TryParse(art, out int arity))
                    arity = 0;
                //
                node.val = value;

                //check if node if function node
                if (Globals.IsFunction(node.val))
                {
                    //int arity = Globals.FunctionAritry(node.val);
                    node.initializeNodeOffspring(arity);

                    for (int i = arity - 1; i >= 0; i--)
                    {
                        //
                        node.Children[i].Level = (short)(node.Level + 1);
                        dataTree.Push(node.Children[i]);
                    }

                }
            }

            return true;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="getDataInputRow">input vector</param>
        /// <param name="param">GP parameters</param>
        /// <returns>vector as evaluation of result</returns>
        public double[] Evaluate2(double[] getDataInputRow, IParameters param)
        {
            double[] args = null;
            var argColl = new Stack<double>();

            //we need to enumerate in reverse order
            foreach (var n in TraverseDFT().Reverse())
            {
                if (Globals.IsFunction(n.val))
                {
                    int functionId = Globals.FunctionIndexFromNode(n.val);
                    var arity = Globals.FunctionArity(n.val);

                    //if root node is specified arity and parameters must be defined
                    if (arity == -1 && param != null && param.RootFunctionNode != null && n.val == param.RootFunctionNode.Id)
                        arity = param.RootFunctionNode.Arity;

                    //if function has parameters 
                    //it must be specified as element of the passing array
                    if (param != null && param.RootFunctionNode != null && n.val == param.RootFunctionNode.Id && param.RootFunctionNode.HasParameter)
                        args = new double[arity + 1];
                    else
                        args = new double[arity];
                    //extract variable
                    for (int i = 0; i < arity; i++)
                    {
                        var num = argColl.Pop();

                        //check if extracted value is valid
                        if (double.IsNaN(num) || double.IsInfinity(num))
                            return null;

                        //
                        args[i] = num;

                        //if parameters exists
                        if (param != null && param.RootFunctionNode != null && n.val == param.RootFunctionNode.Id && param.RootFunctionNode.HasParameter)
                            args[i + 1] = param.RootFunctionNode.Parameter;
                    }
                    //root node returns vector
                    if(functionId==51 && param.IsMultipleOutput)
                    {
                        var retVal = Function.Evaluate2(functionId, param.IsProtectedOperation, args);
                        //
                        for(int l = retVal.Length-1; l >= 0; l--)
                            argColl.Push(retVal[l]);
                    }
                    else//root node returns one value
                    {
                        var retVal = Function.Evaluate(functionId, param.IsProtectedOperation, args);
              
                        if (double.IsNaN(retVal) || double.IsInfinity(retVal))
                            return null;
                        //
                        argColl.Push(retVal);
                    }
                   
                }
                else
                {
                    //extract data value from terminal id
                    var ind = Globals.TerminalIndexFromNode(n.val);
                    var dataValue = getDataInputRow[ind];
                    argColl.Push(dataValue);
                }
            }
            // return the only value from stack
            return argColl.ToArray();
        }
        /// <summary>
        /// evaluate chromosome for input vector
        /// </summary>
        /// <param name="getDataInputRow">input vector</param>
        /// <param name="param"></param>
        /// <returns>value represent calculation of all function in tree structure</returns>
        public double Evaluate(double[] getDataInputRow, IParameters param)
        {
            double[] args = null;
            var argColl = new Stack<double>();

            //we need to enumerate in reverse order
            foreach (var n in TraverseDFT().Reverse())
            {
                if(Globals.IsFunction(n.val))
                {
                    int functionId = Globals.FunctionIndexFromNode(n.val);
                    var arity = Globals.FunctionArity(n.val);

                    //if root node is specified arity and parameters must be defined
                    if (arity == -1 && param!= null && param.RootFunctionNode != null && n.val == param.RootFunctionNode.Id)
                        arity = param.RootFunctionNode.Arity;
                   
                    //if function has parameters 
                    //it must be specified as element of the passing array
                    if (param !=null && param.RootFunctionNode!=null && n.val== param.RootFunctionNode.Id && param.RootFunctionNode.HasParameter)
                        args = new double[arity+1];
                    else
                        args = new double[arity];
                    //extract variable
                    for (int i = 0; i < arity; i++)
                    {
                        var num = argColl.Pop();

                        //check if extracted value is valid
                        if (double.IsNaN(num) || double.IsInfinity(num))
                            return double.NaN;

                        //
                        args[i] = num;

                        //if parameters exists
                        if (param != null && param.RootFunctionNode != null && n.val == param.RootFunctionNode.Id && param.RootFunctionNode.HasParameter)
                            args[i + 1] = param.RootFunctionNode.Parameter;
                    }

                    var retVal = Function.Evaluate(functionId, param.IsProtectedOperation, args);
                    if (double.IsNaN(retVal) || double.IsInfinity(retVal))
                        return double.NaN;
                    //
                    argColl.Push(retVal);

                }
                else
                {
                    //extract data value from terminal id
                    var ind = Globals.TerminalIndexFromNode(n.val); 
                    var dataValue = getDataInputRow[ind];
                    argColl.Push(dataValue);
                }
            }
            // return the only value from stack
            return argColl.Pop();
        }

        #region private Methods
        /// <summary>
        /// Traverse with depth first by using Stack collection
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> TraverseDFT()
        {
            var stack = new Stack<Node>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node;
                //enumerate children
                if (node.Children != null)
                {
                    for (int i = node.Children.Length - 1; i >= 0; i--)
                        stack.Push(node.Children[i]);
                }

            }
        }

        /// <summary>
        /// Traverse with breath first by using Queue collection
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Node> TraverseBFT()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node;
                //enumerate children
                if (node.Children != null)
                {
                    for (int i = node.Children.Length - 1; i >= 0; i--)
                        queue.Enqueue(node.Children[i]);
                }

            }
        }
        
        /// <summary>
        /// Initialize node, by assigning node value from available function and terminal set
        /// </summary>
        /// <param name="maxLevel"></param>
        /// <returns>return number of offspring</returns>
        private int initializeNode(int maxLevel, Generation genType, Function[] funcSet, int[] terSet)
        {
            //constrains for maximum level
            if (Level > maxLevel)
                throw new Exception($"Maximum levels of {maxLevel} exceeded.");
            else if (Level == maxLevel)
                val = GenerateTerminal(terSet);
            else if (Level < maxLevel && genType == Generation.strict)
                val = GenerateFunction(funcSet);
            else if (Level < maxLevel & genType == Generation.random)
            {
                //give more chance to generate function than terminal
                if (Globals.radn.Next(5) == 1)
                    val = GenerateTerminal(terSet);
                else
                    val = GenerateFunction(funcSet);

            }
            else
                throw new Exception("Unsupported case!");

            //Node children generation 
            if (Globals.IsFunction(val))
                return Globals.FunctionArity(val);
            else
                return 0;
        }

        /// <summary>
        /// generates offspring of current node and set current node level
        /// </summary>
        /// <param name="count"></param>
        public void initializeNodeOffspring(int count)
        {
            //Create children of node
            if(count > 0)
                Children = new Node[count];
           
            for (int i = count-1; i >= 0; i--)
            {
                Children[i] = new Node();
                Children[i].Level = (short)(Level + 1);//increase tree level 
            }

        }
        #endregion
    }
}

