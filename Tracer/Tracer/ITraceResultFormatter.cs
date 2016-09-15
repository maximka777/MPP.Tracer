using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal interface ITraceResultFormatter
    {
        void Format(TraceResult traceResult);
    }
}
