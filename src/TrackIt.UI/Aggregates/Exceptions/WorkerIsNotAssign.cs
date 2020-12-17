using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Aggregates.Exceptions
{
    public class WorkerIsNotAssign
    {

        [Serializable]
        public class WorkerIsNotAssignException : Exception
        {
            public WorkerIsNotAssignException() { }
            public WorkerIsNotAssignException(string message) : base(message) { }
            public WorkerIsNotAssignException(string message, Exception inner) : base(message, inner) { }
            protected WorkerIsNotAssignException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}