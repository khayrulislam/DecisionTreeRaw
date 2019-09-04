using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Data
{
    public sealed class TrainingData
    {

        private List<string> featureNames = new List<string>();
        private int featureCount;
        private int dataInstance;
        public int distinctClassCount;
        public List<string> distinctClasses = new List<string>();

        private string[][] data;

        private static TrainingData trainingDataInstance = null;
        private TrainingData() { }

        public static TrainingData GetTrainingDataInstance
        {
            get
            {
                if (trainingDataInstance == null)
                {
                    trainingDataInstance = new TrainingData();
                }
                return trainingDataInstance;
            }
        }

        //store training input data feature value and class result 
        public void StoreData(List<string[]> lines)
        {
            StoreFeatureNames(lines[0]);
            this.dataInstance = lines.Count - 1;
            string[][] inputData = new string[this.dataInstance][];

            for (int i = 1, j = 0; i < lines.Count ; i++ , j = 0)
            {
                inputData[i - 1] = new string[this.featureCount+1];
                foreach (string word in lines[i]) inputData[i - 1][ j++] = word;
            }

            this.data = inputData;
            StoreDistinctClasses();
        }

        private void StoreDistinctClasses()
        {
            HashSet<string> classList = new HashSet<string>();

            foreach(string[] row in this.data)
            {
                classList.Add(row[row.Length-1]);
            }

            this.distinctClasses = classList.ToList();
            this.distinctClassCount = this.distinctClasses.Count;
            
        }

        // store only the feature names in a list
        // remove the class name
        private void StoreFeatureNames(string[] names)
        {
            foreach (string name in names) this.featureNames.Add(name);
            this.featureNames.RemoveAt(featureNames.Count-1);
            this.featureCount = featureNames.Count;
        }

        // return feature list except the input feature list
        public List<string> GetFeatureList(List<string> previousFeatures)
        {
            return featureNames.Except(previousFeatures).ToList();
        }

        // return specific feature value list with their class
        // takes input as feature name and feature value
        // dataInstance are continiously changed on the featureVlaue
        public string[][] GetDataInstances(List<FeatureValuePair> featureValues)
        {
            string[][] dataInstance = data;
            foreach (FeatureValuePair featureValue in featureValues)
            {
                dataInstance = GetFeatureDataWithClass(featureValue, dataInstance);
            }
            return dataInstance;
        }
        // when get both feature name and feature value then return matched data row
        // for feature name return the feature value and class value
        private string[][] GetFeatureDataWithClass(FeatureValuePair featureValue, string[][] istances)
        {
            int featureIndex = this.featureNames.IndexOf(featureValue.featureName);
            string value = featureValue.featureValue;
            List<string[]> filterInstance = new List<string[]>();

            if(value != null)
            {
                for(int i = 0; i < this.dataInstance; i++)
                {
                    if (istances[i][featureIndex] == value)
                    {
                        filterInstance.Add(istances[i]);
                    }
                }
            }
            else
            {
                foreach(string[] data in istances)
                {
                    filterInstance.Add(new string[2] { data[featureIndex], data[data.Length-1]});
                }
            }

            return filterInstance.ToArray();
        }


    }
}
