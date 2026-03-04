using Service.Windows.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Win.Repository
{
    public class MasterHelper
    {

        public int InsertMasterRecord1(string departmentXML, string methodNameXML, string sampleNameXML, string containerNameXML, string unitNameXML, int venueNo, int venueBranchNo, int userNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.Pro_MasterRecord(departmentXML, methodNameXML, sampleNameXML, containerNameXML, unitNameXML, venueNo, venueBranchNo, userNo);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public int InsertMasterRecord(string departmentXML, string methodNameXML, string sampleNameXML, string containerNameXML, string unitNameXML, int venueNo, int venueBranchNo, int userNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.Pro_MasterRecord_V2(departmentXML, methodNameXML, sampleNameXML, containerNameXML, unitNameXML, venueNo, venueBranchNo, userNo);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public int InsertTestMaster1(string TestXL, int venueNo, int venueBranchNo, int userNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.Pro_MasterTestRecord(TestXL, venueNo, venueBranchNo, userNo);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public int InsertTestMaster(string TestXL, int venueNo, int venueBranchNo, int userNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.Pro_MasterTestRecord_V1(TestXL, venueNo, venueBranchNo, userNo);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<pro_CommonDetails_Result> GetCommonMaster1(string Masterkey, int VenueNo, int VenueBranchNo)
        {
            List<pro_CommonDetails_Result> objresult = new List<pro_CommonDetails_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.pro_CommonDetails(Masterkey, VenueNo, VenueNo).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objresult;
        }
        public List<pro_CommonDetails_Result> GetCommonMaster(string Masterkey, int VenueNo, int VenueBranchNo)
        {
            List<pro_CommonDetails_Result> objresult = new List<pro_CommonDetails_Result>();
            //List<pro_CommonDetails_V1_Result> objresultV1 = new List<pro_CommonDetails_V1_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    //arun changes -- For loading Venuebranch number, in that sp need to get venue no also
                    objresult = context.pro_CommonDetails(Masterkey, VenueNo, VenueNo).ToList();
                    //foreach (var item in objresult)
                    //{
                    //    pro_CommonDetails_Result ObjCommonDet = new pro_CommonDetails_Result();
                    //    ObjCommonDet.CommonNo = item.CommonNo;
                    //    ObjCommonDet.CommonKey = item.CommonKey;
                    //    ObjCommonDet.RowNo = item.RowNo;
                    //    ObjCommonDet.CommonCode = item.CommonCode;
                    //    ObjCommonDet.CommonName = item.CommonName;
                    //    ObjCommonDet.CommonValue = item.CommonValue;
                    //    ObjCommonDet.IsDefault = item.IsDefault;
                    //    ObjCommonDet.SequenceNo = item.SequenceNo;
                    //    objresult.Add(ObjCommonDet);
                    //}
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objresult;
        }

        public int InsertTestReferrenceRange(string TestXL, int venueNo, int venueBranchNo, int userNo)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.Pro_InsertTestReferenceRange(TestXL, venueNo, venueBranchNo, userNo);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string InsertQueueOrder(string title, string patientName, string gender, Nullable<System.DateTime> dOB, Nullable<int> age, string ageType, string mobileNo, string email, Nullable<int> uIDType, string uIDNo,
           string localbodies, string serviceNos, string serviceNames, string serviceTypes, string address, string area, string pincode, Nullable<int> venueNo, Nullable<int> venueBranchNo)
        {
            string result = String.Empty;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.Pro_InsertQueueOrder(title, patientName, gender, dOB, age, ageType, mobileNo, email, uIDType, uIDNo, localbodies,localbodies, serviceNos, serviceNames, serviceTypes, address, area, pincode, venueNo, venueBranchNo);
                    foreach (var output in data)
                    {
                        result = (string)output;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public int InsertMessageCommunication(string host, string username, string password, Nullable<int> port, Nullable<bool> isSSL, string param1, string param2, string param3, string communicationType, Nullable<int> venueno, Nullable<int> venueBranchNo, Nullable<int> userno, string templatecode, string subject, string body,string messagetype, Nullable<int> communicationno, Nullable<int> messageTemplateNo, string ccaddress, string bccaddress, Nullable<bool> templateStatus)
        {
            int result = 0;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    var data = context.pro_InsertMessageCommunication(host,username,password,port,isSSL,param1,param2,param3,communicationType,venueno,venueBranchNo,userno,templatecode,subject,body,messagetype,communicationno,messageTemplateNo,ccaddress,bccaddress,templateStatus);
                    foreach (Nullable<decimal> output in data)
                    {
                        result = (int)output.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<pro_GetMessageCommunicationList_Result> GetMessageCommunication(int VenueNo, int VenueBranchNo,int CommunicationNo,int MesgTemplateNo)
        {
            List<pro_GetMessageCommunicationList_Result> objresult = new List<pro_GetMessageCommunicationList_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    objresult = context.pro_GetMessageCommunicationList(VenueNo, VenueBranchNo,CommunicationNo,MesgTemplateNo).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return objresult;
        }
        public List<pro_GetOPDBookingdata_Result> GetOPDBookingdata(int VenueNo, int VenueBranchNo, string AppointmentDate, int PhysicianNo, int PhysicianVenueBranchNo, int BookingType)
        {
            List<pro_GetOPDBookingdata_Result> result = new List<pro_GetOPDBookingdata_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.pro_GetOPDBookingdata(AppointmentDate, PhysicianNo, VenueNo, VenueBranchNo, PhysicianVenueBranchNo, BookingType).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public List<pro_GetOPDPhysicianAppointment_Result> GetOPDPhysicianAppointment(int VenueNo, int VenueBranchNo, string AppointmentDate, int PhysicianNo, int specializationNo, int PhysicianVenueBranchNo)
        {
            List<pro_GetOPDPhysicianAppointment_Result> result = new List<pro_GetOPDPhysicianAppointment_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.pro_GetOPDPhysicianAppointment(AppointmentDate, PhysicianNo, specializationNo, VenueNo, VenueBranchNo, PhysicianVenueBranchNo).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public string InsertOPDPatient(int oPDPatientNo,int oPDPatientAppointmentNo, string titleCode, string firstName, string middleName, string lastName, DateTime dOB,
            string gender, int age, string ageType, string mobileNumber, string whatsappNo, string emailID, string secondaryEmailID, string address, int countryNo, int stateNo,
            int cityNo, string areaName, string pincode, int appointmentMode, int specializationNo, int physicianNo, string reason, DateTime appointmentDateTime,
            DateTime arrivedDateTime, int appointmentStatus, bool isNew, bool isEmergency, bool isVIP, int venueNo, int venueBranchNo, 
            int userNo, string emiratesId, bool isAutoEmail, bool isAutoSMS, bool isAutoWhatsApp,
            bool isSysCalDOB, int patientVisitID, int reasonType, int refferalType, int physNo, string refTypeothers)
        {
            string result = String.Empty;
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    context.pro_InsertOPDPatient(oPDPatientNo, oPDPatientAppointmentNo, titleCode, firstName, middleName, lastName, dOB,
                        gender, age, ageType, mobileNumber, whatsappNo, emailID, secondaryEmailID, address, countryNo, stateNo,
                        cityNo, areaName, pincode, appointmentMode, specializationNo, physicianNo, reason, appointmentDateTime,
                        arrivedDateTime, appointmentStatus, isNew, isEmergency, isVIP, venueNo, venueBranchNo,
                        userNo, emiratesId, isAutoEmail, isAutoSMS, isAutoWhatsApp,
                        isSysCalDOB, patientVisitID, reasonType, refferalType, physNo, refTypeothers,DateTime.Now,0,"");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
