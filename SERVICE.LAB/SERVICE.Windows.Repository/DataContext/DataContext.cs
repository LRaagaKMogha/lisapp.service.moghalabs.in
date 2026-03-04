using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace Service.Win.Repository
{
    public class DataContext
    {
        public string _connectionstring = string.Empty;
        public DataContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public bool check_connection()
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(_connectionstring);
            try
            {
                connection.Open();
                result = true;
                connection.Close();
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public DataTable getdatatable(Dictionary<string, string> objparam, string ProcedureName)
        {
            DataTable result = new DataTable();
            try
            {
                using (SqlConnection oConnection = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand oCommand = new SqlCommand())
                    {
                        oCommand.CommandText = ProcedureName;
                        oCommand.Connection = (SqlConnection)oConnection;
                        oCommand.CommandType = CommandType.StoredProcedure;
                        foreach (var item in objparam)
                        {
                            oCommand.Parameters.AddWithValue(item.Key, item.Value);
                        }
                        SqlDataAdapter da = new SqlDataAdapter(oCommand);
                        da.Fill(result);

                    }
                }
            }
            catch (Exception ex)
            {
                //  Log.Error(ex, ex.Message);
            }
            return result;
        }
    }
    public static class CommonExtension
    {
        public static string ValidateEmpty(this string value)
        {
            return string.IsNullOrEmpty(value) ? string.Empty : value;
        }
        public static List<Dictionary<String, Object>> DatableToDicionary(DataTable dataTable)
        {
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return tableRows;
        }
        /// <summary>
        /// URLShorten
        /// </summary>
        /// <param name="DynamicLink"></param>
        /// <param name="APIKey"></param>
        /// <returns></returns>
        public static string URLShorten(string DynamicLink, string APIKey)
        {
            try
            {
                PostGoogleURL objitem = new PostGoogleURL();
                objitem.longDynamicLink = "" + APIKey.Split('|')[1] + DynamicLink;
                objitem.suffix = new Suffix();
                objitem.suffix.option = "SHORT";
                HttpWebRequest ServiceWebRequest = (HttpWebRequest)WebRequest.Create("https://firebasedynamiclinks.googleapis.com/v1/shortLinks?key=" + APIKey.Split('|')[0]);
                ServiceWebRequest.Method = "POST";
                using (var streamWriter = new StreamWriter(ServiceWebRequest.GetRequestStream()))
                {
                    string json = JsonConvert.SerializeObject(objitem);
                    streamWriter.Write(json);
                    streamWriter.Flush();
                }
                ServiceWebRequest.Proxy = null;
                var httpResponse = (HttpWebResponse)ServiceWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var objResult = JsonConvert.DeserializeObject<GoogleResponse>(streamReader.ReadToEnd());
                    return objResult.shortLink;
                }
            }
            catch (Exception ex)
            {
                return DynamicLink;
            }
        }
    }
}
