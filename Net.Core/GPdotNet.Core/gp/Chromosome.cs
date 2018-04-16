//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the MIT License                                     //
// See license section of  http://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
// use this in case you want to test internal methods
using System.Runtime.CompilerServices;
using GPdotNet.Interfaces;
using GPdotNet.BasicTypes;

[assembly: InternalsVisibleTo("gpdotnet.xunit,PublicKey=6d39bb3776ffb0e4")]

namespace GPdotNet.Core
{
    public class Chromosome : IChromosome
    {
        #region Fields

        //Fitness value for the chromosome
        private float fitness;
        public float Fitness { get => fitness; set => fitness = value; }

        //Additional information initialized while the chromosome being evaluated 
        public Dictionary<int, (double[], double[][])> extraData;
        public Dictionary<int, (double[], double[][])> ExtraData { get => extraData; set => extraData = value; }
        //expression representation of the chromosome
        public Node expressionTree;
        #endregion

        #region Ctor and initialization
        /// <summary>
        /// Default Constructor
        /// </summary>
        public Chromosome()
        {
            expressionTree = null;
            fitness = float.MinValue;
        }

        /// <summary>
        /// Create deep copy of the chromosome
        /// </summary>
        /// <returns></returns>
        public IChromosome Clone(bool includeExtraData = true)
        {
            var ch = new Chromosome();
            ch.fitness = this.fitness;
            
            ch.expressionTree = this.expressionTree.Clone();

            if (ExtraData != null && includeExtraData)
                ch.ExtraData = cloneExtraData();
            return ch;
        }

        //clone extra data of chromosome in case it exists
        private Dictionary<int, (double[], double[][])> cloneExtraData()
        {
            Dictionary<int, (double[], double[][])> ed = null;
            //clone extra daata
            if (ExtraData != null)
            {
                ed = new Dictionary<int, (double[], double[][])>();
                foreach (var k in ExtraData)
                {
                    var cent = new double[k.Value.Item1.Length];
                    Array.Copy(k.Value.Item1, cent, cent.Length);

                    var covMatrix = new double[k.Value.Item2.Length][];
                    for (int i = 0; i < k.Value.Item2.Length; i++)
                    {
                        covMatrix[i] = new double[k.Value.Item2[i].Length];
                        Array.Copy(k.Value.Item2[i], covMatrix[i], k.Value.Item2[i].Length);
                    }

                    ed.Add(k.Key, (cent, covMatrix));
                }

            }
            return ed;
        }
        /// <summary>
        /// Generate random chromosome, with or without predefined root node value
        /// </summary>
        /// <param name="maxLevels">maximum levels of generation</param>
        /// <param name="genType">generation type random or strictly</param>
        /// <param name="funSet">available function set</param>
        /// <param name="terSet"> available terminal set</param>
        /// <param name="rootFunction">predefined function for root node</param>
        public void Generate(int maxLevels, Generation genType, Function[] funSet, int[] terSet, Function rootFunction = null)
        {
            //Create the first node
            this.expressionTree = new Node();
            expressionTree.Generate(maxLevels, genType, funSet, terSet, rootFunction);
            return;
        }

        #endregion

        #region Operations

        /// <summary>
        /// perform mutation operation
        /// </summary>
        public void Mutate(int maxLevels, Function[] funSet, int[] terSet)
        {
            int index1 = Globals.radn.Next(2, expressionTree.NodeCount());
            
           // ApplyMutate(expressionTree, index1, maxLevels, funSet, terSet);
            ApplyMutate(expressionTree, index1, maxLevels, funSet, terSet);
        }

        public static void ApplyMutate(Node ch, int index, int maxLevels, Function[] funSet, int[] terSet)
        {
            var node = ch.NodeAt(index, false);
            //check if the level < maxLevel
            if (node.Level > maxLevels)
                throw new Exception("Level is not a correct number!");
            else if (node.Level == maxLevels)
            {
                node.Value = node.GenerateTerminal(terSet);
                node.Children = null;
            }
            else
            {
                node.Generate(maxLevels, Generation.random, funSet, terSet);
                
            }
        }

        /// <summary>
        /// perform crossover operation
        /// </summary>
        /// <param name="ch2"></param>
        /// <param name="maxLevels"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        public void Crossover(IChromosome ch2, int maxLevels, Function[] funSet, int[] terSet)
        {
            var chromosome2 = ch2 as Chromosome;
            //Get random numbers between 0 and maximum index
            var index1 = Globals.radn.Next(2, expressionTree.NodeCount()); //GetRandomNode(expressionTree.NodeCount());
            var index2 = Globals.radn.Next(2, chromosome2.expressionTree.NodeCount()); //GetRandomNode(chromosome2.expressionTree.NodeCount());

            ApplyCrossover(this.expressionTree, chromosome2.expressionTree, index1, index2, maxLevels, funSet, terSet);
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="index"></param>
        /// <param name="maxLevels"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        public static void ApplyMutate2(Node ch, int index, int maxLevels, Function[] funSet, int[] terSet)
        {
            //We don't want to mutate root
            if (index == 1)
                throw new Exception("Wrong index number for Mutate operation!");

            //start counter from 0
            int count = 0;

            //Collection holds tree nodes
            Stack<Node> dataTree = new Stack<Node>();

            //Add tail recursion
            ch.Level = 0;
            dataTree.Push(ch);
            Node node = null;

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();


                //when the counter is equal to index return current node
                if (count == index)
                {
                    //check if the level < maxLeve
                    if (node.Level > maxLevels)
                        throw new Exception("Level is not a correct number!");
                    else if (node.Level == maxLevels)
                    {
                        node.Value = node.GenerateTerminal(terSet);
                        node.Children = null;
                    }
                    else
                    {
                        node.Generate(maxLevels, Generation.random, funSet, terSet);
                    }
                }

                if (node.Children != null)
                {
                    for (int i = node.Children.Length - 1; i >= 0; i--)
                    {
                        
                           
                        node.Children[i].Level = (short)(node.Level+1);
                        dataTree.Push(node.Children[i]);
                    }
                       
                }

                //count node
                count++;

            }
        }
      
        /// <summary>
        /// Crossover operation on specific index of chromosomes
        /// </summary>
        /// <param name="ch1"></param>
        /// <param name="ch2"></param>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        public static void ApplyCrossover(Node ch1, Node ch2, int index1, int index2, int maxLevels, Function[] funSet, int[] terSet)
        {
            //We don't want to crossover on root node
            if (index1 == 0 || index2 == 0)
                throw new Exception("Wrong index number for Crossover operation!");
            
            //create parts for exchange
            var p1 = ch1.NodeAt(index1, false);
            var p2 = ch2.NodeAt(index2, false);
            
            //clone parts 
            var part1 = p1.Clone();
            var part2 = p2.Clone();

            //share genetic material between parents
            p1.Value = part2.Value;
            p1.Children = part2.Children;
            p2.Value = part1.Value;
            p2.Children = part1.Children;
            // ExchangeMaterial(ch1, index1, part2);
            // ExchangeMaterial(ch2, index2, part1);

            //trim if max levels exceeded
            ch1.Trim(maxLevels, terSet);
            ch2.Trim(maxLevels, terSet);
        }

        /// <summary>
        /// Perform genetic materials exchange. Part from chromosome 1 is assigned to chromosome 2 
        /// </summary>
        /// <param name="chStruct"></param>
        /// <param name="index"></param>
        /// <param name="part"></param>
        private static void exchangeMaterial(Node chStruct, int index, Node part)
        {
            //repeat tail recursion
            int count = 0;
            var dataTree = new Stack<Node>();
            dataTree.Push(chStruct);

            //tail recursion
            Node node = null;
            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Pop();

                //when the counter is equal to index return current node
                if (count == index)
                {
                    node.Value = part.Value;
                    //
                    node.Children = part.Children;
                    // break;
                }

                if (node.Children != null)
                {
                    for (int i = node.Children.Length - 1; i >= 0; i--)
                    {
                        var ch = node.Children[i];
                        //calculate new level
                        ch.Level = (short)(node.Level + 1);
                        dataTree.Push(ch);
                    }

                }

                //count node
                count++;
            }


        }
       
        #endregion

        #region Common Methods
        /// <summary>
        /// Returns expression tree of GPChromosome
        /// </summary>
        /// <returns></returns>
        public Node GetExpression()
        {
            return expressionTree;
        }

       
        /// <summary>
        /// We have to implement smarter algorithm for selecting nodes. Since order of nodes in the container 
        /// is so that the first nodes are functions and then terminals, about half list contains terminal, 
        /// and we have to provide better probability for selecting function instead of terminal
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Node GetRandomNode(Node ch)
        {
            var nc = ch.NodeCount();
            if (nc < 2)
                throw new Exception("Invalid number of chromosome nodes.");

            int index = Globals.radn.Next(2, nc);
            if (index == 1)
                throw new Exception("Wrong index number for Mutate operation!");

            //start counter from 0
            int count = 0;

            //Collection holds tree nodes
            var dataTree = new Queue<Node>();

            //Add tail recursion
            ch.Level = 0;
            dataTree.Enqueue(ch);
            Node node = null;

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();


                //when the counter is equal to index return current node
                if (count == index)
                {
                    return node;
                }

                if (node.Children != null)
                {
                    for (int i = 0; i < node.Children.Length; i++)
                    {


                        node.Children[i].Level = ++node.Level;
                        dataTree.Enqueue(node.Children[i]);
                    }

                }

                //count node
                count++;

            }

            throw new Exception("This line should not be reached.");

        }

        /// <summary>
        /// String representation for the chromosome. It is also used for serializing to txt file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var strExtraD = extraDataToString();
            if(strExtraD == null)
                return string.Format("{0};{1}", fitness.ToString(CultureInfo.InvariantCulture), expressionTree.ToString());
            else
                return string.Format("{0};{1};{2}", fitness.ToString(), expressionTree.ToString(), extraDataToString());
        }

        
        /// <summary>
        ///  Creates chromosome from string. we need to have terminal prior to create chromosome
        /// Create chromosome from string. Chromosome string is stored with the flowing format.
        /// 10.34566;20012005100420032....
        /// fitness;NodeValueNodeValue....;ExtraData
        /// </summary>
        /// <param name="strCromosome">string containing chromosome data</param>
        /// <returns></returns>
        public void FromString(string strCromosome)
        {
            if (string.IsNullOrEmpty(strCromosome))
                return;
            var items = strCromosome.Split(';');

            
            //first item is Fitness. Fitness value must always be formated with POINT 
            if (!float.TryParse(items[0].ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out float fit))
                fit = 0;
            fitness = fit;

            //create expression tree from string
            var node = new Node();
            node.FromString(items[1]);
            expressionTree = node;

            //create extra data stored in the chromosome
            if(items.Length > 2 && !string.IsNullOrEmpty(items[2]))
            {
                extraData =  extraDataFromString(items[2]);
            }
           
        }

        private string extraDataToString()
        {
            string str = "";
            if (extraData == null)
                return null;

            foreach (var e in extraData)
            {
                //
                var data = e.Value;
                var k = e.Key;
                var line = $"{k}$";
                var vector = string.Join(" ", data.Item1.Select(x => x.ToString(CultureInfo.InvariantCulture))) + "$";
                var strMatrix = "";
                //
                foreach (var row in e.Value.Item2)
                {
                    strMatrix += string.Join(" ", row.Select(x => x.ToString(CultureInfo.InvariantCulture))) + " ";
                }

                //joint all three components
                str += line + vector + strMatrix + ":";
            }

            return str;

        }

        private Dictionary<int, (double[], double[][])> extraDataFromString(string strData)
        {
            if (string.IsNullOrEmpty(strData))
                return null;
            //
            var items = strData.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
            if (items == null || items.Length < 1)
                return null;

            Dictionary<int, (double[], double[][])> exData = null;

            //
            foreach (var line in items)
            {
                var it = line.Split(new char[] { '$' }, StringSplitOptions.RemoveEmptyEntries);
                if (it == null || it.Length != 3)
                    break;
                //
                var c = int.Parse(it[0]);
                //vector exctract
                var vector = rowFromString(it[1], ' ');

                //cov matrix extraction
                var matrix = new double[vector.Length][];
                var strMatrix = rowFromString(it[2], ' ');
                //
                for (int i = 0; i < vector.Length; i++)
                {
                    matrix[i] = new double[vector.Length];

                    for (int j = 0; j < vector.Length; j++)
                    {

                        matrix[i][j] = strMatrix[i + j];
                    }
                }

                if (exData == null)
                    exData = new Dictionary<int, (double[], double[][])>();

                exData.Add(c, (vector, matrix));
            }

            return exData;
        }

        double[] rowFromString(string strRow, char delimiter)
        {
            var srow = strRow.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);

            var row = new double[srow.Length];

            for (int i = 0; i < srow.Length; i++)
                row[i] = double.Parse(srow[i], CultureInfo.InvariantCulture);

            return row;
        }

        public int CompareTo(IChromosome other)
        {
            if (other == null)
                return 1;
            return other.Fitness.CompareTo(this.Fitness);
        }

        #endregion
    }
}
