using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.IRepository
{
    public interface IEmployeeMasterUSP
    {
        IEnumerable<EmployeeMaster> GetAllEmployee();

        EmployeeMaster GetEmployeeById(int id);
        void AddEmployee(EmployeeMaster employee);
        void UpdateEmployee(EmployeeMaster employee);

        void DeleteEmployee(int id);

        //List<EmployeeMaster> GetAllDepartmentsUsingDataAdapter();
    }
}
