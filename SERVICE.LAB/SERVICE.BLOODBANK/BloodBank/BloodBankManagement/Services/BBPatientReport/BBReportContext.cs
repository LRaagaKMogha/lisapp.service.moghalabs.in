using AutoMapper;
using BloodBankManagement.Helpers;
using DEV.Common;
using Serilog;
using System.Data;
using Microsoft.Data.SqlClient;

namespace BloodBankManagement.Services.Reports
{
    public class BBReportContext
    {
        private readonly BloodBankDataContext dataContext;
        private readonly IMapper mapper;
        public string _connectionstring = string.Empty;
        public BBReportContext(string connectionstring)
        {
            _connectionstring= connectionstring;
        }
        public DataTable getdatatable(Dictionary<string, string> objparam, string ProcedureName)
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
                Log.Error(ex, ex.Message);
            }
            return result;
        }
    }
}