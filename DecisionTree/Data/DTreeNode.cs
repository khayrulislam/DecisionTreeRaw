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
        public List<FeatureDataPair> previousFeatureValues{ get; set; }
        public string className{ get; set; }
        public bool isLeaf { get; set; }

        public int depth { get; set; }

        public DTreeNode()
        {
            this.isLeaf = false;
            this.previousFeatures = new List<string>();
            this.previousFeatureValues = new List<FeatureDataPair>();
            this.childrenNodes = new List<DTreeNode>();
            this.depth = 0;
        }
    }
}
