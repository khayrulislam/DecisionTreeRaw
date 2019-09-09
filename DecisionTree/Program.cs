using DecisionTree.Data;
using DecisionTree.FileRead;
using DecisionTree.Tree;
using System;
using System.Collections.Generic;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {

            DataReader reader = new DataReader();
            reader.ReadTrainingDataFile(@"..\..\..\InputFile\training-weather.txt");
            reader.ReadTestDataFile(@"..\..\..\InputFile\test-weather.txt");

            DTreeGenerator dtg = new DTreeGenerator();
            dtg.createDecisionTree();

           }
    }
}
