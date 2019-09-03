using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Data
{
    public sealed class TrainingData
    {

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

    }
}
