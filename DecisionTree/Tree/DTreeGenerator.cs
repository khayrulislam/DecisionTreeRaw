using DecisionTree.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DecisionTree.Tree
{
    public class DTreeGenerator
    {
        private DTreeNode root;
        private DTree dTreeIstance = null;
        public DTreeGenerator()
        {
            root = new DTreeNode();
            dTreeIstance = DTree.GetDTreeInstance;
        }

        public void createDecisionTree()
        {
            generateTree(this.root);
            dTreeIstance.Storeroot(this.root);
            dTreeIstance.PrintDecisionTree();
            dTreeIstance.TestExecute();
        }

        private static void generateTree(DTreeNode currentNode)
        {
            FeatureSelection featureSelection = new FeatureSelection();
            DTreeNode splitNode = featureSelection.GetSplitingFeature(currentNode);

            if (splitNode == null) return;
            currentNode.entropy = splitNode.entropy;

            if(currentNode.entropy == 0.0)
            {
                currentNode.className = splitNode.className;
                currentNode.isLeaf = true;
                return;
            }
           
            currentNode.spliteFeatureName = splitNode.spliteFeatureName;            
            currentNode.informationGain = splitNode.informationGain;


            foreach(DTreeNode child in splitNode.childrenNodes)
            {
                DTreeNode childNode = new DTreeNode();
                childNode.spliteFeatureValue = child.spliteFeatureValue;

                List<FeatureDataPair> featureValuePairs = currentNode.previousFeatureValues.ToList();
                featureValuePairs.Add(new FeatureDataPair(currentNode.spliteFeatureName, childNode.spliteFeatureValue));
                childNode.previousFeatureValues = featureValuePairs;

                childNode.depth = currentNode.depth + 1;

                currentNode.childrenNodes.Add(childNode);

                generateTree(childNode);
            }


        }

        
    }
}
