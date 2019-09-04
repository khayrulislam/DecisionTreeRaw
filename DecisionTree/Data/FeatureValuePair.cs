﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Data
{
    public class FeatureValuePair
    {

        public FeatureValuePair(string featureName, string featureValue)
        {
            this.featureName = featureName;
            this.featureValue = featureValue;
        }

        public string featureName { get; }

        public string featureValue { get; }
    }
}
