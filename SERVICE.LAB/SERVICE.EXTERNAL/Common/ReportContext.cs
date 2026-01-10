using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DEV.Web.External
{
    public class ReportContext
    {
        public string _connectionstring = string.Empty;
        public ReportContext(string connectionstring)
        {
            _connectionstring = connectionstring;
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
                throw;
            }
            return result;
        }

        public ReportEntitiy getreportcontext(int VenueNo, int VenueBranchNo, string ReportKey)
        {
            ReportEntitiy result = new ReportEntitiy();
            try
            {
                using (SqlConnection oConnection = new SqlConnection(_connectionstring))
                {
                    using (SqlCommand oCommand = new SqlCommand())
                    {
                        oCommand.CommandText = "Pro_ReportContext";
                        oCommand.Connection = (SqlConnection)oConnection;
                        oCommand.CommandType = CommandType.StoredProcedure;
                        oCommand.Parameters.AddWithValue("VenueNo", VenueNo);
                        oCommand.Parameters.AddWithValue("VenueBranchNo", VenueBranchNo);

                        oCommand.Parameters.AddWithValue("ReportKey", ReportKey);
                        oConnection.Open();
                        using (SqlDataReader oReader = oCommand.ExecuteReader())
                        {
                            if (oReader.HasRows)
                            {
                                while (oReader.Read())
                                {                                    
                                    result.ProcedureName =oReader["ProcedureName"].ToString();
                                    result.ReportPath = oReader["ReportPath"].ToString();
                                    result.ExportPath = oReader["ExportPath"].ToString();
                                    result.ExportURL = oReader["ExportURL"].ToString();
                                }
                            }
                        }
                        oConnection.Close();

                    }
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
