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
using GPdotNet.BasicTypes;
using GPdotNet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GPdotNet.Core
{
    /// <summary>
    /// Class to implement Population in  Genetic Programming
    /// </summary>
    public class Population
    {
        //Current generation of the population.
        internal List<IChromosome> m_Chromosomes;

        //offspring generation of the population
        internal List<IChromosome> m_Offspring;

        //used for stop parallel evaluation
        public CancellationToken Token { get; set; }

        //used for testing only
        public List<IChromosome> Chromosomes => m_Chromosomes;
        /// <summary>
        /// Constructor of the population
        /// </summary>
        public Population()
        {
            m_Chromosomes = new List<IChromosome>();
        }


        #region Initialization
        /// <summary>
        /// starting to initialize the population
        /// </summary>
        /// <param name="param"></param>
        /// <param name="terSet"></param>
        /// <param name="funSet"></param>
        internal void Initialize(Parameters param, Function[] funSet, int[] terSet)
        {
            if (m_Chromosomes != null)
                m_Chromosomes.Clear();
            else
                m_Chromosomes = new List<IChromosome>();
            
            //generate three times bigger initial population in order to select much more different chromosomes
            GenerateValidPopulation(param, funSet, terSet, 3);

        }

        private void GenerateValidPopulation(Parameters param, Function[] funSet, int[] terSet, int factor=1)
        {
            ////generate until all chromosomes are with valid fitness
            //int tries = 0;
            //while (true)
            //{
                //remove all bad chromosomes
                m_Chromosomes.RemoveAll(ch =>
                {
                    if (float.IsNaN(ch.Fitness) || float.MinValue == ch.Fitness || float.MaxValue == ch.Fitness || float.IsInfinity(ch.Fitness))
                    {
                        //ch.Destroy();
                        return true;
                    }
                    else
                        return false;
                }

                    );
                //if the population has less chromosome than defined generate new one
                if (m_Chromosomes.Count < param.PopulationSize /*&& tries <= 5*/)
                    GeneratePopulation(param.PopulationSize*factor - m_Chromosomes.Count, param, funSet, terSet);
            //    else
            //        break;
            //    tries++;
            //}
        }

        /// <summary>
        /// Selection of method to initialize first population
        /// </summary>
        /// <param name="size"></param>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        private void GeneratePopulation(int size, Parameters param, Function[] funSet, int[] terSet)
        {
            switch (param.InitializationMethod)
            {
                case InitializationMethod.Full:
                    FullInitialization(size, param.MaxInitLevel, (level) => Generate(level, param, funSet, terSet));
                    break;
                case InitializationMethod.Grow:
                    GrowInitialization(size, param.MaxInitLevel, (level) => Generate(level, param, funSet, terSet));
                    break;
                case InitializationMethod.HalfHalf:
                    HalfHalfInitialization(size, param.MaxInitLevel, (level) => Generate(level, param, funSet, terSet));
                    break;
                default:
                    HalfHalfInitialization(size, param.MaxInitLevel, (level) => Generate(level, param, funSet, terSet));
                    break;
            }

            //evaluate each chromosome
            EvaluatePopulation(param, 0);
        }

        /// <summary>
        /// Generate chromosome based on the level, parameters terminal and function set
        /// </summary>
        /// <param name="level"></param>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        /// <returns></returns>
        private IChromosome Generate(int level, Parameters param, Function[] funSet, int[] terSet)
        {
            var ch = new Chromosome();

            //generate chromosome
            ch.Generate(level, Generation.strict, funSet, terSet, param.RootFunctionNode);
            return ch;
        }

        /// <summary>
        /// Full initialization method, all chromosomes have the same level.
        /// </summary>
        /// <param name="size"></param>
        private void FullInitialization(int size, int maxLevels, Func<int, IChromosome> generateChromosome)
        {
            //Chromosome generation with exact level 
            for (int i = 0; i < size; i++)
            {
                // generate new chromosome
                IChromosome c = generateChromosome(maxLevels);
                //add chromosome in to population
                m_Chromosomes.Add(c);
            }
        }
        /// <summary>
        /// Every chromosome have randomly generated  levels between 2 and maximumValue.
        /// </summary>
        /// <param name="size"></param>
        private void GrowInitialization(int size, int maxLevels, Func<int, IChromosome> generateChromosome)
        {
            int levels = 2;
            //Chromosome generation with exact level 
            for (int i = 0; i < size; i++)
            {
                //Randomly choose which level chromosome will have
                levels = Globals.radn.Next(2, maxLevels + 1);

                // generate new chromosome
                IChromosome c = generateChromosome(levels);

                // add chromosome in to population
                m_Chromosomes.Add(c);
            }
        }


        /// <summary>
        /// HalfAndHalf method of initialization,all chromosomes are grouped in equal number
        /// and generates with the same levels.
        /// </summary>
        /// <param name="size"></param>
        private void HalfHalfInitialization(int size, int maxLevels, Func<int, IChromosome> generateChromosome)
        {

            //Make equal group for each level
            int br = (size / maxLevels);

            //Create equal number of chromosomes for each level 
            for (int i = 2; i <= maxLevels; i++)
            {
                if (i == maxLevels)//at the end take the rest 
                    br = (size - ((i - 2) * br));
                //Chromosome generation with exact level 
                for (int j = 0; j < br; j++)
                {
                    // create new chromosome
                    IChromosome c = generateChromosome(i);

                    // add to chromosomes                  
                    m_Chromosomes.Add(c);
                }
            }
        }


        #endregion

        #region Genetic Operators
        /// <summary>
        /// Performing crossover in the population
        /// </summary>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        public void Crossover(Parameters param, Function[] funSet, int[] terSet)
        {

            //if crossover is defined
            if (param.CrossoverProbability == 0)
                return;
            for (int i = 1; i < param.PopulationSize; i += 2)
            {

                if (Globals.radn.NextDouble() <= param.CrossoverProbability)
                {
                    //int k = Globals.radn.Next(0, param.PopulationSize);
                    //int l = Globals.radn.Next(0, param.PopulationSize);

                    int counter = 1;
                    //brood size recombination
                    while (counter <= param.BroodSize)
                    {
                        // cloning the chromosome and prepare for crossover
                        var ch1 = SelectChromosome(m_Chromosomes, param);//m_Chromosomes[k].Clone();
                        var ch2 = SelectChromosome(m_Chromosomes, param);//m_Chromosomes[l].Clone();

                        // crossover
                        ch1.Crossover(ch2, param.MaxLevel, funSet, terSet);

                        //reset fitness
                        ch1.Fitness = float.MinValue;
                        ch2.Fitness = float.MinValue;

                        //add new chromosomes to offspring population
                        m_Offspring.Add(ch1);
                        m_Offspring.Add(ch2);
                        //m_Chromosomes.Add(ch1);
                        //m_Chromosomes.Add(ch2);

                        //
                        counter++;

                        if (((Chromosome)ch1).expressionTree.Level > param.MaxLevel)
                            throw new Exception("During crossover the max level exceeded!");
                        if (((Chromosome)ch2).expressionTree.Level > param.MaxLevel)
                            throw new Exception("During crossover the max level exceeded!");
                    }

                }
            }



        }
        /// <summary>
        /// Performing mutation in the population
        /// </summary>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        public void Mutate(Parameters param, Function[] funSet, int[] terSet)
        {
            //if mutation is defined
            if (param.MutationProbability == 0)
                return;
            for (int i = 0; i < param.PopulationSize; i++)
            {
                // 
                if (Globals.radn.NextDouble() <= param.MutationProbability)
                {
                    int counter = 1;
                    //brood size recombination
                    while (counter <= param.BroodSize)
                    {
                        var ch1 = SelectChromosome(m_Chromosomes, param);

                        ch1.Mutate(param.MaxLevel, funSet, terSet);
                        ch1.Fitness = float.MinValue;
                        //add new chromosomes to offspring population
                        m_Offspring.Add(ch1);
                        //m_Chromosomes.Add(ch1);
                        counter++;
                    }

                }
            }

        }
        #endregion

        #region Evaluation Population
        /// <summary>
        /// Evaluation of the population. Implemented parallel Genetic Programming .
        /// </summary>
        /// <param name="param"></param>
        /// <param name="fitnessFun"></param>
        public void EvaluatePopulation(Parameters param, int iteration)
        {
            
            //evaluate current population
            Evaluate(m_Chromosomes, param, iteration);

            //evaluate offspring population
            Evaluate(m_Offspring, param, iteration);
        }

        private void Evaluate(List<IChromosome> chs, Parameters param, int iteration)
        {
            if (chs == null || chs.Count == 0)
                return;

            //Use parallel only with GP 
            if (param.ParallelProcessing)
            {
                //decrease number of processors to be used in evaluation
                //we don't want to take all resources from the PC
                var pc = Environment.ProcessorCount;
                if (pc > 1)
                    pc--;
                int count = chs.Count;
                // Use ParallelOptions instance to store the CancellationToken
                ParallelOptions po = new ParallelOptions();
                po.CancellationToken = Token;
                po.MaxDegreeOfParallelism = pc;

                System.Threading.Tasks.Parallel.For(0, count, po, (i) =>
                    {
                        if (chs[i].Fitness == float.MinValue)
                        {
                            var ch = chs[i] as Chromosome;

                            chs[i].Fitness = param.FitnessFunction.Evaluate(ch, param, iteration);

                        }

                        if(po.CancellationToken !=null)
                            po.CancellationToken.ThrowIfCancellationRequested();
                    });
            }
            else
            {
                for (int i = 0; i < chs.Count; i++)
                {
                    if (chs[i].Fitness == float.MinValue)
                    {
                        var ch = chs[i] as Chromosome;

                        ch.Fitness = param.FitnessFunction.Evaluate(ch, param, iteration);
                    }
                }
            }
        }

        #endregion

        #region Selection Methods
        /// <summary>
        /// Performing selection in population
        /// </summary>
        public void Reproduction(Parameters param, Function[] funSet, int[] terSet)
        {
            var chs = m_Chromosomes;
            // probability of reproduction
            int repNumber = (int)(param.SelectionProbability * param.PopulationSize) - param.Elitism>=0? param.Elitism:0;


            //remove all bad chromosomes
            chs.RemoveAll(ch =>
            {
                if (float.IsNaN(ch.Fitness) || float.MinValue==ch.Fitness || float.MaxValue == ch.Fitness || float.IsInfinity(ch.Fitness))
                {
                    //ch.Destroy();
                    return true;
                }
                else
                    return false;
            }

                );

            //Elitism number of very best chromosome to survive to new generation
            if (param.Elitism > 0)
            {
                int i = param.Elitism;
                foreach (var c in chs.OrderByDescending(x => x.Fitness))
                {
                    if (i == 0)
                        break;
                    m_Offspring.Add(c.Clone());
                    i--;
                }
            }

            //reproduce repNumber of chromosomes in to new population
            for (int j = 0; j < repNumber; j++)
            {
                Chromosome c = SelectChromosome(chs, param);
                m_Offspring.Add(c);
            }
        }

        internal void PrepareForMating()
        {
            m_Offspring = new List<IChromosome>();
        }

        private Chromosome SelectChromosome(List<IChromosome> chs, Parameters param)
        {
            if (param.SelectionMethod == SelectionMethod.FitnessProportionateSelection)
                return  FitnessSelection(chs, param);
            else if (param.SelectionMethod == SelectionMethod.Rankselection)
                return RankSelection(chs, param);
            else if (param.SelectionMethod == SelectionMethod.TournamentSelection)
                return TournamentSelection(chs, param);
            else
                return FitnessSelection(chs, param);
        }

        internal void CreateNewGeneration(Parameters par, Function[] funSet, int[] terSet)
        {

            m_Chromosomes.Clear();
            // m_Chromosomes.AddRange(m_Offspring.OrderByDescending(x=>x.Fitness).GroupBy(x=>x.Fitness).Select(grp=>grp.First()).Take(par.PopulationSize));
            m_Chromosomes.AddRange(m_Offspring.OrderByDescending(x => x.Fitness).Take(par.PopulationSize));


            //if the population has less chromosome than defined generate new one
            if (m_Chromosomes.Count < par.PopulationSize)
                GenerateValidPopulation(par, funSet, terSet);
        }

        private Chromosome FitnessSelection(List<IChromosome> ch, Parameters par)
        {
            if (ch == null || par == null || ch.Count == 0)
                throw new Exception("Population cannot be empty.");

            //parameters of the selection
            double sumOfFitness = 0;

            //calculate sum of fitness
            for (int i = 0; i < ch.Count; i++)
            {
                //check for invalid fitness
                if (double.IsNaN(ch[i].Fitness) || double.IsInfinity(ch[i].Fitness))
                    sumOfFitness += 0;
                else
                    sumOfFitness += ch[i].Fitness;
            }

            //generate random selection point
            var selPoint = Globals.radn.NextDouble() * sumOfFitness;

            //enumerate population from the best to the worst
            double currentScore = 0;
            foreach (var c in ch.OrderByDescending(x => x.Fitness))
            {
                if (selPoint >= currentScore && selPoint <= currentScore + c.Fitness)
                    return c.Clone() as Chromosome;
                currentScore += c.Fitness;
            }

            //this should not hepen
            throw new Exception("Fitness proportionate selection exception.");
        }

        private Chromosome RankSelection(List<IChromosome> ch, Parameters par)
        {
            if (ch == null || par == null || ch.Count == 0 || par.ArgValue < 0 || par.ArgValue > 2)
                throw new Exception("Population cannot be empty.");

            //parameters of the selection
            double N = ch.Count;
            double rMax = par.ArgValue;//par.ArgValue;
            double rMin = 2.0 - rMax;

            //sort the chromosomes
            //m_Chromosomes.Sort();

            //calculate rank probability for each chromosome
            double[] rankProb = new double[(int)N];
            int i = 0;
            foreach (var c in ch.OrderByDescending(x => x.Fitness))
            {
                rankProb[i] = (rMin + (rMax - rMin) * (N - i) / (N - 1)) / N;
                i++;
            }

            //random number
            double selPoint = Globals.radn.NextDouble();

            //
            double currentScore = 0;
            i = 0;
            foreach (var c in ch.OrderByDescending(x => x.Fitness))
            {
                if (selPoint >= currentScore && selPoint <= (currentScore + rankProb[i]))
                    return ch[i].Clone() as Chromosome;

                currentScore += rankProb[i];
                i++;
            }

            return ch[(int)N - 1].Clone() as Chromosome;

        }

        private Chromosome TournamentSelection(List<IChromosome> ch, Parameters par)
        {
            if (ch == null || par == null || ch.Count == 0 || par.ArgValue <=0 )
                throw new Exception("Population cannot be empty.");

            //enumerate population from the best to the worst
            int [] w = new int[(int)par.ArgValue];

            for(int i = 0; i < par.ArgValue; i++)
            {
                w[i] = Globals.radn.Next(0, ch.Count);
            }

            //compete selected chromosomes
            int winner = w[0];
            for(int i=1; i<par.ArgValue; i++)
            {
                if (ch[winner].Fitness < ch[i].Fitness)
                    winner = i;
            }
            //return winner
            return ch[winner].Clone() as Chromosome;
        }

        public bool IsEmpty()
        {
            if (m_Chromosomes == null || m_Chromosomes.Count == 0)
                return true;
            else
                return false;
        }
        #region Obsolute Selection
        /// <summary>
        /// Fitness proportionate selection.
        /// </summary>
        /// <param name="size"></param>
        private void FitnessProportionateSelection(int size)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();


            int currentSize = m_Chromosomes.Count;
            double sumOfFitness = 0;

            //calculate sum of fitness
            for (int i = 0; i < currentSize; i++)
                sumOfFitness += m_Chromosomes[i].Fitness;


            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;

            for (int i = 0; i < currentSize; i++)
            {
                // cumulative normalized fitness
                s += (m_Chromosomes[i].Fitness / sumOfFitness);
                rangeMax[i] = s;
            }

            // select chromosomes from old chromosomes to the new chromosomes
            for (int j = 0; j < size; j++)
            {
                // get wheel value
                double wheelValue = Globals.radn.NextDouble();
                // find the chromosome for the wheel value
                for (int i = 0; i < currentSize; i++)
                {
                    //double wheelValue = rand.NextDouble();
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new population
                        var ch = m_Chromosomes[i].Clone();
                        newPopulation.Add(ch);
                        break;
                    }
                }
            }


            // old population is going to die
            m_Chromosomes.Clear();

            // survived chromosomes
            m_Chromosomes.AddRange(newPopulation);
        }

        /// <summary>
        /// Rank selection
        /// </summary>
        /// <param name="size"></param>
        private void RankingSelection(int size, Parameters param)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();

            for (int j = 0; j < size; j++)
            {
                var c = RankSelection(m_Chromosomes, param);
                newPopulation.Add(c);
            }

            // old population is going to die
            m_Chromosomes.Clear();

            // survived chromosomes
            m_Chromosomes.AddRange(newPopulation);
            /*
                        // size of current chromosomes
                        int currentSize = m_Chromosomes.Count;

                        // calculate amount of ranges in the wheel
                        double ranges = currentSize * (currentSize + 1) / 2;

                        // create wheel ranges
                        double[] rangeMax = new double[currentSize];
                        double s = 0;

                        for (int i = 0, n = currentSize; i < currentSize; i++, n--)
                        {
                            s += ((double)n / ranges);
                            rangeMax[i] = s;
                        }

                        // select chromosomes from old chromosomes to the new chromosomes
                        for (int j = 0; j < size; j++)
                        {
                            // get wheel value
                            double wheelValue = Globals.radn.NextDouble();
                            // find the chromosome for the wheel value
                            for (int i = 0; i < currentSize; i++)
                            {
                                // get wheel value
                                if (wheelValue <= rangeMax[i])
                                {
                                    // add the chromosome to the new chromosomes
                                    newPopulation.Add(m_Chromosomes[i].Clone());
                                    break;
                                }

                            }
                        }

                        // old population is going to die
                        m_Chromosomes.Clear();

                        // survived chromosomes
                        m_Chromosomes.AddRange(newPopulation);
                        */
        }

        /// <summary>
        /// Classic tournament selection
        /// </summary>
        /// <param name="size"></param>
        private void Tournament1Selection(int size, int tourSize)
        {
            // size of current population
            int currentSize = m_Chromosomes.Count;
            List<IChromosome> tourn = new List<IChromosome>(tourSize);

            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();

            for (int j = 0; j < size; j++)
            {
                currentSize = m_Chromosomes.Count;
                for (int i = 0; i < tourSize && i < currentSize; i++)
                {
                    int ind = Globals.radn.Next(currentSize);
                    tourn.Add(m_Chromosomes[ind]);

                }

                if (tourn.Count > 0)
                {
                    tourn.Sort();
                    newPopulation.Add(tourn[0].Clone());
                    m_Chromosomes.Remove(tourn[0]);
                    tourn.Clear();
                }
            }

            // old population is going to die
            m_Chromosomes.Clear();

            // survived chromosomes
            m_Chromosomes.AddRange(newPopulation);
        }

        /// <summary>
        /// Skrgic selection method, originally implemented by GPdotNET 
        /// more info: https://bhrnjica.net/2011/10/20/skrgic-selection-in-gpdotnet/
        /// </summary>
        /// <param name="size">population size for new generation</param>
        /// <param name="argValue">additional method argument.</param>
        private void SkrgicSelection(int size, float argValue)
        {
            // new chromosomes, initially empty
            List<IChromosome> newPopulation = new List<IChromosome>();

            //  double k = 0.2; //additionalParameter;
            double fitnessMax = m_Chromosomes.Max(x => x.Fitness) * (1.0 + argValue);
            //
            for (int i = 0; i < size; i++)
            {
                //random index from the population
                int randomIndex = Globals.radn.Next(0, m_Chromosomes.Count);

                //Random number between 0 -maxFitnes including maxFitness value,
                double randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);

                while (true)
                {

                    //if random generated number <= fitnesValue, perform selection
                    if (randomFitness <= m_Chromosomes[randomIndex].Fitness * (1.0 + argValue / fitnessMax))
                    {
                        newPopulation.Add(m_Chromosomes[randomIndex].Clone());
                        break;
                    }

                    randomIndex = Globals.radn.Next(0, m_Chromosomes.Count);
                    randomFitness = Globals.radn.NextDouble(0, fitnessMax/*, true include MaxValue*/);
                }
            }

            // old population is going to die
            m_Chromosomes.Clear();

            // survived chromosomes
            m_Chromosomes.AddRange(newPopulation);
        }

        #endregion


        #endregion

        #region Helper
        public void Clear()
        {
            if (m_Chromosomes != null && m_Chromosomes.Count > 0)
                m_Chromosomes.Clear();
        }

        public string PopulationToString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                if (m_Chromosomes != null && m_Chromosomes.Count > 0)
                {
                    foreach (var c in m_Chromosomes)
                    {
                        sb.Append(c.ToString() + Environment.NewLine);
                    }

                    return sb.ToString();
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public void PopulationFromString(string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    return;
                m_Chromosomes.Clear();
                var chs = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                //
                foreach (var s in chs)
                {
                    var c = new Chromosome();
                    c.FromString(s);
                    m_Chromosomes.Add(c);
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

    }
}
