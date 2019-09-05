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
            Console.WriteLine(GetSplitFeatureName(this.root));
        }

        private FeatureInfo GetSplitFeatureName(DTreeNode currentNode)
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

                FeatureInfo info = new FeatureInfo(feature, dataInstance);
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
