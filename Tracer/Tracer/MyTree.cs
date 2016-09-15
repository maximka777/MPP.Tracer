using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MyTree
    {
        private TreeNode root = null;

        public TreeNode Root { 
            get 
            { 
                return root; 
            } 
            set {
                root = value; 
            }
        }

        private IEnumerable<TreeNode> ClimbNode(TreeNode node)
        {
            if (node != null)
            {
                yield return node;
                foreach(TreeNode treeNode in node.ChildList){
                    foreach (TreeNode trNode in ClimbNode(treeNode))
                    {
                        yield return trNode;
                    }
                }
            }
            else
            {
                yield break;
            }
        }

        public IEnumerable<TreeNode> ClimbTree()
        {
            if (root != null)
            {
                foreach (TreeNode node in ClimbNode(root))
                {
                    yield return node;
                }
            }
            else{
                yield break;
            }
        }

        public void FindLastMethodInTree(ref TreeNode result)
        {
            foreach (TreeNode node in ClimbTree())
            {
                if((node.StopTime.Ticks == 0) && ((result == null) || (node.StartTime.CompareTo(result.StartTime) >= 0))){
                    result = node;
                }
            }
        }

        public void FindLastMethodByNameInTree(string methodName, ref TreeNode result)
        {
            foreach (TreeNode node in ClimbTree())
            {
                if ((node.MethodName == methodName) && (node.StopTime.Ticks == 0) && ((result == null) || (node.StartTime.CompareTo(result.StartTime) > 0)))
                {
                    result = node;
                }
            }
        }
    }
}
