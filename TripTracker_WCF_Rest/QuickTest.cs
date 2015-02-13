using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TripTracker_WCF_Rest
{
    [DataContract]
    public class QuickTest
    {
        [DataMember]
        public string UserId { get; set; } 
    }
}