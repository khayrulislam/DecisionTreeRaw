using DecisionTree.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace DecisionTree.Tree
{
    public sealed class DTree
    {

        public static DTree treeInstance = null;

        public DTreeNode root { get; private set; }

        public void Storeroot(DTreeNode node)
        {
            this.root = node;
        }

        private DTree()
        {
        }

        public static DTree GetDTreeInstance
        {
            get
            {
                if(treeInstance == null)
                {
                    treeInstance = new DTree();
                }
                return treeInstance;
            }
        }

        // print tree
        public void PrintDecisionTree()
        {
            PrintTree(this.root,"",true);
        }

        private void PrintTree(DTreeNode currentNode, string indent, bool last)
        {
            string value = currentNode.spliteFeatureValue != "" ? "(" + currentNode.spliteFeatureValue + ") " : "" ;
            string ans = currentNode.className !="" ? " " +currentNode.className : "" ;

            Console.WriteLine(indent + "+-- "+ value + currentNode.spliteFeatureName + ans);
            indent += last ? "   " : "|  ";

            for (int i = 0; i < currentNode.childrenNodes.Count; i++)
    {
                PrintTree(currentNode.childrenNodes[i], indent, i == currentNode.childrenNodes.Count - 1);
            }
        }
    }
}
