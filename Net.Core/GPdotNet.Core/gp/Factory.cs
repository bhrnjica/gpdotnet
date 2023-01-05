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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Globalization;
using System.Text.Json;


namespace GPdotNet.Core
{
    
   
    public class IterationStatistics
    {
        public float MaximumFitness { get; set; }
        public float AverageFitness { get; set; }
        public float IterationSeconds { get; set; }
    }
    public class ProgressReport
    {
        public DateTime SolutionStarted { get; set; }
        public IterationStatistics IterationStatistics { get; set; }

        public int Iteration { get; set; }

        public Chromosome BestSolution { get; set; }

        public int BestIteration { get; set; }

        public IterationStatus IterationStatus { get; set; }

        public string Message { get; set; }


    }
    
    public class TerminationCriteria
    {
        public bool IsIteration { get; set; }

        public float Value { get; set; }
    }
   
    [DataContract]
    public class Evolution
    {
        [DataMember]
        public float MaxFitness { get; set; }
        [DataMember]
        public float AvgFitness { get; set; }
        [DataMember]
        public int Iteration { get; set; }
        [DataMember]
        public IterationStatus Status { get; internal set; }
    }
    
    public class Factory
    {

        private Parameters  m_Parameters;
        private int[]       m_TerminalSet;
        private Function[]  m_FunctionSet;
        private List<Evolution> m_History = null;

        ProgressReport      m_progresReport = null;

        Population          m_Population = null;
        TerminationCriteria m_TerminationCriteria=null;

        //default constructor
        public Factory() { m_progresReport = new ProgressReport() { BestIteration = -1,
            BestSolution = null, Iteration = -1, IterationStatus = IterationStatus.Initialize,
            IterationStatistics = new IterationStatistics() { AverageFitness = -1, MaximumFitness = -1 } }; }

        public Function[] FunctionSet { get { return m_FunctionSet; } set { m_FunctionSet = value; } }
        public Parameters Parameters  { get { return m_Parameters; } set { m_Parameters = value; } } 
        public int[] TerminalSet { get { return m_TerminalSet; } set { m_TerminalSet = value; } }
        public ProgressReport ProgresReport { get { return m_progresReport; } set { m_progresReport = value; } }
        public Population Population => m_Population;
        public TerminationCriteria TC { get { return m_TerminationCriteria; } set { m_TerminationCriteria = value; } } 
        public List<Evolution> History { get { return m_History; } set { m_History = value; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        public Factory(Parameters param, Function[] funSet, int[] terSet, CancellationToken token)
        {
            //create population
            m_Population = new Population();
            m_Population.Token = token;
            m_History = new List<Evolution>();

            //prepare progress report for the first time.
            m_progresReport = new ProgressReport();
            m_progresReport.Iteration = 0;
            m_progresReport.IterationStatus = IterationStatus.Initialize;
            m_progresReport.IterationStatistics = new IterationStatistics();
            m_progresReport.Message = "GPFactory successfully created algorithm.";
            m_progresReport.BestSolution = new Chromosome();
            m_progresReport.IterationStatistics.IterationSeconds = 0;

            //initialize main GP components parameters, function and terminals
            initMainComponents(param, funSet, terSet);

        }
        

        public string GPFactoryToString()
        {

            string popStr = m_Parameters == null || m_Population == null ? "" : m_Population.PopulationToString();
            if (m_History == null)
                m_History = new List<Evolution>();
            var jsonStr = JsonSerializer.Serialize( new
                                {
                                    m_Parameters,
                                    m_TerminalSet,
                                    m_FunctionSet,
                                    m_progresReport,
                                    m_TerminationCriteria,
                                    history= m_History.ToArray(),
                                    popStr
                                 });
            return jsonStr;
        }

        public void GPFactoryFromString(string strFactory)
        {
            if (string.IsNullOrEmpty(strFactory))
                return;
            try
            {
                Evolution[] hist=null;
                //JsonSerializerSettings sett = new JsonSerializerSettings();
                //sett.NullValueHandling = NullValueHandling.Ignore;
                var opt = new JsonSerializerOptions();
                

                var obj = JsonSerializer.Deserialize<IDictionary<string, object>>(strFactory, new JsonSerializerOptions());

                var param = obj["m_Parameters"].ToString();
                var ter = obj["m_TerminalSet"];
                var fun = obj["m_FunctionSet"];
                var prog = obj["m_progresReport"];
                var tc = obj["m_TerminationCriteria"];
                var hi = obj["history"];
                var pop = obj["popStr"];

                if(param != null)
                    m_Parameters = JsonSerializer.Deserialize<Parameters>(param);
                
                if (ter != null)
                    m_TerminalSet = JsonSerializer.Deserialize<int[]>(ter.ToString()); 

                if(fun != null)
                    m_FunctionSet = JsonSerializer.Deserialize<Function[]>(fun.ToString());

                if (prog != null)
                    m_progresReport = JsonSerializer.Deserialize<ProgressReport>(prog.ToString());

                if (tc != null)
                    m_TerminationCriteria = JsonSerializer.Deserialize<TerminationCriteria>(tc.ToString()); 
                
                if (hi != null)
                    hist = JsonSerializer.Deserialize<Evolution[]>(hi.ToString());

                //create history
                if (hist != null)
                    m_History = new List<Evolution>(hist);

                m_Population = new Population();

                if (pop !=null)
                {
                    var popStr = pop.ToString(); 
                    m_Population.PopulationFromString(popStr);
                }
               

            }
            catch (Exception)
            {

                throw;
            }
          

        }

        /// <summary>
        /// Continue running factory with last reached generation
        /// </summary>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        public void Continue(Parameters param, Function[] funSet, int[] terSet)
        {
            //
            this.m_Parameters = param;
            this.m_TerminalSet = terSet;
            this.m_FunctionSet = funSet;
            //
            m_progresReport.IterationStatus = IterationStatus.Continue;
            
        }


        public void Run(Action<ProgressReport, Parameters> reportProgress, TerminationCriteria tc, CancellationToken ct)
        {
            m_TerminationCriteria = tc;
            Task.Run(() => startEvolution(reportProgress, tc, ct));
        }

        public async Task RunAsync(Action<ProgressReport, Parameters> reportProgress, TerminationCriteria tc, CancellationToken ct)
        {
            m_TerminationCriteria = tc;
            await Task.Run(() => startEvolution(reportProgress, tc, ct));
        }
        /// <summary>
        /// perform initialization of main GP components
        /// </summary>
        /// <param name="param"></param>
        /// <param name="funSet"></param>
        /// <param name="terSet"></param>
        private void initMainComponents(Parameters param, Function[] funSet, int[] terSet)
        {
            //
            this.m_Parameters = param;
            this.m_TerminalSet = terSet;
            this.m_FunctionSet = funSet;

            //init population
            m_Population.Initialize(param, funSet, terSet);
            //
            m_Population.EvaluatePopulation(param, 0);
            calculatePopulation(m_Population.m_Chromosomes, m_progresReport);

        }

        /// <summary>
        /// Start with evolution 
        /// </summary>
        /// <param name="reportProgress"></param>
        /// <param name="tc"></param>
        /// <param name="ct"></param>
        private void startEvolution(Action<ProgressReport, Parameters> reportProgress, TerminationCriteria tc, CancellationToken ct)
        {
            Stopwatch sp = new Stopwatch();

            try
            {
                //send report population initialized and evaluated
                m_History.Add(initEvolution(m_progresReport));
                reportProgress(m_progresReport,m_Parameters);

                //init termination criteria
                int iterationCount = m_progresReport.Iteration;
                m_progresReport.IterationStatus = IterationStatus.Runing;

                //
                float bestFitness = 0;
                bool breakLoop = false;
                while (!breakLoop)
                {
                    //report progress
                    sp.Restart();
                    m_progresReport.IterationStatus = IterationStatus.Runing;
                    iterationCount++;
                    //
                    performIteration(m_progresReport,ct);

                    //get best solution for the current iteration
                    bestFitness = m_progresReport.BestSolution.Fitness;

                    //check for termination criteria
                    if (isCriteriaSatisfied(tc, iterationCount, bestFitness) || bestFitness == 1000 )
                    {
                        //stopping iteration
                        m_progresReport.IterationStatus = IterationStatus.Compleated;
                        breakLoop = true;
                    }

                    //check for cancellation criteria
                    if(ct.IsCancellationRequested)
                    {
                        //canceling iteration
                        m_progresReport.IterationStatus = IterationStatus.Stopped;
                        breakLoop = true;
                    }
                   
                    //report progress
                    m_progresReport.Iteration = iterationCount;

                    //pr.BestSolution= bestFitness
                    m_progresReport.IterationStatistics.IterationSeconds =  (float)sp.Elapsed.TotalSeconds;

                    //send report and store in history
                    m_History.Add(initEvolution(m_progresReport));
                    reportProgress(m_progresReport, m_Parameters);
                }
            }
            catch (Exception ex)
            {
                m_progresReport.IterationStatus = IterationStatus.Exception;
                m_progresReport.Message = ex.Message;

                m_History.Add(initEvolution(m_progresReport));
                reportProgress(m_progresReport,m_Parameters);
                // throw;
            }

        }

        private Evolution initEvolution(ProgressReport pReport)
        {
            var e = new Evolution();
            e.Iteration = pReport.Iteration;
            e.MaxFitness = pReport.IterationStatistics.MaximumFitness;
            e.AvgFitness = pReport.IterationStatistics.AverageFitness;
            e.Status = pReport.IterationStatus;

            //
            return e;
        }


        /// <summary>
        /// perform one iteration evolution for the GP
        /// </summary>
        /// <param name="pr"></param>
        private void performIteration(ProgressReport pr, CancellationToken ct)
        {

            //mating preparation offspring population
            m_Population.PrepareForMating();

            Stopwatch sw = Stopwatch.StartNew();
            string str = $"It={pr.Iteration}; ";
           
            //1. operaton
            m_Population.Crossover(m_Parameters, m_FunctionSet, m_TerminalSet);
            str += $"Crossover ={sw.Elapsed.TotalSeconds.ToString("F3")} sec| ";
            sw.Restart();

            //2. operation
            m_Population.Mutate(m_Parameters, m_FunctionSet, m_TerminalSet);
            str += $"Mutation ={sw.Elapsed.TotalSeconds.ToString("F3")} sec| ";
            sw.Restart();

            //3. operation
            m_Population.Reproduction(m_Parameters, m_FunctionSet, m_TerminalSet);
            str += $"Reproduction ={sw.Elapsed.TotalSeconds.ToString("F3")} sec| ";
            sw.Restart();

            //4. operation
            m_Population.EvaluatePopulation(m_Parameters, pr.Iteration);
            str += $"FitnessCalc ={sw.Elapsed.TotalSeconds.ToString("F3")} sec| ";
            sw.Restart();
            
            //
            m_Population.CreateNewGeneration(m_Parameters,m_FunctionSet, m_TerminalSet);
            str += $"NewGen ={sw.Elapsed.TotalSeconds.ToString("F3")} sec| ";
            sw.Restart();
            //
            calculatePopulation(m_Population.m_Chromosomes, pr);
            str += $"Calculation ={sw.Elapsed.TotalSeconds.ToString("F3")} sec| ";
            pr.Message = str;

        }

        /// <summary>
        /// Calculate best chromosome in the current population (generation)
        /// </summary>
        /// <param name="chromosomes"></param>
        /// <param name="pr"></param>
        private void calculatePopulation(List<IChromosome> chromosomes, ProgressReport pr)
        {
            // calculate basic stat of the population
            float fitnessMax = float.MinValue;
            float fitnessSum = 0;

            if (chromosomes != null && chromosomes.Count > 0)
                ProgresReport.BestSolution =  setBestSolution(chromosomes[0]);
            else
                return;

            int counter = 0;
            for (int i = 0; i < chromosomes.Count; i++)
            {
                if (float.IsNaN(chromosomes[i].Fitness))
                    continue;
                counter++;
                float fitness = chromosomes[i].Fitness;
                // sum calculation
                fitnessSum += fitness;

                // cal the best chromosome
                if (fitness > fitnessMax)
                {
                    fitnessMax = fitness;
                    pr.BestSolution = setBestSolution(chromosomes[i]);
                }
            }

           float  fitnessAvg = (float)Math.Round(fitnessSum / counter, 4);

            pr.IterationStatistics.AverageFitness = fitnessAvg;
            pr.IterationStatistics.MaximumFitness = fitnessMax;
            
        }

        private Chromosome setBestSolution(IChromosome chromosome)
        {
            var c = chromosome.Clone(true) as Chromosome;
            return c;
        }


        /// <summary>
        /// Generally two termination criteria are available. Iteration count and best fitness value.
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="counter"></param>
        /// <param name="bestFitness"></param>
        /// <returns></returns>
        private bool isCriteriaSatisfied(TerminationCriteria tc, int counter, float bestFitness)
        {
            if (tc.IsIteration)
                return counter >= tc.Value;
            else
                return bestFitness >= tc.Value;
        }

        

    }
}