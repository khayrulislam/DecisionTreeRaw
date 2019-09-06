using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Data
{
    public class Feature
    {
        public string name { get; set; }
        public string className { get; set; }
        public int featureRemainCount { get; set; }
        public List<string> featureDistinctValueList { get; set; }
        public Dictionary<string, string> featureValueClassMap { get; set; }
        public double entropy { get; set; }
        public double valueEntropy { get; set; }
        public double informationGain { get; set; }


    }
}
