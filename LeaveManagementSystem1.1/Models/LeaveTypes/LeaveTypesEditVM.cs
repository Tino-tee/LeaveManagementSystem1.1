using System.ComponentModel.DataAnnotations;
namespace LeaveManagementSystem1._1.Models.LeaveTypes
{
    public class LeaveTypesEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "You have violated the length requirements")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 90)]
        [Display(Name = "Maximum Allocation of Days")]
        public int Days { get; set; }
    }
}
