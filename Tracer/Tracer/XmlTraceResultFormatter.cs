using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private string filePath;

        public XmlTraceResultFormatter(string filePath)
        {
            this.filePath = filePath;
        }
        
        public void Format(TraceResult traceResult)
        {
            XElement root = new XElement("root");
            foreach (int key in traceResult.threadDictionary.Keys)
            {
                XElement threadElement = new XElement("thread");
                threadElement.SetAttributeValue("id", key);
                root.Add(threadElement);
                foreach (TreeNode node in traceResult.threadDictionary[key].ClimbTree()){
                    XElement methodElement = new XElement("method");
                    methodElement.SetAttributeValue("methodName", node.MethodName);
                    methodElement.SetAttributeValue("paramsCount", node.ParamsCount);
                    methodElement.SetAttributeValue("totalTime", node.TotalTime);
                    methodElement.SetAttributeValue("className", node.ClassName);
                    threadElement.Add(methodElement);
                }
            }
            root.Save(filePath);
        }
    }
}
