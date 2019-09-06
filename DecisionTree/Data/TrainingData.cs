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
            //StoreFeatureNames(lines[0]);
            this.dataInstance = lines.Count-1;
            string[][] inputData = new string[this.dataInstance+1][];
            int i = 0 ;
            foreach(string[] dataRow in lines)
            {
                inputData[i] = dataRow;
                i++;
            }

            this.data = inputData;
            StoreDistinctClasses();
        }


        // store distinct class of the total data instance and count
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

        // get data row using feature and feature data
        public string[][] GetDataInstances(List<FeatureDataPair> featureDatas)
        {
            string[][] dataInstance = this.data;
            foreach (FeatureDataPair featureData in featureDatas)
            {
                dataInstance = GetFeatureDataClass(featureData, dataInstance);
            }
            return dataInstance;
        }

        // when get both feature name and feature value then return matched data row
        private string[][] GetFeatureDataClass(FeatureDataPair featureData, string[][] instances)
        {
            int featureIndex = Array.IndexOf(instances[0], featureData.featureName); 
            List<string[]> filterInstance = new List<string[]>();

            // store feature name row
            filterInstance.Add(instances[0]);

            // store the data row matched with feature data
            for (int i = 1; i < instances.Length; i++)
            {
                if (instances[i][featureIndex] == featureData.featureValue) filterInstance.Add(instances[i]);
            }

            return RemoveFeature(filterInstance, featureIndex).ToArray();
        }


        // remove specific feature column;
        private string[][] RemoveFeature(List<string[]> dataInstances, int featureIndex)
        {
            List<string[]> result = new List<string[]>();
            foreach (string[] dataRow in dataInstances)
            {
                List<string> row = dataRow.ToList();
                row.RemoveAt(featureIndex);
                result.Add(row.ToArray()) ;
            }
            return result.ToArray();
        }
    }
}
