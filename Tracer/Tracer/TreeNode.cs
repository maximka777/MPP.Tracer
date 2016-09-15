using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TreeNode
    {
        public string methodName;
        public DateTime startTime;
        public DateTime stopTime;
        public long totalTime;
        public int paramsCount;
        public string className;
        public List<TreeNode> childList = new List<TreeNode>();
        public int level;
    }
}
