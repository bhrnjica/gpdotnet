[![DOI](https://zenodo.org/badge/126142815.svg)](https://zenodo.org/badge/latestdoi/126142815)
[![license](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/bhrnjica/gpdotnet/blob/master/license.md)
![developed by](https://avatars3.githubusercontent.com/u/12556447?s=75&u=f2cd3be70373c9654b9d53a4f69ddfd7a8ed6596&v=4=)
# Latest release

* march, 11, 2019: [GPdotNET v5.1.11.03.2019](https://github.com/bhrnjica/gpdotnet/releases/tag/v5.1.11.03.2019) -brings bug-fix for exporting into R when validation data set is not defined #4. Random constance are show in Result page #3. 

* october, 12, 2018: [GPdotNET v5.1.12.10.2018](https://github.com/bhrnjica/gpdotnet/releases/tag/v5.1.12.10.2018) -brings bug-fix for ribbon popup button for save and save as commands, and other minor code changes. 

* may, 24, 2018: [GPdotNET v5.1.24.05.2018](https://github.com/bhrnjica/gpdotnet/releases/tag/v5.1.24.05.2018) -brings few bug-fixes recognized on non English PCs, which identified the localization issues with comma decimal symbol. 

* april, 16, 2018: [GPdotNET v5.1.16.04.2018](https://github.com/bhrnjica/gpdotnet/releases/tag/v5.1) - version 5.1 which brings few new features e.g.:encoding category columns, application improvements, ...

* april, 8, 2018: [GPdotNET v5.0 - Book Edition](https://github.com/bhrnjica/gpdotnet/releases/tag/BookRelease) - after a year of development, finally GPdoNET v5 is out along my new book [*Optimized Genetic Programming*](https://www.igi-global.com/book/optimized-genetic-programming-applications/195404) published by IGI-Global.

# Introduction
![GPdotNET](https://github.com/bhrnjica/gpdotnet/blob/master/Net/GPdotNET.Wnd.App/Images/gpLogo_start2.png)

GPdotNET v5 is an open source computer program for running tree based genetic programming. GPdotNET started at 2006 to be a simple command line tool for GP application in modelling, and 3 years later GPdotNET published as an open source project. 
This software version is GPdotNET v 5 specially developed for the book edition: Optimized Genetic Programming Applications: Emerging Research and Opportunities, published by IGI-Global and can be found at https://www.igi-global.com/book/optimized-genetic-programming-applications/195404. 

From this version the GPdotNET is developed to be GP-based computer program only, so the other non-related GP modules (e.g. GA and ANN related modules) have been removed and are going to be released as parts of other open source projects. So from the previous GPdotNET v4, several open source projects came out:

1. [GPdotNET v5 - Generic programming tool](http://github.com/bhrnjica/gpdotnet)
2. [ANNdotNET V1.0 - Deep neural network tool based on CNTK and suposed to be CNTK GUI tool](http://github.com/bhrnjica/anndotnet)
3. [ML Data Preparation Tool - small GUI tool in order to prepare txt data for machine learning](http://github.com/bhrnjica/mldatapreparationtool)

The previous version GPdotNET V4 has moved to http://github.com/bhrnjica/gpdotnetv4 and will be for backward compatibitlity only. 
 

# Getting Started
GPdotNET v5 is mostly Windows application with full GUI support for modelling, exporting models in diferent tools (Excel, R language and Wolrfam Mathematica) and saving group of models in one project file (*.gpa file format). Beside main version there is a GPdotNET console application which supposed to be as GPdotNET commald lline tool. It is developed on .NET Core, thus it will be running on any OS where .NET Core is supported. Further more the GPdotNET cmd line tool is fully compatible with GUI version, and any project saved in command line version will be posible to be opened in GPdotNET v5. In order to used GPdotNET cmd, you have to clone the repository and run within Visual Studio IDE.

# System requirements
Both GPdotNET v5 and GPdotNET cmd line tool have no any special software of hardware requiremens. It is based on .NET Framework 4.7.1 and .NET Core 2.0, and few Nuget packages which can be downloaded once the solution is opened in Visual Studio.
So in order to run GPdotNET the following prerequesities have to be installed:

1. NET Framework 4.7.1+
2. .NET Core 2.0+

# How to Install GPdotNET v5 binaries

1. Download the GPdotNET binaries from release section
2. Unzip downloaded zip file in new folder
3. Find GPdotNET.exe and run it. You can copy the file and pase it as shortcut on desktop or task bar.

# How to Install GPdotNET Excel AddIn
In order to open exported GP models based on SoftMax root node, additional Excel AddIn must be installed. The GPdotNET AddIn can be downloaded from the GPdotNET release package. The Excel AddIn can be downloaded separatly and it is completely is independent of the GPdotNET. The GPdotNET AddIn can be installed as ordinary Office AddIn from the AddIn option dialog.

# Tutorials and Samples
The GPdotNET Start page contains several precalculated models which can be opened and learn how the models are build and trained. Also on YoutTube there are severaly tutorials lessons how to basic stuff in GPdotNET. For deeper look in examples the Chapter 5 of the Book Optimized Genetic Programming Applications: Emerging Research and Opportunities, published by IGI-Global, explains in detailes some of the real world examples.

# YoutTube GPdotNET Tutorials
1. Introduction to GPdotNET (https://youtu.be/T47XVFUWU7g)
2. Lesson 1: What is GPdotNET and how to install it and train the first GP model(https://youtu.be/ZeZzpsvdil8)
3. Lesson 2: Using GPdotNET in data preparation to train regression model (https://youtu.be/D4tDbdZv630) 
4. Lesson 3: How to Load a custom data set and create GP model (https://youtu.be/d04olbXFY_A)
5. Lesson 4: Training models in GPdotNET (https://youtu.be/N9y8nuiuBk8)
6. Lesson 5: Exporting GPdotNET models (https://youtu.be/uO9P-NHXoqU)
7. Lesson 6: Handling with project and related models(https://youtu.be/qEN4Ka4-n6c) 
