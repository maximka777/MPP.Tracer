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
            treeNode.MethodName = methodBase.Name;
            treeNode.ParamsCount = methodBase.GetParameters().Count();
            treeNode.StartTime = startTime;
            treeNode.ClassName = methodBase.DeclaringType.ToString();

            if (threadDictionary.ContainsKey(threadID))
            {
                MyTree myTree = threadDictionary[threadID];
                TreeNode node = null;
                myTree.FindLastMethodInTree(ref node);
                treeNode.Level = node.Level + 1;
                node.ChildList.Add(treeNode);
            }
            else
            {
                MyTree myTree = new MyTree();
                threadDictionary.TryAdd(threadID, myTree);
                treeNode.Level = 0;
                myTree.Root = treeNode;
            }
        }

        public void AddStopTimeToMethod(int threadID, MethodBase methodBase, DateTime stopTime){
            MyTree myTree = threadDictionary[threadID];
            TreeNode treeNode = null;
            myTree.FindLastMethodByNameInTree(methodBase.Name, ref treeNode);
            treeNode.StopTime = stopTime;
            treeNode.TotalTime = (long)(stopTime.Subtract(treeNode.StartTime).TotalMilliseconds);
        }
    }
}
