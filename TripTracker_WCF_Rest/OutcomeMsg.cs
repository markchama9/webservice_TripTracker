using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TripTracker_WCF_Rest
{
    [DataContract]
    public class OutcomeMsg
    {
        [DataMember]
        public string Result { get; set; }

        public OutcomeMsg(string msg)
        {
            this.Result = msg;
        }
    }


}