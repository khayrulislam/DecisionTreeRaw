using DecisionTree.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Tree
{
    public class DTreeGenerator
    {
        private DTreeNode root;
        private TrainingData trainingDataInstance = null;
        private List<string> distinctClassList = new List<string>();
        public DTreeGenerator()
        {
            root = new DTreeNode();
            trainingDataInstance = TrainingData.GetTrainingDataInstance;
            distinctClassList = trainingDataInstance.distinctClasses;
        }



        public void createDecisionTree()
        {
            GetSplitFeatureName(this.root);
        }

        private void GetSplitFeatureName(DTreeNode currentNode)
        {
            List<string> remainFeatures = trainingDataInstance.GetFeatureList(currentNode.previousFeatures);

            foreach (string feature in remainFeatures)
            {
                List<FeatureValuePair> previousFeatureValueList = currentNode.previousFeatureValues;
                previousFeatureValueList.Add(new FeatureValuePair(feature, null));
                string[][] dataInstance = trainingDataInstance.GetDataInstances(previousFeatureValueList);
                previousFeatureValueList.Clear();


                List<string> classList = new List<string>();
                foreach(string[] row in dataInstance)
                {
                    classList.Add(row[row.Length-1]);
                }
                calculateEntropy(classList);

            }
        }


        // get class list as input
        // count every class occurance
        // calculate entropy and return  
        private double calculateEntropy(List<string> classList)
        {
            double entropy = 0;
            double totalClass = classList.Count;
            List<double> classCount = Enumerable.Repeat(0.0,distinctClassList.Count).ToList();

            foreach (string classValue in classList)
            {
                classCount[this.distinctClassList.IndexOf(classValue)] += 1;
            }

            foreach(double singleClassCount in classCount)
            {
                double prob = ClassProbability(singleClassCount, totalClass);
                entropy += - prob * Math.Log( prob, 2);
            }

            return entropy;
        }

        private double ClassProbability(double singleClassCount, double totalClass)
        {
            return singleClassCount / totalClass;
        }
    }
}
