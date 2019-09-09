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
        private DTree dTreeIstance = null;
        private List<string> distinctClassList = new List<string>();
        public DTreeGenerator()
        {
            root = new DTreeNode();
            trainingDataInstance = TrainingData.GetTrainingDataInstance;
            dTreeIstance = DTree.GetDTreeInstance;
            //distinctClassList = trainingDataInstance.distinctClasses;
        }

        public void createDecisionTree()
        {
            generateTree(this.root);
            dTreeIstance.Storeroot(this.root);
            dTreeIstance.PrintDecisionTree();
        }

        private static void generateTree(DTreeNode currentNode)
        {
            FeatureSelection featureSelection = new FeatureSelection();
            DTreeNode splitNode = featureSelection.GetSplitingFeature(currentNode);

            if (splitNode == null) return;
            currentNode.entropy = splitNode.entropy;

            if(currentNode.entropy == 0.0)
            {
                currentNode.className = splitNode.className;
                currentNode.isLeaf = true;
                return;
            }
           
            currentNode.spliteFeatureName = splitNode.spliteFeatureName;            
            currentNode.informationGain = splitNode.informationGain;

            


            foreach(DTreeNode child in splitNode.childrenNodes)
            {
                DTreeNode childNode = new DTreeNode();
                childNode.spliteFeatureValue = child.spliteFeatureValue;

                List<FeatureDataPair> featureValuePairs = currentNode.previousFeatureValues.ToList();
                featureValuePairs.Add(new FeatureDataPair(currentNode.spliteFeatureName, childNode.spliteFeatureValue));
                childNode.previousFeatureValues = featureValuePairs;

                childNode.depth = currentNode.depth + 1;

                currentNode.childrenNodes.Add(childNode);

                generateTree(childNode);
            }


        }
/*
        private FeatureComputation GetSplitFeatureInfo(DTreeNode currentNode)
        {
            double maxInformationGain = -1000000;
            FeatureComputation featureinfo = null;

            List<string> remainFeatures = trainingDataInstance.GetFeatureList(currentNode.previousFeatures);

            foreach (string feature in remainFeatures)
            {
                List<FeatureDataPair> previousFeatureValueList = currentNode.previousFeatureValues;
                previousFeatureValueList.Add(new FeatureDataPair(feature, null));
                string[][] dataInstance = trainingDataInstance.GetDataInstances(previousFeatureValueList);
                previousFeatureValueList.Clear();

                FeatureComputation info = new FeatureComputation(feature, dataInstance,remainFeatures.Count);
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
        }*/


        
    }
}
