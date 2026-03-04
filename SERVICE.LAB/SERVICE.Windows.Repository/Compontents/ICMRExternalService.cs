using Newtonsoft.Json;
using Service.Win.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Script.Serialization;

namespace Service.Win.Repository
{
    public class ICMRExternalService
    {
        string result = string.Empty;
        public void PushService()
        {
            string ExternalServiceURL = ConfigurationManager.AppSettings["ExternalServiceURL"].ToString();
            string UserName = ConfigurationManager.AppSettings["UserName"].ToString();
            string Password = ConfigurationManager.AppSettings["Password"].ToString();
            result = GetTockenService(ExternalServiceURL, UserName, Password);
            if (!string.IsNullOrEmpty(result))
            {
                var lst = GetICMRTest();
                foreach (var item in lst)
                {
                    var response = PushICMRData(ExternalServiceURL, result, item);
                    JavaScriptSerializer objserlize = new JavaScriptSerializer();
                    string resultresponse = objserlize.Serialize(response);
                    UpdateICMRResult(item.ICMRResultNo, resultresponse, response.icmr_id.ToString());
                }
            }
            else {
                Logger.LogWrite("Invalid Token error");               
            }
        }
        public string GetTockenService(string ExternalServiceURL, string UserName, string Password)
        {
            string result = string.Empty;
            try
            {
                string ServiceMethod = ExternalServiceURL + "login";
                var ServiceWebRequest = WebRequest.CreateHttp(ServiceMethod);
                ServiceWebRequest.ContentType = "application/json";
                ServiceWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(ServiceWebRequest.GetRequestStream()))
                {
                    ExternalUserModel objExternalUser = new ExternalUserModel();
                    objExternalUser.username = UserName;
                    objExternalUser.password = Password;
                    string json = JsonConvert.SerializeObject(objExternalUser);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var data = JsonConvert.DeserializeObject<Tokenresponse>(streamReader.ReadToEnd());
                    result = data.token;
                    Logger.LogWrite(data.token);
                }
            }
            catch (WebException ex)
            {
                Logger.LogWrite(ex.Message);
                
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.Message);
               
            }
            return result;
        }
        public ExteranlPatientResponse PushICMRData(string ExternalServiceURL, string Tokendata, pro_GetICMRResultDetails_v2_Result objDTO)
        {
            ExteranlPatientResponse result = new ExteranlPatientResponse();
            string ServiceMethod = string.Empty;
            try
            {
                if (objDTO.Is_record == 1)
                    ServiceMethod = ExternalServiceURL + "add-record-apoorva";
                else if (objDTO.Is_record == 2)
                    ServiceMethod = ExternalServiceURL + "edit-record-apoorva";
                else if (objDTO.Is_record == 3)
                    ServiceMethod = ExternalServiceURL + "followup-record-apoorva";

                var ServiceWebRequest = WebRequest.CreateHttp(ServiceMethod);
                ServiceWebRequest.Headers["Authorization"] = "Bearer " + Tokendata;
                ServiceWebRequest.ContentType = "application/json";
                ServiceWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(ServiceWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(objDTO);
                    Logger.LogWrite(json);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = JsonConvert.DeserializeObject<ExteranlPatientResponse>(streamReader.ReadToEnd());
                    string json = JsonConvert.SerializeObject(result);
                    Logger.LogWrite(json);
                }
            }
            catch (WebException ex)
            {
                Logger.LogWrite(ex.Message);
                UpdateICMRResult(objDTO.ICMRResultNo, ex.Message, "0");
             
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.Message);
                UpdateICMRResult(objDTO.ICMRResultNo, ex.Message, "0");
       
            }
            return result;

        }
        public List<pro_GetICMRResultDetails_v2_Result> GetICMRTest()
        {
            List<pro_GetICMRResultDetails_v2_Result> result = new List<pro_GetICMRResultDetails_v2_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    result = context.pro_GetICMRResultDetails_v2(0, 0).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.Message);
                throw;
            }
            return result;
        }
        public void UpdateICMRResult(int ICMRResultNo, string response, string ICMRID)
        {
            List<pro_GetICMRResultDetails_v1_Result> result = new List<pro_GetICMRResultDetails_v1_Result>();
            try
            {
                using (YMEntities context = new YMEntities())
                {
                    context.pro_updateICMRResult_v1(0, 0, ICMRResultNo, response, ICMRID);
                }
            }
            catch (Exception ex)
            {
                Logger.LogWrite(ex.Message);
                throw;
            }
        }
    }
}
