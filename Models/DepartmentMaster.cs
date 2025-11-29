
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models
{
    public class DepartmentMaster
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter department name")]
        public string DepartmentName { get; set; }

        public string IsActive { get; set; }
    }
}
