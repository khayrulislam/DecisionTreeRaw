using DecisionTree.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Tree
{
    public class DTreeGenerator
    {
        private DTreeNode root;
        private TrainingData trainingDataInstance = null;
        public DTreeGenerator()
        {
            root = new DTreeNode();
            trainingDataInstance = TrainingData.GetTrainingDataInstance;
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

                calculateEntropy(dataInstance);

            }
        }

        private void calculateEntropy(string[][] dataInstance)
        {

        }
    }
}
