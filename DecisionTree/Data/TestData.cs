using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Data
{
    public class TestData
    {
        public List<Dictionary<string, string>> testDataList = new List<Dictionary<string, string>>();
        private static TestData testDataInstance = null;
        private TestData() { }

        public static TestData GetTestDataInstance
        {
            get
            {
                if (testDataInstance == null)
                {
                    testDataInstance = new TestData();
                }
                return testDataInstance;
            }
        }

        public void StoreData(List<string[]> lines)
        {
            string[] featureRow = lines[0];

            for (int i= 1; i< lines.Count; i++)
            {
                Dictionary<string, string> input = new Dictionary<string, string>();

                for (int j = 0; j < lines[i].Length; j++)
                    input[featureRow[j]] = lines[i][j];
                this.testDataList.Add(input);
            }

        }
    }
}
