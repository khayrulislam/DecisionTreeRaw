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


        public Feature GetSplitingFeature(DTreeNode currentNode)
        {
            double maxInformationGain = -1000000;
            Feature feature = null;

            List<string> remainFeatures = GetRemainingFeature( new List<string>(currentNode.previousFeatures) );

            foreach (string featureName in remainFeatures)
            {
                List<FeatureDataPair> featureDataPairs = new List<FeatureDataPair>(currentNode.previousFeatureValues) ;
                featureDataPairs.Add(new FeatureDataPair(featureName, null));

                string[][] dataInstances = trainingDataInstance.GetDataInstances(featureDataPairs);
                featureDataPairs.Clear();

                FeatureComputation featureComp = new FeatureComputation(featureName, dataInstances, remainFeatures.Count);
                featureComp.Execute();
                Feature tempFeature = featureComp.feature;

                if (tempFeature.informationGain > maxInformationGain)
                {
                    feature = tempFeature;
                    maxInformationGain = tempFeature.informationGain;
                }

            }
            return feature;
        }

        private List<string> GetRemainingFeature(List<string> previousFeatures)
        {
            return this.trainingDataInstance.GetFeatureList(previousFeatures);
        }
    }
}
