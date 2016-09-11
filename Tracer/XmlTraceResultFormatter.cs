﻿using System;
using System.Xml.Linq;

namespace Tracer
{
    public sealed class XmlTraceResultFormatter : ITraceResultFormatter
    {
        public string FileName { get; }

        public XmlTraceResultFormatter(string fileName)
        {
            if (fileName == null) throw new ArgumentNullException(nameof(fileName));

            FileName = fileName;
        }

        public void Format(TraceResult traceResult)
        {
            if (traceResult == null) throw new ArgumentNullException(nameof(traceResult));

            var document = new XDocument();
            var root = new XElement("root");

            var headNodes = traceResult.HeadNodes;
            foreach (var threadId in headNodes.Keys)
            {
                var thread = new XElement("thread", 
                    new XAttribute("id", threadId));

                foreach (var node in headNodes[threadId].TopLevelNodes)
                {
                    AddNodesToElement(thread, node);
                }

                root.Add(thread);
            }

            document.Add(root);
            document.Save(FileName);
        }

        private static void AddNodesToElement(XContainer element, TraceResultNode node)
        {
            var timePresentation = $"{Math.Round(node.TracingTime.TotalMilliseconds)}ms";

            var method = new XElement("method",
                new XAttribute("name", node.MethodName),
                new XAttribute("time", timePresentation),
                new XAttribute("class", node.ClassName),
                new XAttribute("params", node.Parameters.Count));

            foreach (var internalNode in node.InternalNodes)
            {
                AddNodesToElement(method, internalNode);
            }

            element.Add(method);
        }
    }
}