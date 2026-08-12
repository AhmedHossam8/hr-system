using System.ComponentModel.DataAnnotations;

namespace HrSystem.Application.DTOs
{
    public class AttendanceDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public TimeOnly ClockIn { get; set; }
        public TimeOnly? ClockOut { get; set; }
    }
}