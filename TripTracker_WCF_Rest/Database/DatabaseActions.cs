using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace TripTracker_WCF_Rest.Database
{
    public class DatabaseActions : IDatabaseRequests
    {
       
        private SqlConnection dConn;
        private string connectionString;
        private string connectionLocation;
        private string sResult;
        //private string vServer = "MARK-HP";
        //private string vUser = "triptracker";
        //private string vPwd = "4ZHe3WA7dyi";
        //private string vDbase = "TripTracker";

        public  DatabaseActions()
        {
            try
            {
                //connectionString = "Data Source=1515LP-KNOBLAUC.\\SQLEXPRESS;Initial Catalog=TripTracker;Integrated Security=True";
                //connectionLocation = "1515LP-2";
                
                connectionString = "Server=2227SR-CSSQL03\\CSWASDB1;User Id = triptracker; Password = 4ZHe3WA7dyi; Initial Catalog=TripTracker";
                connectionLocation = "2227SR-1r";

                dConn = new SqlConnection(connectionString);
                dConn.Open();

                
                dConn.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());               
            }
        }

        public OutcomeMsg OpenConnTest()
        {
            try
            {
                dConn.Open();
                
                sResult = " Success connecting to the database via the webservice " + connectionLocation;
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                dConn.Close();
                return oMsg;
            }
            catch(Exception e)
            {
                sResult = " Problem with connecting to the database via the webservice" + connectionLocation + "  " + "   errMsg  " + e.ToString(); 
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                return oMsg;
            }                      
        }

        public OutcomeMsg JsonTest(QuickTest quickTest)
        {
            dConn.Open();
            try
            {
                SqlParameter myParmUserId = new SqlParameter("@pUserId", System.Data.SqlDbType.VarChar);
                myParmUserId.Value = quickTest.UserId;
                SqlCommand cmdInsert = new SqlCommand("INSERT INTO jsonTest ( UserId ) " +
                                "Values ( @pUserId )", dConn);
                cmdInsert.Parameters.Add(myParmUserId);
                cmdInsert.ExecuteNonQuery();

                sResult = "UserId saved";
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                return oMsg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sResult = "UserId Problem  errMsg  " + e.ToString();
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                return oMsg;
            }
        }

        public OutcomeMsg SaveTrip(TripData trip)
        {
            dConn.Open();           
            try
            {
                //  The device # of the phone serves as the unique identifier for each user.
                //  It can be accessed in trip.user.device
                //  The value of trip.user.device is the User_Id
                SqlParameter myParmUserId = new SqlParameter("@pUserId", System.Data.SqlDbType.VarChar);
                myParmUserId.Value = trip.user.device;

                //  See if user rec already exists.
                //  If User_id # is not in surveyUsers, then create a new user record
                SqlDataReader ReadUsers = null;
                SqlCommand cmdSelectUser = new SqlCommand("select * from surveyUsers WHERE user_id = @pUserId", dConn);
                cmdSelectUser.Parameters.Add(myParmUserId);
                ReadUsers = cmdSelectUser.ExecuteReader();

                string vUser_id = null;
                while (ReadUsers.Read())
                    {
                        vUser_id = Convert.ToString(ReadUsers["user_id"]);
                    }
                ReadUsers.Close();
          
                
                if (vUser_id == null) 
                    { 
                        //  User record does not exist.  Insert a new user record
                        SqlParameter myParmUser_id = new SqlParameter("@pUser_id", System.Data.SqlDbType.VarChar);
                        myParmUser_id.Value = trip.user.device;
                        vUser_id = trip.user.device;
                        SqlParameter myParmTime = new SqlParameter("@pTime", System.Data.SqlDbType.DateTime);
                        myParmTime.Value = Convert.ToDateTime(trip.startTime);
                        SqlParameter myParmEmail = new SqlParameter("@pEmail", System.Data.SqlDbType.VarChar);
                        myParmEmail.Value = "NA";
                        SqlParameter myParmAge = new SqlParameter("@pAge", System.Data.SqlDbType.VarChar);
                        myParmAge.Value = trip.user.age;
                        SqlParameter myParmGender = new SqlParameter("@pGender", System.Data.SqlDbType.VarChar);
                        myParmGender.Value = trip.user.gender;
                        
                        SqlParameter myParmWorkDays = new SqlParameter("@pWorkDays", System.Data.SqlDbType.Int);
                        myParmWorkDays.Value = trip.user.workdays;
                        SqlParameter myParmFulltime = new SqlParameter("@pFulltime", System.Data.SqlDbType.Int);
                        myParmFulltime.Value = trip.user.fulltime;
                        SqlParameter myParmParttime = new SqlParameter("@pParttime", System.Data.SqlDbType.Int);
                        myParmParttime.Value = trip.user.parttime;
                        SqlParameter myParmEmpLess5Months = new SqlParameter("@pEmpLess5Months", System.Data.SqlDbType.Int);
                        myParmEmpLess5Months.Value = trip.user.empLess5Months;
                        SqlParameter myParmUnemployed = new SqlParameter("@pUnemployed", System.Data.SqlDbType.Int);
                        myParmUnemployed.Value = trip.user.unemployed;
                        SqlParameter myParmRetired = new SqlParameter("@pRetired", System.Data.SqlDbType.Int);
                        myParmRetired.Value = trip.user.retired;
                        SqlParameter myParmWorkAtHome = new SqlParameter("@pWorkAtHome", System.Data.SqlDbType.Int);
                        myParmWorkAtHome.Value = trip.user.workAtHome;
                        SqlParameter myParmHomemaker = new SqlParameter("@pHomemaker", System.Data.SqlDbType.Int);
                        myParmHomemaker.Value = trip.user.homemaker;
                        SqlParameter myParmSelfemployed = new SqlParameter("@pSelfemployed", System.Data.SqlDbType.Int);
                        myParmSelfemployed.Value = trip.user.selfemployed;

                        SqlParameter myParmStudent = new SqlParameter("@pStudent", System.Data.SqlDbType.Int);
                        myParmStudent.Value = trip.user.student;
                        SqlParameter myParmStudentLevel = new SqlParameter("@pStudentLevel", System.Data.SqlDbType.VarChar);
                        bool strTest = string.IsNullOrEmpty(trip.user.studentlevel);
                        if (strTest)
                        {
                            myParmStudentLevel.Value = "NA";
                        }
                        else { myParmStudentLevel.Value = trip.user.studentlevel;  }
                        SqlParameter myParmDriverLic = new SqlParameter("@pDriverLic", System.Data.SqlDbType.Int);
                        myParmDriverLic.Value = trip.user.driverLicense;
                        SqlParameter myParmTransitPass = new SqlParameter("@pTransitPass", System.Data.SqlDbType.Int);
                        myParmTransitPass.Value = trip.user.transitpass;
                        SqlParameter myParmDisablePass = new SqlParameter("@pDisablePass", System.Data.SqlDbType.Int);
                        myParmDisablePass.Value = trip.user.disableparkpass;

                        SqlCommand cmdInsert = new SqlCommand("INSERT INTO surveyUsers ( user_id, created, email,age,gender,workdays," +
                                "fulltime,parttime,empLess5Months,unemployed,retired,workAtHome,homemaker,selfemployed,student,studentLevel, driverLicense,transitpass, disableParkPass) " +
                                "Values ( @pUser_id ,@pTime, @pEmail, @pAge, @pGender,@pWorkDays," +
                                "@pFulltime,@pParttime,@pEmpLess5Months,@pUnemployed,@pRetired,@pWorkAtHome,@pHomemaker,@pSelfemployed,@pStudent,@pStudentLevel,@pDriverLic,@pTransitPass,@pDisablePass)", dConn);
                                             
                        cmdInsert.Parameters.Add(myParmUser_id);
                        cmdInsert.Parameters.Add(myParmTime);
                        cmdInsert.Parameters.Add(myParmEmail);
                        cmdInsert.Parameters.Add(myParmAge);
                        cmdInsert.Parameters.Add(myParmGender);

                        cmdInsert.Parameters.Add(myParmWorkDays);
                        cmdInsert.Parameters.Add(myParmFulltime);
                        cmdInsert.Parameters.Add(myParmParttime);
                        cmdInsert.Parameters.Add(myParmEmpLess5Months);
                        cmdInsert.Parameters.Add(myParmUnemployed);
                        cmdInsert.Parameters.Add(myParmRetired);
                        cmdInsert.Parameters.Add(myParmWorkAtHome);
                        cmdInsert.Parameters.Add(myParmHomemaker);
                        cmdInsert.Parameters.Add(myParmSelfemployed);
                       
                        cmdInsert.Parameters.Add(myParmStudent);
                        cmdInsert.Parameters.Add(myParmStudentLevel);
                        cmdInsert.Parameters.Add(myParmDriverLic);
                        cmdInsert.Parameters.Add(myParmTransitPass);
                        cmdInsert.Parameters.Add(myParmDisablePass);
                        cmdInsert.ExecuteNonQuery();
                    }

                //  vUser_id/trip.user.User_id uniquely identifies this Survey User
                //  Add the new trip data to the database

                //  Add question data/info to trip table
                SqlParameter myParmUser_Id1 = new SqlParameter("@pUser_Id1", System.Data.SqlDbType.VarChar);
                myParmUser_Id1.Value = vUser_id;
                SqlParameter myParmPurpose = new SqlParameter("@pPurpose", System.Data.SqlDbType.VarChar);
                myParmPurpose.Value = trip.purpose;
                SqlParameter myParmTravelBy = new SqlParameter("@pTravelBy", System.Data.SqlDbType.VarChar);
                myParmTravelBy.Value = trip.travelBy;
               
                SqlParameter myParmStartTime = new SqlParameter("@pStartTime", System.Data.SqlDbType.DateTime);
                myParmStartTime.Value = Convert.ToDateTime(trip.startTime);
                SqlParameter myParmStopTime = new SqlParameter("@pStopTime", System.Data.SqlDbType.DateTime);
                myParmStopTime.Value = Convert.ToDateTime(trip.stopTime);
                SqlParameter myParmMembers = new SqlParameter("@pMembers", System.Data.SqlDbType.Int);
                myParmMembers.Value = trip.members;
                SqlParameter myParmNonMembers = new SqlParameter("@pNonMembers", System.Data.SqlDbType.Int);
                myParmNonMembers.Value = trip.nonMembers;
                SqlParameter myParmDelays = new SqlParameter("@pDelays", System.Data.SqlDbType.Int);
                myParmDelays.Value = trip.delays;
                SqlParameter myParmToll = new SqlParameter("@pToll", System.Data.SqlDbType.Int);
                myParmToll.Value = trip.toll;
                SqlParameter myParmTollAmt = new SqlParameter("@pTollAmt", System.Data.SqlDbType.Decimal);
                myParmTollAmt.Value = trip.tollAmt;
                SqlParameter myParmPayForParking = new SqlParameter("@pPayForParking", System.Data.SqlDbType.Int);
                myParmPayForParking.Value = trip.payForParking;
                SqlParameter myParmPayForParkingAmt = new SqlParameter("@pPayForParkingAmt", System.Data.SqlDbType.Decimal);
                myParmPayForParkingAmt.Value = trip.payForParkingAmt;
                SqlParameter myParmFare = new SqlParameter("@pFare", System.Data.SqlDbType.Decimal);
                myParmFare.Value = trip.fare;             

                SqlCommand cmdTrip = new SqlCommand("INSERT INTO trips ( user_id,purpose,travelBy,startTime, stopTime, members,nonMembers,delays,toll,tollAmt,payForParking,payForParkingAmt,fare) " +
                    "Values (@pUser_Id1,@pPurpose,@pTravelBy,@pStartTime,@pStopTime,@pMembers,@pNonMembers,@pDelays,@pToll,@pTollAmt,@pPayForParking,@pPayForParkingAmt,@pFare)", dConn);
                cmdTrip.Parameters.Add(myParmUser_Id1);
                cmdTrip.Parameters.Add(myParmPurpose);
                cmdTrip.Parameters.Add(myParmTravelBy);
                cmdTrip.Parameters.Add(myParmStartTime);
                cmdTrip.Parameters.Add(myParmStopTime);
                cmdTrip.Parameters.Add(myParmMembers);
                cmdTrip.Parameters.Add(myParmNonMembers);
                cmdTrip.Parameters.Add(myParmDelays);
                cmdTrip.Parameters.Add(myParmToll);
                cmdTrip.Parameters.Add(myParmTollAmt);
                cmdTrip.Parameters.Add(myParmPayForParking);
                cmdTrip.Parameters.Add(myParmPayForParkingAmt);
                cmdTrip.Parameters.Add(myParmFare);
                     
                cmdTrip.ExecuteNonQuery();

                SqlDataReader readLastTrip = null;
                SqlParameter myParmUser_Id2 = new SqlParameter("@pUser_Id2", System.Data.SqlDbType.VarChar);
                myParmUser_Id2.Value = vUser_id;
                SqlParameter myParmStartTime2 = new SqlParameter("@pStartTime2", System.Data.SqlDbType.DateTime);
                myParmStartTime2.Value = Convert.ToDateTime(trip.startTime);
                SqlCommand cmdFindTrip = new SqlCommand("select * from trips WHERE user_id = @pUser_Id2 and startTime = @pStartTime2", dConn);
                cmdFindTrip.Parameters.Add(myParmUser_Id2);
                cmdFindTrip.Parameters.Add(myParmStartTime2);
                readLastTrip = cmdFindTrip.ExecuteReader();
                int vTripId = -1;
                while (readLastTrip.Read())
                {
                    vTripId = Convert.ToInt32(readLastTrip["trip_id"]);
                }
                readLastTrip.Close();
            
                //  Add coords of this trip to coord table
                //  vTripId is the primary key in table Trips and is a foreign key in table coords

                int coordRecs = trip.coords.Length;

                DataTable dTable = new DataTable("coords");

                DataColumn dcTripId = new DataColumn();
                dcTripId.ColumnName = "trip_id";
                dcTripId.DataType = Type.GetType("System.Int32");

                DataColumn dcTimeRec = new DataColumn();
                dcTimeRec.ColumnName = "recorded";
                dcTimeRec.DataType = Type.GetType("System.DateTime");

                DataColumn dcLat = new DataColumn();
                dcLat.ColumnName = "latitude";
                dcLat.DataType = Type.GetType("System.Decimal");

                DataColumn dcLong = new DataColumn();
                dcLong.ColumnName = "longitude";
                dcLong.DataType = Type.GetType("System.Decimal");

                dTable.Columns.Add(dcTripId);
                dTable.Columns.Add(dcTimeRec);
                dTable.Columns.Add(dcLat);
                dTable.Columns.Add(dcLong);

                for (int j = 0; j < coordRecs; j++)
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["trip_id"] = vTripId;
                    dRow["recorded"] = Convert.ToDateTime(trip.coords[j].coord.rec);
                    dRow["latitude"] = trip.coords[j].coord.lat;
                    dRow["longitude"] = trip.coords[j].coord.lon;
                    dTable.Rows.Add(dRow);
                }

                using (SqlBulkCopy sBulk = new SqlBulkCopy(dConn))
                {
                    sBulk.DestinationTableName = dTable.TableName;
                    foreach (var column in dTable.Columns)
                        sBulk.ColumnMappings.Add(column.ToString(), column.ToString());
                    sBulk.WriteToServer(dTable);
                }

                dConn.Close();
                sResult = "Trip data was saved";
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                return oMsg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sResult = " Problem saving  errMsg  " + e.ToString();
                OutcomeMsg oMsg = new OutcomeMsg(sResult);

                //SqlParameter eUser_Id = new SqlParameter("@eUser_Id", System.Data.SqlDbType.VarChar);
                //eUser_Id.Value = trip.user.device;
                //SqlParameter eErrMsg = new SqlParameter("@eErr", System.Data.SqlDbType.VarChar);
                //eErrMsg.Value = e.ToString();
                //SqlCommand cmdErr = new SqlCommand("INSERT INTO ErrMsgs_Internal ( user_id,dbaseErr) " +
                //   "Values (@eUser_Id,@eErr)", dConn);
                //cmdErr.Parameters.Add(eUser_Id);
                //cmdErr.Parameters.Add(eErrMsg);
                return oMsg;
            }
        }

        //
        public OutcomeMsg TestTrip(TripData trip)
        {
            dConn.Open();
            try
            {
                double t1 = DateTime.Now.TimeOfDay.TotalMilliseconds;
                //  The device # of the phone serves as the unique identifier for each user.
                //  It can be accessed in trip.user.device
                //  The value of trip.user.device is the User_Id
                SqlParameter myParmUserId = new SqlParameter("@pUserId", System.Data.SqlDbType.VarChar);
                myParmUserId.Value = trip.user.device;

                //  See if user rec already exists.
                //  If User_id # is not in surveyUsers, then create a new user record
                SqlDataReader ReadUsers = null;
                SqlCommand cmdSelectUser = new SqlCommand("select * from surveyUsers WHERE user_id = @pUserId", dConn);
                cmdSelectUser.Parameters.Add(myParmUserId);
                ReadUsers = cmdSelectUser.ExecuteReader();

                string vUser_id = null;
                while (ReadUsers.Read())
                {
                    vUser_id = Convert.ToString(ReadUsers["user_id"]);
                }
                ReadUsers.Close();


                if (vUser_id == null)
                {
                    //  User record does not exist.  Insert a new user record
                    SqlParameter myParmUser_id = new SqlParameter("@pUser_id", System.Data.SqlDbType.VarChar);
                    myParmUser_id.Value = trip.user.device;
                    vUser_id = trip.user.device;
                    SqlParameter myParmTime = new SqlParameter("@pTime", System.Data.SqlDbType.DateTime);
                    myParmTime.Value = Convert.ToDateTime(trip.startTime);
                    SqlParameter myParmEmail = new SqlParameter("@pEmail", System.Data.SqlDbType.VarChar);
                    myParmEmail.Value = "NA";
                    SqlParameter myParmAge = new SqlParameter("@pAge", System.Data.SqlDbType.VarChar);
                    myParmAge.Value = trip.user.age;
                    SqlParameter myParmGender = new SqlParameter("@pGender", System.Data.SqlDbType.VarChar);
                    myParmGender.Value = trip.user.gender;

                    SqlParameter myParmWorkDays = new SqlParameter("@pWorkDays", System.Data.SqlDbType.Int);
                    myParmWorkDays.Value = trip.user.workdays;
                    SqlParameter myParmFulltime = new SqlParameter("@pFulltime", System.Data.SqlDbType.Int);
                    myParmFulltime.Value = trip.user.fulltime;
                    SqlParameter myParmParttime = new SqlParameter("@pParttime", System.Data.SqlDbType.Int);
                    myParmParttime.Value = trip.user.parttime;
                    SqlParameter myParmEmpLess5Months = new SqlParameter("@pEmpLess5Months", System.Data.SqlDbType.Int);
                    myParmEmpLess5Months.Value = trip.user.empLess5Months;
                    SqlParameter myParmUnemployed = new SqlParameter("@pUnemployed", System.Data.SqlDbType.Int);
                    myParmUnemployed.Value = trip.user.unemployed;
                    SqlParameter myParmRetired = new SqlParameter("@pRetired", System.Data.SqlDbType.Int);
                    myParmRetired.Value = trip.user.retired;
                    SqlParameter myParmWorkAtHome = new SqlParameter("@pWorkAtHome", System.Data.SqlDbType.Int);
                    myParmWorkAtHome.Value = trip.user.workAtHome;
                    SqlParameter myParmHomemaker = new SqlParameter("@pHomemaker", System.Data.SqlDbType.Int);
                    myParmHomemaker.Value = trip.user.homemaker;
                    SqlParameter myParmSelfemployed = new SqlParameter("@pSelfemployed", System.Data.SqlDbType.Int);
                    myParmSelfemployed.Value = trip.user.selfemployed;

                    SqlParameter myParmStudent = new SqlParameter("@pStudent", System.Data.SqlDbType.Int);
                    myParmStudent.Value = trip.user.student;
                    SqlParameter myParmStudentLevel = new SqlParameter("@pStudentLevel", System.Data.SqlDbType.VarChar);
                    bool strTest = string.IsNullOrEmpty(trip.user.studentlevel);
                    if (strTest)
                    {
                        myParmStudentLevel.Value = "NA";
                    }
                    else { myParmStudentLevel.Value = trip.user.studentlevel; }
                    SqlParameter myParmDriverLic = new SqlParameter("@pDriverLic", System.Data.SqlDbType.Int);
                    myParmDriverLic.Value = trip.user.driverLicense;
                    SqlParameter myParmTransitPass = new SqlParameter("@pTransitPass", System.Data.SqlDbType.Int);
                    myParmTransitPass.Value = trip.user.transitpass;
                    SqlParameter myParmDisablePass = new SqlParameter("@pDisablePass", System.Data.SqlDbType.Int);
                    myParmDisablePass.Value = trip.user.disableparkpass;

                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO surveyUsers ( user_id, created, email,age,gender,workdays," +
                            "fulltime,parttime,empLess5Months,unemployed,retired,workAtHome,homemaker,selfemployed,student,studentLevel, driverLicense,transitpass, disableParkPass) " +
                            "Values ( @pUser_id ,@pTime, @pEmail, @pAge, @pGender,@pWorkDays," +
                            "@pFulltime,@pParttime,@pEmpLess5Months,@pUnemployed,@pRetired,@pWorkAtHome,@pHomemaker,@pSelfemployed,@pStudent,@pStudentLevel,@pDriverLic,@pTransitPass,@pDisablePass)", dConn);

                    cmdInsert.Parameters.Add(myParmUser_id);
                    cmdInsert.Parameters.Add(myParmTime);
                    cmdInsert.Parameters.Add(myParmEmail);
                    cmdInsert.Parameters.Add(myParmAge);
                    cmdInsert.Parameters.Add(myParmGender);

                    cmdInsert.Parameters.Add(myParmWorkDays);
                    cmdInsert.Parameters.Add(myParmFulltime);
                    cmdInsert.Parameters.Add(myParmParttime);
                    cmdInsert.Parameters.Add(myParmEmpLess5Months);
                    cmdInsert.Parameters.Add(myParmUnemployed);
                    cmdInsert.Parameters.Add(myParmRetired);
                    cmdInsert.Parameters.Add(myParmWorkAtHome);
                    cmdInsert.Parameters.Add(myParmHomemaker);
                    cmdInsert.Parameters.Add(myParmSelfemployed);

                    cmdInsert.Parameters.Add(myParmStudent);
                    cmdInsert.Parameters.Add(myParmStudentLevel);
                    cmdInsert.Parameters.Add(myParmDriverLic);
                    cmdInsert.Parameters.Add(myParmTransitPass);
                    cmdInsert.Parameters.Add(myParmDisablePass);
                    cmdInsert.ExecuteNonQuery();
                }

                //  vUser_id/trip.user.User_id uniquely identifies this Survey User
                //  Add the new trip data to the database

                //  Add question data/info to trip table
                SqlParameter myParmUser_Id1 = new SqlParameter("@pUser_Id1", System.Data.SqlDbType.VarChar);
                myParmUser_Id1.Value = vUser_id;
                SqlParameter myParmPurpose = new SqlParameter("@pPurpose", System.Data.SqlDbType.VarChar);
                myParmPurpose.Value = trip.purpose;
                SqlParameter myParmTravelBy = new SqlParameter("@pTravelBy", System.Data.SqlDbType.VarChar);
                myParmTravelBy.Value = trip.travelBy;

                SqlParameter myParmStartTime = new SqlParameter("@pStartTime", System.Data.SqlDbType.DateTime);
                myParmStartTime.Value = Convert.ToDateTime(trip.startTime);
                SqlParameter myParmStopTime = new SqlParameter("@pStopTime", System.Data.SqlDbType.DateTime);
                myParmStopTime.Value = Convert.ToDateTime(trip.stopTime);
                SqlParameter myParmMembers = new SqlParameter("@pMembers", System.Data.SqlDbType.Int);
                myParmMembers.Value = trip.members;
                SqlParameter myParmNonMembers = new SqlParameter("@pNonMembers", System.Data.SqlDbType.Int);
                myParmNonMembers.Value = trip.nonMembers;
                SqlParameter myParmDelays = new SqlParameter("@pDelays", System.Data.SqlDbType.Int);
                myParmDelays.Value = trip.delays;
                SqlParameter myParmToll = new SqlParameter("@pToll", System.Data.SqlDbType.Int);
                myParmToll.Value = trip.toll;
                SqlParameter myParmTollAmt = new SqlParameter("@pTollAmt", System.Data.SqlDbType.Decimal);
                myParmTollAmt.Value = trip.tollAmt;
                SqlParameter myParmPayForParking = new SqlParameter("@pPayForParking", System.Data.SqlDbType.Int);
                myParmPayForParking.Value = trip.payForParking;
                SqlParameter myParmPayForParkingAmt = new SqlParameter("@pPayForParkingAmt", System.Data.SqlDbType.Decimal);
                myParmPayForParkingAmt.Value = trip.payForParkingAmt;
                SqlParameter myParmFare = new SqlParameter("@pFare", System.Data.SqlDbType.Decimal);
                myParmFare.Value = trip.fare;

                SqlCommand cmdTrip = new SqlCommand("INSERT INTO trips ( user_id,purpose,travelBy,startTime, stopTime, members,nonMembers,delays,toll,tollAmt,payForParking,payForParkingAmt,fare) " +
                    "Values (@pUser_Id1,@pPurpose,@pTravelBy,@pStartTime,@pStopTime,@pMembers,@pNonMembers,@pDelays,@pToll,@pTollAmt,@pPayForParking,@pPayForParkingAmt,@pFare)", dConn);
                cmdTrip.Parameters.Add(myParmUser_Id1);
                cmdTrip.Parameters.Add(myParmPurpose);
                cmdTrip.Parameters.Add(myParmTravelBy);
                cmdTrip.Parameters.Add(myParmStartTime);
                cmdTrip.Parameters.Add(myParmStopTime);
                cmdTrip.Parameters.Add(myParmMembers);
                cmdTrip.Parameters.Add(myParmNonMembers);
                cmdTrip.Parameters.Add(myParmDelays);
                cmdTrip.Parameters.Add(myParmToll);
                cmdTrip.Parameters.Add(myParmTollAmt);
                cmdTrip.Parameters.Add(myParmPayForParking);
                cmdTrip.Parameters.Add(myParmPayForParkingAmt);
                cmdTrip.Parameters.Add(myParmFare);

                cmdTrip.ExecuteNonQuery();

                SqlDataReader readLastTrip = null;
                SqlParameter myParmUser_Id2 = new SqlParameter("@pUser_Id2", System.Data.SqlDbType.VarChar);
                myParmUser_Id2.Value = vUser_id;
                SqlParameter myParmStartTime2 = new SqlParameter("@pStartTime2", System.Data.SqlDbType.DateTime);
                myParmStartTime2.Value = Convert.ToDateTime(trip.startTime);
                SqlCommand cmdFindTrip = new SqlCommand("select * from trips WHERE user_id = @pUser_Id2 and startTime = @pStartTime2", dConn);
                cmdFindTrip.Parameters.Add(myParmUser_Id2);
                cmdFindTrip.Parameters.Add(myParmStartTime2);
                readLastTrip = cmdFindTrip.ExecuteReader();
                int vTripId = -1;
                while (readLastTrip.Read())
                {
                    vTripId = Convert.ToInt32(readLastTrip["trip_id"]);
                }
                readLastTrip.Close();

                //  Add coords of this trip to coord table
                //  vTripId is the primary key in table Trips and is a foreign key in table coords

                int coordRecs = trip.coords.Length;
                
                DataTable dTable = new DataTable("coords");

                DataColumn dcTripId = new DataColumn();
                dcTripId.ColumnName = "trip_id";
                dcTripId.DataType = Type.GetType("System.Int32");

                DataColumn dcTimeRec = new DataColumn();
                dcTimeRec.ColumnName = "recorded";
                dcTimeRec.DataType = Type.GetType("System.DateTime");

                DataColumn dcLat = new DataColumn();
                dcLat.ColumnName = "latitude";
                dcLat.DataType = Type.GetType("System.Decimal");

                DataColumn dcLong = new DataColumn();
                dcLong.ColumnName = "longitude";
                dcLong.DataType = Type.GetType("System.Decimal");

                dTable.Columns.Add(dcTripId);
                dTable.Columns.Add(dcTimeRec);
                dTable.Columns.Add(dcLat);
                dTable.Columns.Add(dcLong);

                for (int j = 0; j < coordRecs; j++)
                {
                    DataRow dRow = dTable.NewRow();
                    dRow["trip_id"] = vTripId;
                    dRow["recorded"] = Convert.ToDateTime(trip.coords[j].coord.rec);
                    dRow["latitude"] = trip.coords[j].coord.lat;
                    dRow["longitude"] = trip.coords[j].coord.lon;
                    dTable.Rows.Add(dRow);
                }
                
                using (SqlBulkCopy sBulk = new SqlBulkCopy(dConn))
                {
                    //int rowCount = dTable.Rows.Count;
                    //int tId = dTable.Rows[2].Field<Int32>("latitude");
                    //tId = dTable.Rows[rowCount-1].Field<Int32>("trip_id");
                    sBulk.DestinationTableName = dTable.TableName;
                    foreach (var column in dTable.Columns)
                        sBulk.ColumnMappings.Add(column.ToString(), column.ToString());
                    sBulk.WriteToServer(dTable);
                }
                    //SqlParameter myParmTripId = new SqlParameter("@pTripId", System.Data.SqlDbType.Int);
                    //myParmTripId.Value = vTripId;
                    //SqlParameter myParmRecorded = new SqlParameter("@pRecorded", System.Data.SqlDbType.DateTime);
                    //myParmRecorded.Value = Convert.ToDateTime(trip.coords[j].coord.rec);
                    //SqlParameter myParmLatitude = new SqlParameter("@pLatitude", System.Data.SqlDbType.Decimal);
                    //myParmLatitude.Value = trip.coords[j].coord.lat;
                    //SqlParameter myParmLongitude = new SqlParameter("@pLongitude", System.Data.SqlDbType.Decimal);
                    //myParmLongitude.Value = trip.coords[j].coord.lon;
                    //SqlParameter myParmAltitude = new SqlParameter("@pAltitude", System.Data.SqlDbType.Decimal);
                    //myParmAltitude.Value = trip.coords[j].coord.alt;
                    //SqlParameter myParmSpeed = new SqlParameter("@pSpeed", System.Data.SqlDbType.Decimal);
                    //myParmSpeed.Value = trip.coords[j].coord.spd;
                    //SqlParameter myParmHAcc = new SqlParameter("@pHAcc", System.Data.SqlDbType.Decimal);
                    //myParmHAcc.Value = trip.coords[j].coord.hac;
                    //SqlParameter myParmVAcc = new SqlParameter("@pVAcc", System.Data.SqlDbType.Decimal);
                    //myParmVAcc.Value = trip.coords[j].coord.vac;

                    //SqlCommand cmdCoords = new SqlCommand("INSERT INTO coords ( trip_id,recorded,latitude,longitude,altitude,speed,hAccuracy,vAccuracy) " +
                    //"Values ( @pTripId , @pRecorded, @pLatitude, @pLongitude,@pAltitude, @pSpeed,@pHAcc,@pVAcc)", dConn);

                    //SqlCommand cmdCoords = new SqlCommand("INSERT INTO coords ( trip_id,recorded,latitude,longitude) " +
                    //"Values ( @pTripId , @pRecorded, @pLatitude, @pLongitude)", dConn);

                    //cmdCoords.Parameters.Add(myParmTripId);
                    //cmdCoords.Parameters.Add(myParmRecorded);
                    //cmdCoords.Parameters.Add(myParmLatitude);
                    //cmdCoords.Parameters.Add(myParmLongitude);
                    //cmdCoords.Parameters.Add(myParmAltitude);
                    //cmdCoords.Parameters.Add(myParmSpeed);
                    //cmdCoords.Parameters.Add(myParmHAcc);
                    //cmdCoords.Parameters.Add(myParmVAcc);
                    //cmdCoords.ExecuteNonQuery();

                

                double t2 = DateTime.Now.TimeOfDay.TotalMilliseconds;
                double t3 = ((t2 - t1) / 1000);

                dConn.Close();
                //sResult = "Trip data was saved";
                sResult = "Trip data was saved     " + t3;
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                return oMsg;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                sResult = " Problem saving  errMsg  " + e.ToString();
                OutcomeMsg oMsg = new OutcomeMsg(sResult);

                SqlParameter eUser_Id = new SqlParameter("@eUser_Id", System.Data.SqlDbType.VarChar);
                eUser_Id.Value = trip.user.device;
                SqlParameter eErrMsg = new SqlParameter("@eErr", System.Data.SqlDbType.VarChar);
                eErrMsg.Value = e.ToString();
                SqlCommand cmdErr = new SqlCommand("INSERT INTO ErrMsgs_Internal ( user_id,dbaseErr) " +
                   "Values (@eUser_Id,@eErr)", dConn);
                cmdErr.Parameters.Add(eUser_Id);
                cmdErr.Parameters.Add(eErrMsg);
                cmdErr.ExecuteNonQuery();

                return oMsg;
            }
        }
        
        public OutcomeMsg readTable()
        {
            try
            {
                dConn.Open();
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from test",
                                                         dConn);
                myReader = myCommand.ExecuteReader();
                int vId = 0;
                string vRec = "";
                string vAddress = "";
                while (myReader.Read())
                {
                    vId = Convert.ToInt16(myReader["id"]);
                    vRec = myReader["name"].ToString();
                    vAddress = myReader["address"].ToString();
                }
                myReader.Close();
                dConn.Close();
                sResult = " Success reading the database via the webservice " + connectionLocation;
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                dConn.Close();
                return oMsg;
            }
            catch (Exception e)
            {
                sResult = " Problem reading from the database via the webservice" + connectionLocation + "  " + "   errMsg  " + e.ToString();
                OutcomeMsg oMsg = new OutcomeMsg(sResult);
                return oMsg;
            }
        }
        
    }
}