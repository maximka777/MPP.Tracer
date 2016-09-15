using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly Object syncObj = new Object();
        private TraceResult traceResult;

        private Tracer()
        {
            traceResult = new TraceResult();
        }

        public static Tracer Instance
        {
            get{
                if (instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new Tracer();
                        }
                    }
                }
                return instance;
            }
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            DateTime startTime = DateTime.Now;
            traceResult.AddMethod(Thread.CurrentThread.ManagedThreadId, methodBase, startTime);
        }

        public void StopTrace()
        {
            DateTime stopTime = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(1).GetMethod();
            traceResult.AddStopTimeToMethod(Thread.CurrentThread.ManagedThreadId, methodBase, stopTime);
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
