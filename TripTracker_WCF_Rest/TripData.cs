using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TripTracker_WCF_Rest
{
    [DataContract]
    public class TripData
    {
        [DataMember]
        public AllPairs[] coords { get; set; }
        [DataMember]
        public string purpose { get; set; }
        [DataMember]
        public string travelBy { get; set; }
        [DataMember]
        public int members { get; set; }
        [DataMember]
        public int nonMembers { get; set; }
        [DataMember]
        public int delays { get; set; }
        [DataMember]
        public int toll { get; set; }
        [DataMember]
        public double tollAmt { get; set; }
        [DataMember]
        public int payForParking { get; set; }
        [DataMember]
        public double payForParkingAmt { get; set; }
        [DataMember]
        public string startTime { get; set; }
        [DataMember]
        public string stopTime { get; set; }
        [DataMember]
        public userData user { get; set; }
        [DataMember]
        public int version { get; set; }
        [DataMember]
        public double fare { get; set; }

    }

    [DataContract]
    public class AllPairs
    {
        [DataMember]
        public GPS_Point coord { get; set; }
    }

    [DataContract]
    public class GPS_Point
    {
        [DataMember]
        public int hac { get; set; }
        [DataMember]
        public double spd { get; set; }
        [DataMember]
        public string rec { get; set; }
        [DataMember]
        public int vac { get; set; }
        [DataMember]
        public int alt { get; set; }
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class userData
    {
        [DataMember]
        public string device { get; set; }
        
        [DataMember]
        public string age { get; set; }
        [DataMember]
        public string gender { get; set; }

        
        [DataMember]
        public int workdays { get; set; }
        [DataMember]
        public int fulltime { get; set; }
        [DataMember]
        public int parttime { get; set; }
        [DataMember]
        public int empLess5Months { get; set; }
        [DataMember]
        public int unemployed { get; set; }
        [DataMember]
        public int retired { get; set; }
        [DataMember]
        public int workAtHome  { get; set; }
        [DataMember]
        public int homemaker  { get; set; }
        [DataMember]
        public int selfemployed { get; set; }

        [DataMember]
        public int student { get; set; }
        [DataMember]
        public string studentlevel { get; set; }
        [DataMember]
        public int driverLicense { get; set; }
        [DataMember]
        public int transitpass { get; set; }
        [DataMember]
        public int disableparkpass { get; set; }
        

    }

}