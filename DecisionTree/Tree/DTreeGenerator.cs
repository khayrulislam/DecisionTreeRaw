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
            //distinctClassList = trainingDataInstance.distinctClasses;
        }

        public void createDecisionTree()
        {
            //Console.WriteLine(GetSplitFeatureInfo(this.root));

            generateTree(this.root);
        }

        private void generateTree(DTreeNode currentNode)
        {
            FeatureInfo featureInfo = GetSplitFeatureInfo(currentNode);

            currentNode.entropy = featureInfo.entropy;

            if(currentNode.entropy == 0.0)
            {
                currentNode.className = featureInfo.className;
                currentNode.isLeaf = true;
                return;
            }
           
            currentNode.spliteFeatureName = featureInfo.name;            
            currentNode.informationGain = featureInfo.informationGain;

            Console.WriteLine(currentNode.spliteFeatureName);

            foreach (string child in featureInfo.featureDistinctValueList)
            {
                DTreeNode childNode = new DTreeNode();
                childNode.spliteFeatureValue = child;

                List<string> preFeatures = currentNode.previousFeatures.ToList();
                preFeatures.Add(currentNode.spliteFeatureName);
                childNode.previousFeatures = preFeatures;

                List<FeatureValuePair> featureValuePairs = currentNode.previousFeatureValues.ToList();
                featureValuePairs.Add(new FeatureValuePair(currentNode.spliteFeatureName,childNode.spliteFeatureValue));
                childNode.previousFeatureValues = featureValuePairs;

                currentNode.childrenNodes.Add(childNode);
            }

            /*if (featureInfo.featureRemainCount == 1)
            {
                foreach (DTreeNode childNode in currentNode.childrenNodes)
                {
                    childNode.isLeaf = true;
                    childNode.className = featureInfo.featureValueClassMap[childNode.spliteFeatureName];
                }
                return;
            }*/

            foreach (DTreeNode childNode in currentNode.childrenNodes)
            {
                generateTree(childNode);
            }

        }

        private FeatureInfo GetSplitFeatureInfo(DTreeNode currentNode)
        {
            double maxInformationGain = -1000000;
            FeatureInfo featureinfo = null;

            List<string> remainFeatures = trainingDataInstance.GetFeatureList(currentNode.previousFeatures);

            foreach (string feature in remainFeatures)
            {
                List<FeatureValuePair> previousFeatureValueList = currentNode.previousFeatureValues;
                previousFeatureValueList.Add(new FeatureValuePair(feature, null));
                string[][] dataInstance = trainingDataInstance.GetDataInstances(previousFeatureValueList);
                previousFeatureValueList.Clear();

                FeatureInfo info = new FeatureInfo(feature, dataInstance,remainFeatures.Count);
                info.Execute();

                if(info.informationGain > maxInformationGain)
                {
                    featureinfo = info;
                    maxInformationGain = info.informationGain;
                }

                //Console.WriteLine(feature+"  "+ calculateFeatureEntropy(dataInstance));
                //Console.WriteLine(" feature value  "+ calculateFeatureValueEntropy(dataInstance));
                //calculateFeatureEntropy(dataInstance);
                //calculateFeatureValueEntropy(dataInstance);
               
            }
            return featureinfo;
        }


        
    }
}
