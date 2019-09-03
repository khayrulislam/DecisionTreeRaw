using DecisionTree.FileRead;
using System;

namespace DecisionTree
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Console.ReadLine();

            TrainingDataReader reader = new TrainingDataReader();
            reader.ReadDataFile(@"..\..\..\InputFile\training-weather.txt");

        }
    }
}
