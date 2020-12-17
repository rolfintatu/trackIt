using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackIt.UI.Aggregates.Enums
{
    public enum DbState
    {
        Added = 4,
        Deleted = 8,
        Modified = 16
    }
}