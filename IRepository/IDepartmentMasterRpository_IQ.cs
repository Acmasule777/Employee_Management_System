using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.IRepository
{
    public interface IDepartmentMasterRpository_IQ
    {
        IEnumerable<DepartmentMaster> GetAllDepartments();

        DepartmentMaster GetDepartmentById(int id);
        void AddDepartment(DepartmentMaster department);
        void UpdateDepartments(DepartmentMaster department);

        void DeleteDepartment(int id);

        List<DepartmentMaster> GetAllDepartmentsUsingDataAdapter();
    }
}
