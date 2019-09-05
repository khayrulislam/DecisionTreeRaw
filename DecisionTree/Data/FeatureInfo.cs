using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Data
{
    public class FeatureInfo
    {
        private string[][] featureDataInstance;
        public string name;
        public List<string> featureDistinctValueList = new List<string>();
        public double entropy;
        public double valueEntropy;
        public double informationGain;
        private List<string> distinctClassList = new List<string>();
        private TrainingData trainingDataInstance = null;


        public FeatureInfo(string featureName, string[][] dataInstance)
        {
            this.featureDataInstance = dataInstance;
            this.name = featureName;
            Execute();
        }

        public void Execute()
        {
            this.trainingDataInstance = TrainingData.GetTrainingDataInstance;
            this.distinctClassList = trainingDataInstance.distinctClasses;
            this.featureDistinctValueList = GetDistinctFeatureValue();
            this.entropy = calculateFeatureEntropy();
            this.valueEntropy = calculateFeatureEntropy();
            this.informationGain = this.entropy - this.valueEntropy;
        }

        // get distinct feature value for calculating entropy for every value
        // for distinct feature value get the class list, using class list calculate entropy
        // with every entropy multiply a factor and sum all the entropy
        private double calculateFeatureValueEntropy()
        {
            double factor, entropy = 0;
            List<string> classList = new List<string>();
            
            foreach (string featureValue in this.featureDistinctValueList)
            {
                foreach (string[] row in this.featureDataInstance)
                {
                    if (row[0] == featureValue) classList.Add(row[row.Length - 1]);
                }

                factor = (double) classList.Count / this.featureDataInstance.Length;
                entropy += factor * calculateEntropy(classList);
                classList.Clear();
            }
            return entropy;
        }

        private List<string> GetDistinctFeatureValue()
        {
            HashSet<string> distinctValue = new HashSet<string>();
            foreach (string[] row in this.featureDataInstance)
            {
                distinctValue.Add(row[0]);
            }
            return distinctValue.ToList();
        }

        // calculate a single features entropy
        private double calculateFeatureEntropy()
        {
            List<string> classList = new List<string>();
            foreach (string[] row in this.featureDataInstance)
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
            List<double> classCount = Enumerable.Repeat(0.0, distinctClassList.Count).ToList();

            if (classList.Distinct().ToList().Count > 1)
            {
                foreach (string classValue in classList)
                {
                    classCount[this.distinctClassList.IndexOf(classValue)] += 1;
                }

                foreach (double singleClassCount in classCount)
                {
                    double prob = ClassProbability(singleClassCount, totalClass);
                    entropy += - prob * Math.Log(prob, 2);
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
