using System.ComponentModel.DataAnnotations;

namespace HrSystem.Application.DTOs
{
    public class UpdateAttendanceDto
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        public TimeOnly ClockIn { get; set; }
        public TimeOnly? ClockOut { get; set; }
    }
}