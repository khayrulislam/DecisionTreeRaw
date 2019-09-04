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

        private string GetSplitFeatureName(DTreeNode currentNode)
        {
            double informationGain, maxInformationGain = -1000000;
            string splitFeature=null;

            List<string> remainFeatures = trainingDataInstance.GetFeatureList(currentNode.previousFeatures);

            foreach (string feature in remainFeatures)
            {
                List<FeatureValuePair> previousFeatureValueList = currentNode.previousFeatureValues;
                previousFeatureValueList.Add(new FeatureValuePair(feature, null));
                string[][] dataInstance = trainingDataInstance.GetDataInstances(previousFeatureValueList);
                previousFeatureValueList.Clear();

                informationGain = calculateFeatureEntropy(dataInstance) - calculateFeatureValueEntropy(dataInstance);

                if(informationGain > maxInformationGain)
                {
                    splitFeature = feature;
                    maxInformationGain = informationGain;
                }

                //Console.WriteLine(feature+"  "+ calculateFeatureEntropy(dataInstance));
                //Console.WriteLine(" feature value  "+ calculateFeatureValueEntropy(dataInstance));
                //calculateFeatureEntropy(dataInstance);
                //calculateFeatureValueEntropy(dataInstance);
               
            }
            return splitFeature;
        }

        // get distinct feature value for calculating entropy for every value
        // for distinct feature value get the class list, using class list calculate entropy
        // with every entropy multiply a factor and sum all the entropy
        private double calculateFeatureValueEntropy(string[][] dataInstance)
        {
            double factor,entropy=0;
            HashSet<string> distinctFeatureValue = new HashSet<string>();
            List<string> classList = new List<string>();
            foreach (string[] row in dataInstance)
            {
                distinctFeatureValue.Add(row[0]);
            }

            foreach(string featureValue in distinctFeatureValue)
            {
                foreach (string[] row in dataInstance)
                {
                    if(row[0] == featureValue) classList.Add(row[row.Length - 1]);
                }

                factor = (double) classList.Count / dataInstance.Length;
                entropy += factor * calculateEntropy(classList);
                classList.Clear();
            }
            return entropy;
        }

        // calculate a single features entropy
        private double calculateFeatureEntropy(string[][] dataInstance)
        {
            List<string> classList = new List<string>();
            foreach (string[] row in dataInstance)
            {
                classList.Add(row[row.Length - 1]);
            }
            return calculateEntropy(classList);
        }


        // get class list as input
        // count every class occurance
        // calculate entropy and return  
        // if single class contain then entropy is zero
        private double calculateEntropy(List<string> classList)
        {
            double entropy = 0;
            double totalClass = classList.Count;
            List<double> classCount = Enumerable.Repeat(0.0,distinctClassList.Count).ToList();

            if (classList.Distinct().ToList().Count > 1)
            {
                foreach (string classValue in classList)
                {
                    classCount[this.distinctClassList.IndexOf(classValue)] += 1;
                }

                foreach (double singleClassCount in classCount)
                {
                    double prob = ClassProbability(singleClassCount, totalClass);
                    entropy += -prob * Math.Log(prob, 2);
                }
            }
            return entropy;
        }

        private double ClassProbability(double singleClassCount, double totalClass)
        {
            return singleClassCount / totalClass;
        }
    }
}
