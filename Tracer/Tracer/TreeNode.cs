using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TreeNode
    {
        public TreeNode()
        {
            ChildList = new List<TreeNode>();
        }

        public string MethodName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public long TotalTime { get; set; }
        public int ParamsCount { get; set; }
        public string ClassName { get; set; }
        public List<TreeNode> ChildList { get; set; }
        public int Level { get; set; }
    }
}
