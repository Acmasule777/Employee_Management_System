using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DesignationMaster
    {
        [Key]
        public int DesignationId { get; set; }

        [Required(ErrorMessage = "Please enter designation")]
        public string DesignationName { get; set; }

        public string IsActive { get; set; }

    }
}
