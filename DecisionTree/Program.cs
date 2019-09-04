using DecisionTree.Data;
using DecisionTree.FileRead;
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

            TrainingDataReader reader = new TrainingDataReader();
            reader.ReadDataFile(@"..\..\..\InputFile\training-weather.txt");

            TrainingData td = TrainingData.GetTrainingDataInstance;

/*            string[][] res = td.GetDataInstances(new List<FeatureValuePair>() { new FeatureValuePair("outlook","sunny"), new FeatureValuePair("temperature", null) });

            for (int i = 0; i < res.Length; i++)
            {
                for (int j = 0; j < res[i].Length; j++) Console.Write(res[i][j] + " ");
                Console.WriteLine();
            }*/

            //Console.WriteLine(td.GetFeatureList(new List<string>() { "temperature", "outlook", "humidity" })[0]) ;
        }
    }
}
