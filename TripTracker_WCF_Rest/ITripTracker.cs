using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TripTracker_WCF_Rest
{
    [ServiceContract]
    public interface ITripTracker
    {
   
        // WebInvoke attribute is used to make POST, DELETE and PUT request in WCF REST service
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "SaveTrip")]
        OutcomeMsg SaveTrip(TripData trip);

        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "TestTrip")]
        OutcomeMsg TestTrip(TripData trip);

        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "OpenConnTest")]
        OutcomeMsg OpenConnTest();

        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "ContactWebService")]
        OutcomeMsg ContactWebService();

        [WebInvoke(ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST", UriTemplate = "JsonTest")]
        OutcomeMsg JsonTest(QuickTest quickTest);
    }
}
