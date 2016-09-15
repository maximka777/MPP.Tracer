using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Tracer
{
    public class TraceResult
    {
        public ConcurrentDictionary<int, MyTree> threadDictionary { get; set; }

        public TraceResult()
        {
            threadDictionary = new ConcurrentDictionary<int, MyTree>();
        }

        public void AddMethod(int threadID, MethodBase methodBase, DateTime startTime)
        {
            TreeNode treeNode = new TreeNode();
            treeNode.methodName = methodBase.Name;
            treeNode.paramsCount = methodBase.GetParameters().Count();
            treeNode.startTime = startTime;
            treeNode.className = methodBase.DeclaringType.ToString();

            if (threadDictionary.ContainsKey(threadID))
            {
                MyTree myTree = threadDictionary[threadID];
                TreeNode node = null;
                myTree.FindLastMethodInTree(ref node);
                treeNode.level = node.level + 1;
                node.childList.Add(treeNode);
            }
            else
            {
                MyTree myTree = new MyTree();
                threadDictionary.TryAdd(threadID, myTree);
                treeNode.level = 0;
                myTree.Root = treeNode;
            }
        }

        public void AddStopTimeToMethod(int threadID, MethodBase methodBase, DateTime stopTime){
            MyTree myTree = threadDictionary[threadID];
            TreeNode treeNode = null;
            myTree.FindLastMethodByNameInTree(methodBase.Name, ref treeNode);
            treeNode.stopTime = stopTime;
            treeNode.totalTime = (long)(stopTime.Subtract(treeNode.startTime).TotalMilliseconds);
        }
    }
}
