using DEV.Windows.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace DEV.Model.EF
{
    public class ReportContext
    {
        public string _connectionstring = string.Empty;
        public ReportContext(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public DataTable getdatatable(Dictionary<string,string> objparam, string ProcedureName)
        {
            DataTable result = new DataTable();          
            try
            {               
                using (SqlConnection oConnection = new SqlConnection(EncryptionHelper.Decrypt(_connectionstring)))
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
               // Log.Error(ex, ex.Message);
            }         
            return result;
        }

    }
}
