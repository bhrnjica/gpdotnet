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

namespace GPdotNet.Interfaces
{
    public interface IParameters
    {
        Function RootFunctionNode { get; set; }

        //int MiniBatch { get; set; }
        float Threshold { get; set; }//cutoff value for binary classification problems
        int FeatureCount { get; set; }//random constants are not included
        bool IsProtectedOperation { get; set; }
        bool IsMultipleOutput { get; set; }//when SoftMax root node return more than one value
    }
}
