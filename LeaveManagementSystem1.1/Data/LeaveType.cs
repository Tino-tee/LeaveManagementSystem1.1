using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveManagementSystem1._1.Data
{
    public class LeaveType
    {
        public int Id { get; set; }
        [MaxLength(100)]

        public string Name { get; set; }

        public int NumberOfDays { get; set; }
    }
}
