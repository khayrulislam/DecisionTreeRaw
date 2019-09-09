using DecisionTree.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DecisionTree.FileRead
{
    public class DataReader
    {
        public DataReader() { }

        public void ReadTrainingDataFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string[]> allLine = new List<string[]>();

                foreach (string line in lines)
                {
                    string[] words = line.Split(',');
                    allLine.Add(words);
                }

                TrainingData instance = TrainingData.GetTrainingDataInstance;
                instance.StoreData(allLine);
            }
            else
            {
                Console.WriteLine("Training File not found!!!!");
            }

        }

        public void ReadTestDataFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                List<string[]> allLine = new List<string[]>();

                foreach (string line in lines)
                {
                    string[] words = line.Split(',');
                    allLine.Add(words);
                }

                TestData instance = TestData.GetTestDataInstance;
                instance.StoreData(allLine);
            }
            else
            {
                Console.WriteLine("Test File not found!!!!");
            }

        }

    }
}
