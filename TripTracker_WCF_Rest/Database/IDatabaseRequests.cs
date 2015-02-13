using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TripTracker_WCF_Rest.Database
{
    interface IDatabaseRequests
    {        
        OutcomeMsg SaveTrip(TripData trip);
        OutcomeMsg TestTrip(TripData trip);
        OutcomeMsg JsonTest(QuickTest quickTest);
        OutcomeMsg OpenConnTest();
    }
}