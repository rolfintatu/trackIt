using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Aggregates.Exceptions
{
    public class WorkerAlreadyAssigned
    {

        [Serializable]
        public class WorkerAlreadyAssignedException : Exception
        {
            public WorkerAlreadyAssignedException() { }
            public WorkerAlreadyAssignedException(string message) : base(message) { }
            public WorkerAlreadyAssignedException(string message, Exception inner) : base(message, inner) { }
            protected WorkerAlreadyAssignedException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}