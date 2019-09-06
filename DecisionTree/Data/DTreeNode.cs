using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Data
{
    public class DTreeNode
    {
        public string spliteFeatureName { get; set; }
        public string spliteFeatureValue { get; set; }
        public double entropy { get; set; }
        public double informationGain { get; set; }
        public List<DTreeNode> childrenNodes { get; set; }
        public List<string> previousFeatures { get; set; }
        public List<FeatureDataPair> previousFeatureValues{ get; set; }
        public string className{ get; set; }
        public bool isLeaf { get; set; }
        public int depth { get; set; }

        private List<string[]> nodeDataInstance;

        private List<string> distinctClassList;

        private TrainingData trainingDataInstance;

        public DTreeNode()
        {
            this.isLeaf = false;
            this.previousFeatures = new List<string>();
            this.previousFeatureValues = new List<FeatureDataPair>();
            this.childrenNodes = new List<DTreeNode>();
            this.depth = 0;
        }

        public void ExecuteNode(List<string[]> nodeData)
        {
            this.nodeDataInstance = nodeData;
            this.trainingDataInstance = TrainingData.GetTrainingDataInstance;
            this.distinctClassList = this.trainingDataInstance.distinctClasses;
            this.entropy = calculateEntropy();
            // condition
            double childEntropy = createChildNodesAndGetChildEntropy();
            this.informationGain = this.entropy - childEntropy;
        }

        private double createChildNodesAndGetChildEntropy()
        {
            List<string> featureData = GetDistinctFeatureData();
            double childEntropy = 0;
            if (featureData.Count > 1)
            {
                foreach(string data in featureData)
                {
                    DTreeNode childNode = new DTreeNode();
                    childNode.spliteFeatureValue = data;
                    List<string[]> childData = GetChildNodeData(data);
                    childNode.ExecuteNode(childData);
                    this.childrenNodes.Add(childNode);


                    double factor = (double)childData.Count / this.nodeDataInstance.Count;

                    childEntropy += factor * childNode.entropy;

                }
            }
            return childEntropy;
        }

        private List<string[]> GetChildNodeData(string data)
        {
            List<string[]> dataList = new List<string[]>();

            foreach(string[] dataRow in this.nodeDataInstance)
            {
                if (data == dataRow[0]) dataList.Add(dataRow);
            }
            return dataList;
        }

        private List<string> GetDistinctFeatureData()
        {
            HashSet<string> distinctList = new HashSet<string>();

            foreach(string[] dataRow in this.nodeDataInstance)
            {
                distinctList.Add(dataRow[0]);
            }
            return distinctList.ToList();
        }

        // get class list as input
        // count every class occurance
        // calculate entropy and return  
        // if single class contain then entropy is zero
        private double calculateEntropy()
        {
            double entropy = 0;

            List<string> allClass = GetClassList();

            double totalClass = allClass.Count;

            List<double> classCount = GetClassCount(allClass);

            if (allClass.Distinct().ToList().Count > 1)
            {
                foreach (double singleClassCount in classCount)
                {
                    double prob = ClassProbability(singleClassCount, totalClass);
                    entropy += -prob * Math.Log(prob, 2);
                }
            }
            else
            {
                this.className = allClass[0];
                // leaf end
            }
            return entropy;
        }

        private List<string> GetClassList()
        {
            List<string> list = new List<string>();
            foreach (string[] row in this.nodeDataInstance)
            {
                list.Add(row[row.Length - 1]);
            }
            return list;
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
