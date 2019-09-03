using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DecisionTree.FileRead
{
    public class TrainingDataReader
    {
        public TrainingDataReader() { }

        public void ReadDataFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

            }
            else
            {
                Console.WriteLine("File not found!!!!");
            }

        }

    }
}
