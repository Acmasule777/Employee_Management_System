using Microsoft.Data.SqlClient;
using System.Data;
using WebApplication2.IRepository;
using WebApplication2.Models;

namespace WebApplication2.Repository
{
    public class EmployeeMasterRepository : IEmployeeMasterUSP
    {

        private readonly string _connectionString;

        public EmployeeMasterRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ProjectConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'ProjectConnectionString' not found. ");

            }
            _connectionString = connectionString;

        }


        public IEnumerable<EmployeeMaster> GetAllEmployee()
        {
            var employeemasters = new List<EmployeeMaster>();
            using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "se_SelectAllEmployee";
                //For parameters input below commented line is used
                //cmd.Parameters.AddWithValue("@Emp" , id);
                cmd.Connection = conn;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeemasters.Add(new EmployeeMaster
                        {
                            EmpId = (int)reader["EmpId"],
                            EmpFirstName = reader["EmpFirstName"]?.ToString() ?? string.Empty,
                            EmpMiddleName = reader["EmpMiddleName"]?.ToString() ?? string.Empty,
                            EmpLastName = reader["EmpLastName"]?.ToString() ?? string.Empty,
                            EmpEmailId = reader["EmpEmailId"]?.ToString() ?? string.Empty,
                            EmpBirthDate = (DateTime)reader["EmpBirthDate"],
                            EmpGender = reader["EmpGender"]?.ToString() ?? string.Empty,
                            PhoneNumber = reader["PhoneNumber"]?.ToString() ?? string.Empty,
                            EmployeeAddress = reader["EmployeeAddress"]?.ToString() ?? string.Empty,
                            Salary = (decimal) reader["Salary"],
                            EmpStatus = (bool)reader["EmpStatus"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty 
                        });
                    }
                }

                    return employeemasters;

            }
        }

        public EmployeeMaster GetEmployeeById(int id)
        {
            EmployeeMaster employeemasters = null;
            using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "se_GetEmployeeById";
                cmd.Parameters.AddWithValue("@EmpId", id);
                cmd.Connection = conn;
                conn.Open();

                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employeemasters = new EmployeeMaster
                        {
                            EmpId = (int)reader["EmpId"],
                            EmpFirstName = reader["EmpFirstName"]?.ToString() ?? string.Empty,
                            EmpMiddleName = reader["EmpMiddleName"]?.ToString() ?? string.Empty,
                            EmpLastName = reader["EmpLastName"]?.ToString() ?? string.Empty,
                            EmpEmailId = reader["EmpEmailId"]?.ToString() ?? string.Empty,
                            EmpBirthDate = (DateTime)reader["EmpBirthDate"],
                            EmpGender = reader["EmpGender"]?.ToString() ?? string.Empty,
                            PhoneNumber = reader["PhoneNumber"]?.ToString() ?? string.Empty,
                            EmployeeAddress = reader["EmployeeAddress"]?.ToString() ?? string.Empty,
                            Salary = (decimal)reader["Salary"],
                            EmpStatus = (bool)reader["EmpStatus"],
                            DepartmentName = reader["DepartmentName"]?.ToString() ?? string.Empty,
                            DesignationName = reader["DesignationName"]?.ToString() ?? string.Empty
                        };
                    }
                }

            }

            return employeemasters;
        }

        public void AddEmployee(EmployeeMaster employee)
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "se_InsertEmployee";
                cmd.Parameters.AddWithValue("@EmpFirstName", employee.EmpFirstName);
                cmd.Parameters.AddWithValue("@EmpMiddleName", employee.EmpMiddleName);
                cmd.Parameters.AddWithValue("@EmpLastName", employee.EmpLastName);
                cmd.Parameters.AddWithValue("@EmpEmailId", employee.EmpEmailId);
                cmd.Parameters.AddWithValue("@EmpBirthDate", employee.EmpBirthDate);
                cmd.Parameters.AddWithValue("@EmpGender", employee.EmpGender);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@EmployeeAddress", employee.EmployeeAddress);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@EmpStatus", employee.EmpStatus);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@DesignationId", employee.DesignationId);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }

        public void UpdateEmployee(EmployeeMaster employee)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateEmployee";
                cmd.Parameters.AddWithValue("@EmpId", employee.EmpId);
                cmd.Parameters.AddWithValue("@EmpFirstName", employee.EmpFirstName);
                cmd.Parameters.AddWithValue("@EmpMiddleName", employee.EmpMiddleName);
                cmd.Parameters.AddWithValue("@EmpLastName", employee.EmpLastName);
                cmd.Parameters.AddWithValue("@EmpEmailId", employee.EmpEmailId);
                cmd.Parameters.AddWithValue("@EmpBirthDate", employee.EmpBirthDate);
                cmd.Parameters.AddWithValue("@EmpGender", employee.EmpGender);
                cmd.Parameters.AddWithValue("@PhoneNumber", employee.PhoneNumber);
                cmd.Parameters.AddWithValue("@EmployeeAddress", employee.EmployeeAddress);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@EmpStatus", employee.EmpStatus);
                cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                cmd.Parameters.AddWithValue("@DesignationId", employee.DesignationId);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public void DeleteEmployee(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeleterEmployee";
                cmd.Parameters.AddWithValue("@EmpId", id);
                cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        
    }
}
