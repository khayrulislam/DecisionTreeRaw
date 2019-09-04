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

        }


    }
}
