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
        public void Format(TraceResult traceResult)
        {
            XElement root = new XElement("root");
            foreach (int key in traceResult.threadDictionary.Keys)
            {
                XElement threadElement = new XElement("thread");
                threadElement.SetAttributeValue("id", key);
                root.Add(threadElement);
                foreach (TreeNode node in traceResult.threadDictionary[key].ClimbTree()){
                    XElement methodElement = new XElement("mathod");
                    methodElement.SetAttributeValue("methodName", node.methodName);
                    methodElement.SetAttributeValue("paramsCount", node.paramsCount);
                    methodElement.SetAttributeValue("totalTime", node.totalTime);
                    methodElement.SetAttributeValue("className", node.className);
                    threadElement.Add(methodElement);
                }
            }
            root.Save("xml.xml");
        }
    }
}
