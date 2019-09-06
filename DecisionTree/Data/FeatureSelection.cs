using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Data
{
    public class FeatureSelection
    {

        TrainingData trainingDataInstance = null;

        public FeatureSelection()
        {
            this.trainingDataInstance = TrainingData.GetTrainingDataInstance;
        }


        public DTreeNode GetSplitingFeature(DTreeNode currentNode)
        {
            double maxInformationGain = -1000000;
            DTreeNode splitNode = null;
            string[][] currentOperationData = trainingDataInstance.GetDataInstances(currentNode.previousFeatureValues);

            List<string> featureList = GetFeatureList(currentOperationData[0]);

            foreach(string featureName in featureList)
            {
                List<string[]> featureData = GetFeatureData(currentOperationData, featureName);
                DTreeNode node = new DTreeNode();
                node.spliteFeatureName = featureData[0][0];
                featureData.RemoveAt(0);
                node.ExecuteNode(featureData);

                if (node.entropy == 0.0)
                {
                    splitNode = currentNode;
                    splitNode.entropy = node.entropy;

                    splitNode.className = node.className;
                }

                if(node.informationGain > maxInformationGain)
                {
                    maxInformationGain = node.informationGain;
                    splitNode = node;
                }

            }

            return splitNode;
        }

        private List<string[]> GetFeatureData(string[][] currentOperationData, string featureName)
        {
            int featureIndex = Array.IndexOf(currentOperationData[0], featureName);
            int ansIndex = currentOperationData[0].Length - 1;
            List<string[]> result = new List<string[]>();

            foreach(string[] dataRow in currentOperationData)
            {
                result.Add(new string[] { dataRow[featureIndex], dataRow[ansIndex] });
            }

            return result;
        }

        private List<string> GetFeatureList(string[] featureArray)
        {
            List<string> list = new List<string>(featureArray);
            list.RemoveAt(list.Count - 1);
            return list;
        }

        /*private List<string> GetRemainingFeature(List<string> previousFeatures)
        {
            return this.trainingDataInstance.GetFeatureList(previousFeatures);
        }*/
    }
}
