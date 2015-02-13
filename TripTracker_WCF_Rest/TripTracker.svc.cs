using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Xml.Linq;
using TripTracker_WCF_Rest.Database;

namespace TripTracker_WCF_Rest
{
    public class TripTracker : ITripTracker
    {
        private IDatabaseRequests databaseAction;

        public TripTracker()
        {
            databaseAction = new DatabaseActions();
        }
     
        public OutcomeMsg OpenConnTest()
        {
            OutcomeMsg oMsg = databaseAction.OpenConnTest();
            return oMsg;
        }

        public OutcomeMsg ContactWebService()
        {
            OutcomeMsg oMsg = new OutcomeMsg("Web service contacted and responded");
            return oMsg;
        }

        public OutcomeMsg SaveTrip(TripData trip)
        {
            OutcomeMsg oMsg = databaseAction.SaveTrip(trip);          
            return oMsg;
        }

        public OutcomeMsg TestTrip(TripData trip)
        {
            OutcomeMsg oMsg = databaseAction.TestTrip(trip);
            return oMsg;
        }

        public OutcomeMsg JsonTest(QuickTest quickTest)
        {
            OutcomeMsg oMsg = databaseAction.JsonTest(quickTest);
            return oMsg;
        }
    }
}
