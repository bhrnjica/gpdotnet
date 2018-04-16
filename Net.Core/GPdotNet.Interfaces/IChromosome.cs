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
using GPdotNet.BasicTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GPdotNet.Interfaces
{
    /// <summary>
    /// This interface should be derived when declaring any chromosome in the application.
    /// It contains declaration of standard methods for every chromosome in GPdotNET.
    /// </summary>
    public interface IChromosome : IComparable<IChromosome>
    {
        /// <summary>
        /// Chromosome's fintess value
        /// </summary>
        float Fitness { get; set; }

        /// <summary>
        /// Clone the chromosome
        /// </summary>
        IChromosome Clone(bool includeExtraData = true);

        /// <summary>
        /// Mutation operator
        /// </summary>
        void Mutate(int maxLevel, Function[] funSet, int[] terSet);
       

        /// <summary>
        /// Crossover operator
        /// </summary>
        void Crossover(IChromosome ch2, int maxLevels, Function[] funSet, int[] terSet);

    }
}
