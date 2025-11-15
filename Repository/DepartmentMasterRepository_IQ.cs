using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication2.IRepository;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class DepartmentMasterRepository_IQ : IDepartmentMasterRpository_IQ
    {

        private readonly string _connectionString;

        public DepartmentMasterRepository_IQ(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ProjectConnectionString' not found. ");
                
            }
            _connectionString = connectionString;

        }

        public IEnumerable<DepartmentMaster> GetAllDepartments()
        {
            var departments = new List<DepartmentMaster>();
            using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();      //ExecuteNonQuery()
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetAllDepartments";
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        departments.Add(new DepartmentMaster
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            IsActive = reader["IsActive"]?.ToString() ?? string.Empty
                        });

                    }
                }
            }
            return departments;
        }

        public DepartmentMaster GetDepartmentById( int id)
        {
            DepartmentMaster department = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetDepartmentById";
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new DepartmentMaster
                        {
                            DepartmentId = (int)reader["DepartmentId"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            IsActive = reader["IsActive"]?.ToString() ?? string.Empty
                        };

                    }
                }
            }
            return department;
        }


        public void AddDepartment(DepartmentMaster department)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertDepartment";
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateDepartments(DepartmentMaster department)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateDepartmentById";
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                cmd.Parameters.AddWithValue("@DepartmentId", department.DepartmentId);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteDepartment(int id)
        {
           using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleteDepartmentById";
                cmd.Parameters.AddWithValue("@DepartmentId", id);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<DepartmentMaster> GetAllDepartmentsUsingDataAdapter()
        {
            List<DepartmentMaster> departmentMasters = new List<DepartmentMaster>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT DepartmentId, DepartmentName FROM DepartmentMasters";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();

                adapter.Fill(ds, "DepartmentMaster");
                var table = ds.Tables["DepartmentMaster"];

                if(table != null)
                {
                    foreach(DataRow row in table.Rows)
                    {
                        departmentMasters.Add(new DepartmentMaster
                        {
                            DepartmentId = Convert.ToInt32(row["DepartmentId"]),
                            DepartmentName = row["DepartmentName"]?.ToString() ?? string.Empty
                        });
                    }
                }
            }

            return departmentMasters;
        }
    }
}
