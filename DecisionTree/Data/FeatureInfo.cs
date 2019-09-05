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
        public string className;
        public int featureRemainCount;
        public List<string> featureDistinctValueList = new List<string>();
        public Dictionary<string,string> featureValueClassMap = new Dictionary<string, string>();
        public double entropy;
        public double valueEntropy;
        public double informationGain;
        private List<string> distinctClassList = new List<string>();
        private TrainingData trainingDataInstance = null;


        public FeatureInfo(string featureName, string[][] dataInstance, int featureRemain)
        {
            this.featureDataInstance = dataInstance;
            this.name = featureName;
            this.featureRemainCount = featureRemain;
        }

        public void Execute()
        {
            this.trainingDataInstance = TrainingData.GetTrainingDataInstance;
            this.distinctClassList = trainingDataInstance.distinctClasses;
            this.featureDistinctValueList = GetDistinctFeatureValue();
            this.entropy = calculateFeatureEntropy();
            this.valueEntropy = calculateFeatureValueEntropy();
            this.informationGain = this.entropy - this.valueEntropy;
            //if(this.featureRemainCount==1) 
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
                if (this.featureRemainCount == 1) this.featureValueClassMap.Add(featureValue, GetClass(classList));

                factor = (double) classList.Count / this.featureDataInstance.Length;
                entropy += factor * calculateEntropy(classList);
                classList.Clear();
            }
            return entropy;
        }

        private string GetClass(List<string> classList)
        {
            double max = 0.0;
            string result = null;
            List<double> classCount = GetClassCount(classList);

            foreach(double count in classCount)
            {
                if(count > max)
                {
                    result = this.distinctClassList[classCount.IndexOf(count)];
                    max = count;
                }
            }

            return result;
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
            
            List<double> classCount = GetClassCount(classList);

            if (classList.Distinct().ToList().Count > 1)
            {
                /*foreach (string classValue in classList)
                {
                    classCount[this.distinctClassList.IndexOf(classValue)] += 1;
                }
*/
                foreach (double singleClassCount in classCount)
                {
                    double prob = ClassProbability(singleClassCount, totalClass);
                    entropy += - prob * Math.Log(prob, 2);
                }
            }
            else
            {
                this.className = classList[0];
            }
            return entropy;
        }

        private List<double> GetClassCount(List<string> classList)
        {
            List<double> classCount = Enumerable.Repeat(0.0, distinctClassList.Count).ToList();
            foreach (string classValue in classList)
            {
                classCount[this.distinctClassList.IndexOf(classValue)] += 1;
            }
            return classCount;
        }

        private double ClassProbability(double singleClassCount, double totalClass)
        {
            return singleClassCount / totalClass;
        }
    }
}
