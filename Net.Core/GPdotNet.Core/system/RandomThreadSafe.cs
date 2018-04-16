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
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GPdotNet.Core
{
    /// <summary>
    /// Represents a thread-safe, pseudo-random number generator.
    /// </summary>
    public sealed class ThreadSafeRandom : Random, IDisposable
    {
       
        public void Dispose()
        {
            _local.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>The underlyin provider of randomness, one instance per thread, initialized with 
        /// </summary>
        private ThreadLocal<Random> _local = new ThreadLocal<Random>(() =>
        {
            return new Random((int)DateTime.Now.Ticks);
        });


        /// <summary>Returns a nonnegative random number.</summary>
        /// <returns>A 32-bit signed integer greater than or equal to zero and less than MaxValue.</returns>
        public override int Next()
        {
            return _local.Value.Next();
        }

        /// <summary>Returns a nonnegative random number less than the specified maximum.</summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to zero. 
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero, and less than maxValue; 
        /// that is, the range of return values ordinarily includes zero but not maxValue. However, 
        /// if maxValue equals zero, maxValue is returned.
        /// </returns>
        public override int Next(int maxValue)
        {
            return _local.Value.Next(maxValue);
        }

        /// <summary>Returns a random number within a specified range.</summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue; 
        /// that is, the range of return values includes minValue but not maxValue. 
        /// If minValue equals maxValue, minValue is returned.
        /// </returns>
        public override int Next(int minValue, int maxValue)
        {
            return _local.Value.Next(minValue, maxValue);
        }
        /// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public override void NextBytes(byte[] buffer)
        {
            _local.Value.NextBytes(buffer);
        }
        /// <summary>Returns a random number between 0.0 and 1.0.</summary>
        /// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
        public override double NextDouble()
        {
            return _local.Value.NextDouble();
        }
        public double NextDouble(double minValue, double maxValue)
        {
            return minValue + _local.Value.NextDouble() * (maxValue - minValue);
        }
    }
}
