using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem1._1.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        [Display(Name = "Maximum Allocation of Days")]
        public int Days { get; set; }
    }
}
