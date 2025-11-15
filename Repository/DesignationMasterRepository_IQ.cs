using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication2.IRepository;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class DesignationMasterRepository_IQ : IDesignationMasterRepository_IQ
    {

        private readonly string _connectionString;

        public DesignationMasterRepository_IQ(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ProjectConnectionString' not found. ");

            }
            _connectionString = connectionString;

        }


        public IEnumerable<DesignationMaster> GetAllDesignation()
        {
            var designationMaster = new List<DesignationMaster>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();      //ExecuteNonQuery()
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetAllDesignations";
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        designationMaster.Add(new DesignationMaster
                        {
                            DesignationId = (int)reader["DesignationId"],
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty,
                            IsActive = reader["IsActive"]?.ToString() ?? string.Empty
                        });

                    }
                }
            }
            return designationMaster;
        }

        public DesignationMaster GetDesignationById(int id)
        {
            DesignationMaster designationMaster = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetDesignationById";
                cmd.Parameters.AddWithValue("@DesignationId", id);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        designationMaster = new DesignationMaster
                        {
                            DesignationId = (int)reader["DesignationId"],
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty,
                            IsActive = reader["IsActive"]?.ToString() ?? string.Empty
                        };

                    }
                }
            }
            return designationMaster;
        }

        public void AddDesignation(DesignationMaster designationMaster)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.CommandText = "usp_InsertDesignation";
                cmd.Parameters.AddWithValue("@DesignationName", designationMaster.DesignationName);
                cmd.Connection = conn;  
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void UpdateDesignation(DesignationMaster designationMaster)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateDesignationById";
                cmd.Parameters.AddWithValue("@DesignationName", designationMaster.DesignationName);
                cmd.Parameters.AddWithValue("@DesignationId", designationMaster.DesignationId);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteDesignation(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure; 
                cmd.CommandText = "usp_DeleteDesignationById";
                cmd.Parameters.AddWithValue("@DesignationId", id);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


    }
}
