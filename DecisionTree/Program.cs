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
            //Console.WriteLine("Hello World!");
            //Console.ReadLine();

            DataReader reader = new DataReader();
            reader.ReadTrainingDataFile(@"..\..\..\InputFile\training-weather.txt");
            reader.ReadTestDataFile(@"..\..\..\InputFile\test-weather.txt");

            TrainingData td = TrainingData.GetTrainingDataInstance;


            DTreeGenerator dtg = new DTreeGenerator();
            dtg.createDecisionTree();


            /*
                        foreach (string clas in td.distinctClasses)
                        Console.WriteLine(clas);

                        });

                        for (int i = 0; i < res.Length; i++)
                        {
                            for (int j = 0; j < res[i].Length; j++) Console.Write(res[i][j] + " ");
                            Console.WriteLine();
                        }*/
            //string[][] res = td.GetDataInstances(new List<FeatureDataPair>() { new FeatureDataPair("outlook", "sunny") });
            //Console.WriteLine(td.GetFeatureList(new List<string>() { "temperature", "outlook", "humidity" })[0]) ;
        }
    }
}
