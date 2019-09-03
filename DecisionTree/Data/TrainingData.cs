using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Data
{
    public sealed class TrainingData
    {

        private List<string> featureNames = new List<string>();

        private string[,] data;

        private static TrainingData trainingDataInstance = null;
        private TrainingData() { }

        public static TrainingData GetTrainingDataInstance
        {
            get
            {
                if (trainingDataInstance == null)
                {
                    trainingDataInstance = new TrainingData();
                }
                return trainingDataInstance;
            }
        }

        public void StoreData(List<string[]> lines)
        {
            StoreFeatureNames(lines[0]);
            
            for(int i = 1; i < lines.Count; i++)
            {
                int j = 0;
                foreach (string word in lines[i]) data[i - 1, j++] = word;
            }
        }

        private void StoreFeatureNames(string[] names)
        {
            foreach (string name in names) featureNames.Add(name);
        }
    }
}
