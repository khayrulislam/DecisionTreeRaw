using System;
using System.Collections.Generic;
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
        public List<FeatureValuePair> previousFeatureValues{ get; set; }
        public string className{ get; set; }
        public bool isLeaf { get; set; }

        public DTreeNode()
        {
            this.isLeaf = false;
            this.previousFeatures = new List<string>();
            this.previousFeatureValues = new List<FeatureValuePair>();
            this.childrenNodes = new List<DTreeNode>();
        }
    }
}
