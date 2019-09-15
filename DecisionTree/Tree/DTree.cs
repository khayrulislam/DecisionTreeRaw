using DecisionTree.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private static void PrintTree(DTreeNode currentNode, string indent, bool last)
        {
            string data = currentNode.spliteFeatureValue != null ? "--"+currentNode.spliteFeatureValue : "";
            string feature = currentNode.spliteFeatureName !=null ? "---[" + currentNode.spliteFeatureName + "]" : "";
            string ans = currentNode.className != null ? "---("+currentNode.className+")" : "";
            string entropy = currentNode.entropy != 0.0 ? "entropy=(" + currentNode.entropy + ")," : "";
            string ig = currentNode.informationGain != 0.0? "igain=("+currentNode.informationGain+")" : "";
            Console.WriteLine(indent + "+"+ data + feature + ans+ entropy + ig);
            indent += last ? " " : "|";
            for (int i = 0; i < data.Length+3; i++) indent += " ";

            for (int i = 0; i < currentNode.childrenNodes.Count; i++)
            {
                PrintTree(currentNode.childrenNodes[i], indent, i == currentNode.childrenNodes.Count - 1);
            }
        }

        public void TestExecute()
        {
            TestData testDataInstance = TestData.GetTestDataInstance;

            List<Dictionary<string, string>> testList = testDataInstance.testDataList;

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Original     Prediction");

            foreach(Dictionary<string, string> testInput in testList)
            {
                Console.WriteLine(testInput["Play"]+"             " +TraverseDTree(this.root,testInput));
            }
        }

        private static string TraverseDTree(DTreeNode currentNode, Dictionary<string, string> testInput)
        {
            if (currentNode.isLeaf)
            {
                return currentNode.className;
            }
            string feature = currentNode.spliteFeatureName;
            string data = testInput[feature];
            testInput.Remove(feature);

            foreach(DTreeNode childNode in currentNode.childrenNodes)
            {
                if (childNode.spliteFeatureValue == data) return TraverseDTree(childNode,testInput);
            }
            return null;
        }

    }
}
