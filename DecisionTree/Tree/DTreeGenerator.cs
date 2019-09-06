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

        private static void generateTree(DTreeNode currentNode)
        {
            FeatureSelection featureSelection = new FeatureSelection();
            Feature feature = featureSelection.GetSplitingFeature(currentNode);

            //FeatureComputation featureInfo = GetSplitFeatureInfo(currentNode);
           // FeatureInfo featureInfo =  new FeatureInfo(null,null);
            if (feature == null) return;
            currentNode.entropy = feature.entropy;

            if(currentNode.entropy == 0.0)
            {
                currentNode.className = feature.className;
                currentNode.isLeaf = true;
                return;
            }
           
            currentNode.spliteFeatureName = feature.name;            
            currentNode.informationGain = feature.informationGain;

            Console.WriteLine(currentNode.spliteFeatureName);

            foreach (string child in feature.featureDistinctValueList)
            {
                DTreeNode childNode = new DTreeNode();
                childNode.spliteFeatureValue = child;

                List<string> preFeatures = currentNode.previousFeatures.ToList();
                preFeatures.Add(currentNode.spliteFeatureName);
                childNode.previousFeatures = preFeatures;

                List<FeatureDataPair> featureValuePairs = currentNode.previousFeatureValues.ToList();
                featureValuePairs.Add(new FeatureDataPair(currentNode.spliteFeatureName,childNode.spliteFeatureValue));
                childNode.previousFeatureValues = featureValuePairs;

                childNode.depth = currentNode.depth + 1;

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
