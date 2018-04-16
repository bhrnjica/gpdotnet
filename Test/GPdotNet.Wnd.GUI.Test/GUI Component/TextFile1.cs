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
using GPdotNet.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GPdotNet.Data
{
    /// <summary>
    /// ROC class implemented based on Accord Statistics Library http://accord-framework.net implementation
    /// </summary>
    public class ROC
    {
        private double m_area;     // the exact area computed using the trapezoidal rule
        private double m_error;    // the AUC ROC standard error for sample


        private double[] m_expected; // The ground truth, confirmed data
        private double[] m_prediction;  // The test predictions for the data

        private double[] m_positiveResults; // the subjects which should have been computed as positive
        private double[] m_negativeResults; // the subjects which should have been computed as negative

        private double[] m_positiveAccuracy; // DeLong's pseudoaccuracy for positive subjects
        private double[] m_negativeAccuracy; // DeLong's pseudoaccuracy for negative subjects

        // The real number of positives and negatives in the actual (true) data
        private int m_positiveCount;
        private int m_negativeCount;

        // The values which represent positive and negative values in our
        //  actual data (such as presence or absence of some disease)
        private double m_dtrueValue;
        private double m_dfalseValue;

        // The minimum and maximum values in the prediction data (such
        // as categorical rankings collected from test subjects)
        private double m_min;
        private double m_max;


        // The cm_points to hold our curve point information
        //Confusion matrix points for each value of threshold
        private ROCPoints cm_points;

        public ROCPoints ROCCollection { get { return cm_points; } }
        public double Area { get { return m_area; }}
        public double Error { get { return m_error; } }

        public double PositiveLabel { get { return m_dtrueValue; } }
        public double NegativeLabel { get { return m_dfalseValue; } }

        /// <summary>
        /// main constructor
        /// </summary>
        /// <param name="expected"></param>
        /// <param name="prediction"></param>
        public ROC(double[] expected, double[] prediction)
        {
            // Initial argument checking
            if (expected.Length != prediction.Length)
                throw new ArgumentException("The size of the expected and prediction arrays must match.");
            this.m_expected = new double[expected.Length];
            this.m_prediction = new double[prediction.Length];
            //
            Array.Copy(expected, this.m_expected, prediction.Length);
            Array.Copy(prediction, this.m_prediction, prediction.Length);

            initialize();
        }


        /// <summary>
        /// calculate ROC curve for given number of points
        /// </summary>
        /// <param name="ptRoc"></param>
        public void Calculate(double ptRoc)
        {
            var step = (m_max - m_min) / ptRoc;
            var points = new List<ConfusionMatrix>();
            double cutoff;

            // Create the curve, computing a point for each cutoff value
            for (cutoff = m_min; cutoff <= m_max; cutoff += step)
                points.Add(ComputePoint(cutoff));


            // Sort the curve by descending specificity
            points.Sort(new Comparison<ConfusionMatrix>(order));
            var last = points[points.Count - 1];
            if (last.FalsePositiveRate != 0.0 || last.Sensitivity != 0.0)
                points.Add(ComputePoint(Double.PositiveInfinity));
            

            // Create the point cm_points
            this.cm_points = new ROCPoints(points.ToArray());

            // Calculate area and error associated with this curve
            this.m_area = calculateAreaUnderCurve();
            this.m_error = calculateStandardError();
        }

        /// <summary>
        /// computes points for given threshold
        /// </summary>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public ConfusionMatrix ComputePoint(double threshold)
        {
            int truePositives = 0;
            int trueNegatives = 0;

            for (int i = 0; i < this.m_expected.Length; i++)
            {
                bool actual = (this.m_expected[i] == m_dtrueValue);
                bool predicted = (this.m_prediction[i] >= threshold);


                // If the prediction equals the true measured value
                if (predicted == actual)
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (predicted == true)
                        truePositives++; // Positive hit
                    else trueNegatives++;// Negative hit
                }
            }

            // The other values can be computed from available variables
            int falsePositives = m_negativeCount - trueNegatives;
            int falseNegatives = m_positiveCount - truePositives;

            var cm = new ConfusionMatrix(
                truePositives: truePositives, falseNegatives: falseNegatives,
                falsePositives: falsePositives, trueNegatives: trueNegatives);

            cm.SetCutOffValue(threshold);

            return cm;
        }

        /// <summary>
        /// Pepares data to show
        /// </summary>
        /// <param name="zedModel"></param>
        /// <param name="includeRandom"></param>
        public void GetData(out double[] x, out double[] y)
        {
            x = cm_points.GetOneMinusSpecificity();
            y = cm_points.GetSensitivity();

            return;   
        }


        /// <summary>
        /// Initialize ROC curve by calculating variables
        /// </summary>
        private void initialize()
        {

            // Determine which numbers correspond to each binary category
            m_dtrueValue = m_dfalseValue = m_expected[0];
            for (int i = 1; i < m_expected.Length; i++)
            {
                if (m_dtrueValue < m_expected[i]) m_dtrueValue = m_expected[i];
                if (m_dfalseValue > m_expected[i]) m_dfalseValue = m_expected[i];
            }

            // Count the real number of positive and negative cases
            for (int i = 0; i < m_expected.Length; i++)
            {
                if (m_expected[i] == m_dtrueValue)
                    this.m_positiveCount++;
            }

            m_min = m_prediction.Min();
            m_max = m_prediction.Max();

            // Negative cases is just the number of cases minus the number of positives
            this.m_negativeCount = this.m_expected.Length - this.m_positiveCount;

            // Get ratings for true positives
            int[] positiveIndices = getIndicesOfValue(m_expected, m_dtrueValue);
            double[] X = getValuesFromIndices(m_prediction, positiveIndices);

            int[] negativeIndices = getIndicesOfValue(m_expected, m_dfalseValue);
            double[] Y = getValuesFromIndices(m_prediction, negativeIndices);

            m_positiveResults = X;
            m_negativeResults = Y;

            // cal culates accuracy
            m_positiveAccuracy = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                m_positiveAccuracy[i] = calculateProbab(X[i], Y);
            }
            //cal neg acurracy
            m_negativeAccuracy = new double[Y.Length];
            for (int i = 0; i < Y.Length; i++)
            {
                m_negativeAccuracy[i] = 1.0 - calculateProbab(Y[i], X);
            }

        }

        /// <summary>
        /// calculate probability for x on y set
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static double calculateProbab(double x, double[] y)
        {
            double sum = 0;
            for (int i = 0; i < y.Length; i++)
            {
                if (y[i] < x)
                    sum += 1;
                if (y[i] == x)
                    sum += 0.5;
            }

            return sum / y.Length;
        }

        /// <summary>
        /// returns values for a given array indices
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        private double[] getValuesFromIndices(double[] elements, int[] indices)
        {
            List<double> values = new List<double>();
            for (int i = 0; i < indices.Length; i++)
            {
                values.Add(elements[indices[i]]);

            }

            return values.ToArray();
        }

        /// <summary>
        /// returns indices of the array for specified condition
        /// </summary>
        /// <param name="expected"></param>
        /// <returns></returns>
        private int[] getIndicesOfValue(double[] elements, double value)
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == value)
                    indices.Add(i);

            }

            return indices.ToArray();
        }

        /// <summary>
        /// order confusion matrices
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int order(ConfusionMatrix a, ConfusionMatrix b)
        {
            // First order by descending specificity
            int c = a.Specificity.CompareTo(b.Specificity);

            if (c == 0) // then order by ascending sensitivity
                return -a.Sensitivity.CompareTo(b.Sensitivity);
            else return c;
        }


        /// <summary>
        ///   Calculates the area under the ROC curve using the trapezium method.
        /// </summary>
        /// <remarks>
        ///   The area under a ROC curve can never be less than 0.50. If the area is first calculated as
        ///   less than 0.50, the definition of abnormal will be reversed from a higher test value to a
        ///   lower test value.
        /// </remarks>
        private double calculateAreaUnderCurve()
        {
            double sum = 0.0;
            double tpz = 0.0;

            for (int i = 0; i < cm_points.Count - 1; i++)
            {
                // Obs: False Positive Rate = (1-specificity)
                tpz = cm_points[i].Sensitivity + cm_points[i + 1].Sensitivity;
                tpz = tpz * (cm_points[i].FalsePositiveRate - cm_points[i + 1].FalsePositiveRate) / 2.0;
                sum += tpz;
            }

            if (sum < 0.5)
                return 1.0 - sum;
            else return sum;
        }


        /// <summary>
        /// calculate Standardized Error
        /// </summary>
        /// <returns></returns>
        private double calculateStandardError()
        {
            double[] Vx = m_positiveAccuracy;
            double[] Vy = m_negativeAccuracy;

            double varVx = Vx.VarianceOfS();
            double varVy = Vy.VarianceOfS();

            return Math.Sqrt(varVx / Vx.Length + varVy / Vy.Length);
        }

        
    }
    public class ConfusionMatrix
    {

        //  2x2 confusion matrix
        //
        //         | a(TP)    b(FN) |
        //   A =   |                |
        //         | c(FP)    d(TN) |
        //

        private int truePositives;  // a
        private int falseNegatives; // b
        private int falsePositives; // c
        private int trueNegatives;  // d


        private double? chiSquare;



        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        public ConfusionMatrix(
            int truePositives, int falseNegatives,
            int falsePositives, int trueNegatives)
        {
            this.truePositives = truePositives;
            this.trueNegatives = trueNegatives;
            this.falsePositives = falsePositives;
            this.falseNegatives = falseNegatives;
        }

        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        public ConfusionMatrix(int[,] matrix)
        {
            this.truePositives = matrix[0, 0];
            this.falseNegatives = matrix[0, 1];

            this.falsePositives = matrix[1, 0];
            this.trueNegatives = matrix[1, 1];
        }

        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        /// <param name="predicted">The values predicted by the model.</param>
        /// <param name="expected">The actual, truth values from the data.</param>
        /// 
        public ConfusionMatrix(bool[] predicted, bool[] expected)
        {
            // Initial argument checking
            if (predicted == null)
                throw new ArgumentNullException("predicted");
            if (expected == null)
                throw new ArgumentNullException("expected");
            if (predicted.Length != expected.Length)
                throw new Exception("The size of the predicted and expected arrays must match.");


            // For each of the predicted values,
            for (int i = 0; i < predicted.Length; i++)
            {
                bool prediction = predicted[i];
                bool expectation = expected[i];


                // If the prediction equals the true measured value
                if (expectation == prediction)
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (prediction == true)
                    {
                        truePositives++; // Positive hit
                    }
                    else
                    {
                        trueNegatives++; // Negative hit
                    }
                }
                else
                {
                    // We have a miss. Now we have to see
                    //  if it was a positive or negative miss
                    if (prediction == true)
                    {
                        falsePositives++; // Positive hit
                    }
                    else
                    {
                        falseNegatives++; // Negative hit
                    }
                }
            }
        }

        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        /// <param name="predicted">The values predicted by the model.</param>
        /// <param name="expected">The actual, truth values from the data.</param>
        /// <param name="positiveValue">The integer label which identifies a value as positive.</param>
        /// 
        public ConfusionMatrix(int[] predicted, int[] expected, int positiveValue = 1)
        {
            // Initial argument checking
            if (predicted == null) throw new ArgumentNullException("predicted");
            if (expected == null) throw new ArgumentNullException("expected");
            if (predicted.Length != expected.Length)
                throw new Exception("The size of the predicted and expected arrays must match.");


            for (int i = 0; i < predicted.Length; i++)
            {
                bool prediction = predicted[i] == positiveValue;
                bool expectation = expected[i] == positiveValue;


                // If the prediction equals the true measured value
                if (expectation == prediction)
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (prediction == true)
                    {
                        truePositives++; // Positive hit
                    }
                    else
                    {
                        trueNegatives++; // Negative hit
                    }
                }
                else
                {
                    // We have a miss. Now we have to see
                    //  if it was a positive or negative miss
                    if (prediction == true)
                    {
                        falsePositives++; // Positive hit
                    }
                    else
                    {
                        falseNegatives++; // Negative hit
                    }
                }
            }

        }


        /// <summary>
        ///   Constructs a new Confusion Matrix.
        /// </summary>
        /// 
        /// <param name="predicted">The values predicted by the model.</param>
        /// <param name="expected">The actual, truth values from the data.</param>
        /// <param name="positiveValue">The integer label which identifies a value as positive.</param>
        /// <param name="negativeValue">The integer label which identifies a value as negative.</param>
        /// 
        public ConfusionMatrix(int[] predicted, int[] expected, int positiveValue, int negativeValue)
        {
            // Initial argument checking
            if (predicted == null) throw new ArgumentNullException("predicted");
            if (expected == null) throw new ArgumentNullException("expected");
            if (predicted.Length != expected.Length)
                throw new Exception("The size of the predicted and expected arrays must match.");


            for (int i = 0; i < predicted.Length; i++)
            {

                // If the prediction equals the true measured value
                if (predicted[i] == expected[i])
                {
                    // We have a hit. Now we have to see
                    //  if it was a positive or negative hit
                    if (predicted[i] == positiveValue)
                    {
                        truePositives++; // Positive hit
                    }
                    else if (predicted[i] == negativeValue)
                    {
                        trueNegatives++; // Negative hit
                    }
                }
                else
                {
                    // We have a miss. Now we have to see
                    //  if it was a positive or negative miss
                    if (predicted[i] == positiveValue)
                    {
                        falsePositives++; // Positive hit
                    }
                    else if (predicted[i] == negativeValue)
                    {
                        falseNegatives++; // Negative hit
                    }
                }
            }
        }

        public void SetCutOffValue(double value)
        {
            cutoff = value;
        }
        // Discrimination threshold (cutoff value)
        private double cutoff;

        /// <summary>
        ///   Gets the cutoff value (discrimination threshold) for this point.
        /// </summary>
        /// 
        public double Cutoff
        {
            get { return cutoff; }
        }

        /// <summary>
        ///   Gets the confusion matrix in count matrix form.
        /// </summary>
        /// 
        /// <remarks>
        ///   The table is listed as true positives, false negatives
        ///   on its first row, false positives and true negatives in
        ///   its second row.
        /// </remarks>
        /// 
        public int[,] Matrix
        {
            get
            {
                return new int[,]
                {
                    { truePositives, falseNegatives },
                    { falsePositives, trueNegatives },
                };
            }
        }

        /// <summary>
        ///   Gets the marginal sums for table rows.
        /// </summary>
        /// 
        /// <value>
        ///   Returns a vector with the sum of true positives and 
        ///   false negatives on its first position, and the sum
        ///   of false positives and true negatives on the second.
        /// </value>
        /// 
        public int[] RowTotals
        {
            get
            {
                return new int[]
                {
                    truePositives + falseNegatives, // ActualPositives
                    falsePositives + trueNegatives, // ActualNegatives
                };
            }
        }

        /// <summary>
        ///   Gets the marginal sums for table columns.
        /// </summary>
        /// 
        /// <value>
        ///   Returns a vector with the sum of true positives and
        ///   false positives on its first position, and the sum
        ///   of false negatives and true negatives on the second.
        /// </value>
        /// 
        public int[] ColumnTotals
        {
            get
            {
                return new int[]
                {
                    truePositives + falsePositives, // PredictedPositives
                    falseNegatives + trueNegatives, // PredictedNegatives
                };
            }
        }


        /// <summary>
        ///   Gets the number of observations for this matrix
        /// </summary>
        /// 
        public int Samples
        {
            get
            {
                return trueNegatives + truePositives +
                    falseNegatives + falsePositives;
            }
        }

        /// <summary>
        ///   Gets the number of actual positives.
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of positives cases can be computed by
        ///   taking the sum of true positives and false negatives.
        /// </remarks>
        /// 
        public int ActualPositives
        {
            get { return truePositives + falseNegatives; }
        }

        /// <summary>
        ///   Gets the number of actual negatives
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of negatives cases can be computed by
        ///   taking the sum of true negatives and false positives.
        /// </remarks>
        /// 
        public int ActualNegatives
        {
            get { return trueNegatives + falsePositives; }
        }

        /// <summary>
        ///   Gets the number of predicted positives.
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of cases predicted as positive by the
        ///   test. This value can be computed by adding the
        ///   true positives and false positives.
        /// </remarks>
        /// 
        public int PredictedPositives
        {
            get { return truePositives + falsePositives; }
        }

        /// <summary>
        ///   Gets the number of predicted negatives.
        /// </summary>
        /// 
        /// <remarks>
        ///   The number of cases predicted as negative by the
        ///   test. This value can be computed by adding the
        ///   true negatives and false negatives.
        /// </remarks>
        /// 
        public int PredictedNegatives
        {
            get { return trueNegatives + falseNegatives; }
        }

        /// <summary>
        ///   Cases correctly identified by the system as positives.
        /// </summary>
        /// 

        public int TruePositives
        {
            get { return truePositives; }
        }

        /// <summary>
        ///   Cases correctly identified by the system as negatives.
        /// </summary>
        /// 
        public int TrueNegatives
        {
            get { return trueNegatives; }
        }

        /// <summary>
        ///   Cases incorrectly identified by the system as positives.
        /// </summary>
        /// 
        public int FalsePositives
        {
            get { return falsePositives; }
        }

        /// <summary>
        ///   Cases incorrectly identified by the system as negatives.
        /// </summary>
        /// 
        public int FalseNegatives
        {
            get { return falseNegatives; }
        }

        /// <summary>
        ///   Sensitivity, also known as True Positive Rate
        /// </summary>
        /// 
        /// <remarks>
        ///   The Sensitivity is calculated as <c>TPR = TP / (TP + FN)</c>.
        /// </remarks>
        /// 
        public double Sensitivity
        {
            get
            {
                return (truePositives == 0) ?
                    0 : (double)truePositives / (truePositives + falseNegatives);
            }
        }

        /// <summary>
        ///   Specificity, also known as True Negative Rate
        /// </summary>
        /// 
        /// <remarks>
        ///   The Specificity is calculated as <c>TNR = TN / (FP + TN)</c>.
        ///   It can also be calculated as: <c>TNR = (1-False Positive Rate)</c>.
        /// </remarks>
        /// 
        public double Specificity
        {
            get
            {
                return (trueNegatives == 0) ?
                    0 : (double)trueNegatives / (trueNegatives + falsePositives);
            }
        }

        /// <summary>
        ///  Efficiency, the arithmetic mean of sensitivity and specificity
        /// </summary>
        /// 
        public double Efficiency
        {
            get { return (Sensitivity + Specificity) / 2.0; }
        }

        /// <summary>
        ///   Accuracy, or raw performance of the system
        /// </summary>
        /// 
        /// <remarks>
        ///   The Accuracy is calculated as 
        ///   <c>ACC = (TP + TN) / (P + N).</c>
        /// </remarks>
        /// 
        public double Accuracy
        {
            get { return 1.0 * (truePositives + trueNegatives) / Samples; }
        }

        /// <summary>
        ///  Prevalence of outcome occurrence.
        /// </summary>
        /// 
        public double Prevalence
        {
            get { return ActualPositives / (double)Samples; }
        }

        /// <summary>
        ///   Positive Predictive Value, also known as Positive Precision
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The Positive Predictive Value tells us how likely is 
        ///   that a patient has a disease, given that the test for
        ///   this disease is positive.</para>
        /// <para>
        ///   The Positive Predictive Rate is calculated as
        ///   <c>PPV = TP / (TP + FP)</c>.</para>
        /// </remarks>
        /// 
        public double PositivePredictiveValue
        {
            get
            {
                double f = truePositives + FalsePositives;

                if (f != 0)
                    return truePositives / f;

                return 1.0;
            }
        }

        /// <summary>
        ///   Negative Predictive Value, also known as Negative Precision
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The Negative Predictive Value tells us how likely it is
        ///   that the disease is NOT present for a patient, given that
        ///   the patient's test for the disease is negative.</para>
        /// <para>
        ///   The Negative Predictive Value is calculated as 
        ///   <c>NPV = TN / (TN + FN)</c>.</para> 
        /// </remarks>
        /// 

        public double NegativePredictiveValue
        {
            get
            {
                double f = (trueNegatives + falseNegatives);

                if (f != 0)
                    return trueNegatives / f;

                return 1.0;
            }
        }


        /// <summary>
        ///   False Positive Rate, also known as false alarm rate.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The rate of false alarms in a test.</para>
        /// <para>
        ///   The False Positive Rate can be calculated as
        ///   <c>FPR = FP / (FP + TN)</c> or <c>FPR = (1-specificity)</c>.
        /// </para>
        /// </remarks>
        /// 

        public double FalsePositiveRate
        {
            get
            {
                return (double)falsePositives / (falsePositives + trueNegatives);
            }
        }

        /// <summary>
        ///   False Discovery Rate, or the expected false positive rate.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>
        ///   The False Discovery Rate is actually the expected false positive rate.</para>
        /// <para>
        ///   For example, if 1000 observations were experimentally predicted to
        ///   be different, and a maximum FDR for these observations was 0.10, then
        ///   100 of these observations would be expected to be false positives.</para>
        /// <para>
        ///   The False Discovery Rate is calculated as
        ///   <c>FDR = FP / (FP + TP)</c>.</para>
        /// </remarks>
        /// 

        public double FalseDiscoveryRate
        {
            get
            {
                double d = falsePositives + truePositives;

                if (d != 0.0)
                    return falsePositives / d;

                return 1.0;
            }
        }

        /// <summary>
        ///   Matthews Correlation Coefficient, also known as Phi coefficient 
        /// </summary>
        /// 
        /// <remarks>
        ///   A coefficient of +1 represents a perfect prediction, 0 an
        ///   average random prediction and −1 an inverse prediction.
        /// </remarks>
        /// 

        public double MatthewsCorrelationCoefficient
        {
            get
            {
                double tp = truePositives;
                double tn = trueNegatives;
                double fp = falsePositives;
                double fn = falseNegatives;

                double den = System.Math.Sqrt((tp + fp) * (tp + fn) * (tn + fp) * (tn + fn));

                if (den == 0.0)
                    return 0;

                double num = (tp * tn) - (fp * fn);

                return num / den;
            }
        }


        /// <summary>
        ///   Odds-ratio.
        /// </summary>
        /// 
        /// <remarks>
        ///   References: http://www.iph.ufrgs.br/corpodocente/marques/cd/rd/presabs.htm
        /// </remarks>
        /// 

        public double OddsRatio
        {
            get
            {
                return (double)(truePositives * trueNegatives) / (falsePositives * falseNegatives);
            }
        }

        /// <summary>
        ///   Kappa coefficient.
        /// </summary>
        ///
        /// <remarks>
        ///   References: http://www.iph.ufrgs.br/corpodocente/marques/cd/rd/presabs.htm
        /// </remarks>
        ///

        public double Kappa
        {
            get
            {
                double a = truePositives;
                double b = falsePositives;
                double c = falseNegatives;
                double d = trueNegatives;
                double N = Samples;

                return (double)((a + d) - (((a + c) * (a + b) + (b + d) * (c + d)) / N))
                    / (N - (((a + c) * (a + b) + (b + d) * (c + d)) / N));
            }
        }



        /// <summary>
        ///   Diagnostic power.
        /// </summary>
        /// 

        public double OverallDiagnosticPower
        {
            get { return (double)(falsePositives + trueNegatives) / Samples; }
        }

        /// <summary>
        ///   Normalized Mutual Information.
        /// </summary>
        /// 

        public double NormalizedMutualInformation
        {
            get
            {
                double a = truePositives;
                double b = falsePositives;
                double c = falseNegatives;
                double d = trueNegatives;
                double N = Samples;

                double num = a * Math.Log(a) + b * Math.Log(b) + c * Math.Log(c) + d * Math.Log(d)
                           - (a + b) * Math.Log(a + b) - (c + d) * Math.Log(c + d);

                double den = N * Math.Log(N) - ((a + c) * Math.Log(a + c) + (b + d) * Math.Log(b + d));

                return 1 + num / den;
            }
        }

        /// <summary>
        ///   Precision, same as the <see cref="PositivePredictiveValue"/>.
        /// </summary>
        /// 
        public double Precision
        {
            get { return PositivePredictiveValue; }
        }

        /// <summary>
        ///   Recall, same as the <see cref="Sensitivity"/>.
        /// </summary>
        /// 
        public double Recall
        {
            get { return Sensitivity; }
        }

        /// <summary>
        ///   F-Score, computed as the harmonic mean of
        ///   <see cref="Precision"/> and <see cref="Recall"/>.
        /// </summary>
        /// 

        public double FScore
        {
            get { return 2.0 * (Precision * Recall) / (Precision + Recall); }
        }

        /// <summary>
        ///   Expected values, or values that could
        ///   have been generated just by chance.
        /// </summary>
        /// 

        public double[,] ExpectedValues
        {
            get
            {
                var row = RowTotals;
                var col = ColumnTotals;

                var expected = new double[2, 2];

                for (int i = 0; i < row.Length; i++)
                    for (int j = 0; j < col.Length; j++)
                        expected[i, j] = (col[j] * row[i]) / (double)Samples;

                return expected;
            }
        }

        /// <summary>
        ///   Gets the Chi-Square statistic for the contingency table.
        /// </summary>
        /// 

        public double ChiSquare
        {
            get
            {
                if (chiSquare == null)
                {
                    var row = RowTotals;
                    var col = ColumnTotals;

                    double x = 0;
                    for (int i = 0; i < row.Length; i++)
                    {
                        for (int j = 0; j < col.Length; j++)
                        {
                            double e = (row[i] * col[j]) / (double)Samples;
                            double o = Matrix[i, j];

                            x += ((o - e) * (o - e)) / e;
                        }
                    }

                    chiSquare = x;
                }

                return chiSquare.Value;
            }
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> representing this confusion matrix.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> representing this confusion matrix.
        /// </returns>
        /// 
        public override string ToString()
        {
            return String.Format(System.Globalization.CultureInfo.CurrentCulture,
                "TP:{0} FP:{2}, FN:{3} TN:{1}",
                truePositives, trueNegatives, falsePositives, falseNegatives);
        }

        /// <summary>
        ///   Converts this matrix into a <see cref="GeneralConfusionMatrix"/>.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="GeneralConfusionMatrix"/> with the same contents as this matrix.
        /// </returns>
        /// 
        //public GeneralConfusionMatrix ToGeneralMatrix()
        //{
        //    // Create a new matrix assuming negative instances 
        //    // are class 0, and positive instances are class 1.

        //    int[,] matrix =
        //    {
        //        //   class 0          class 1
        //        { trueNegatives,  falsePositives }, // class 0
        //        { falseNegatives, truePositives  }, // class 1
        //    };

        //    return new GeneralConfusionMatrix(matrix);
        //}

        /// <summary>
        ///   Combines several confusion matrices into one single matrix.
        /// </summary>
        /// 
        /// <param name="matrices">The matrices to combine.</param>
        /// 
        public static ConfusionMatrix Combine(params ConfusionMatrix[] matrices)
        {
            if (matrices == null) throw new ArgumentNullException("matrices");
            if (matrices.Length == 0) throw new ArgumentException("At least one confusion matrix is required.");

            int[,] total = new int[2, 2];

            foreach (var matrix in matrices)
            {
                for (int j = 0; j < 2; j++)
                    for (int k = 0; k < 2; k++)
                        total[j, k] += matrix.Matrix[j, k];
            }

            return new ConfusionMatrix(total);
        }
    }

    public class ROCPoints : ReadOnlyCollection<ConfusionMatrix>
    {
        internal ROCPoints(ConfusionMatrix[] points)
            : base(points) { }

        /// <summary>
        ///   Gets the (1-specificity, sensitivity) values as (x,y) coordinates.
        /// </summary>
        /// 
        /// <returns>
        ///   An jagged double array where each element is a double[] vector
        ///   with two positions; the first is the value for 1-specificity (x)
        ///   and the second the value for sensitivity (y).
        /// </returns>
        /// 
        public double[][] GetXYValues()
        {
            double[] x = new double[this.Count];
            double[] y = new double[this.Count];

            for (int i = 0; i < Count; i++)
            {
                x[i] = 1.0 - this[i].Specificity;
                y[i] = this[i].Sensitivity;
            }

            double[][] points = new double[this.Count][];
            for (int i = 0; i < points.Length; i++)
                points[i] = new[] { x[i], y[i] };

            return points;
        }

        /// <summary>
        ///   Gets an array containing (1-specificity) 
        ///   values for each point in the curve.
        /// </summary>
        /// 
        public double[] GetOneMinusSpecificity()
        {
            double[] x = new double[this.Count];
            for (int i = 0; i < x.Length; i++)
                x[i] = 1.0 - this[i].Specificity;
            return x;
        }

        /// <summary>
        ///   Gets an array containing (sensitivity) 
        ///   values for each point in the curve.
        /// </summary>
        /// 
        public double[] GetSensitivity()
        {
            double[] y = new double[this.Count];
            for (int i = 0; i < y.Length; i++)
                y[i] = this[i].Sensitivity;
            return y;
        }

        /// <summary>
        ///   Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// 
        /// <returns>
        ///   A <see cref="System.String"/> that represents this instance.
        /// </returns>
        /// 
        public override string ToString()
        {
            return Count.ToString();
        }

    }
}
