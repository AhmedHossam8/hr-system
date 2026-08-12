using HrSystem.Application.DTOs;
using HrSystem.Application.Interfaces;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;

        public AttendanceService (IAttendanceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AttendanceDto>> GetAllAsync()
        {
            var attendances = await _repository.GetAllAsync();
            return attendances.Select(ToDto);
        }

        public async Task<AttendanceDto?> GetByIdAsync(int id)
        {
            var attendance = await _repository.GetByIdAsync(id);
            return attendance is null ? null : ToDto(attendance);
        }

        public async Task<AttendanceDto> CreateAsync(CreateAttendanceDto dto)
        {
            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,
                Date = dto.Date,
                ClockIn = dto.ClockIn,
                ClockOut = dto.ClockOut,
            };

            await _repository.AddAsync(attendance);
            return ToDto(attendance);
        }

        public async Task UpdateAsync(int id, UpdateAttendanceDto dto)
        {
            var attendance = new Attendance
            {
                Id = id,
                EmployeeId = dto.EmployeeId,
                Date = dto.Date,
                ClockIn = dto.ClockIn,
                ClockOut = dto.ClockOut,
            };
    
            await _repository.UpdateAsync(attendance);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private static AttendanceDto ToDto(Attendance attendance)
        {
            return new AttendanceDto
            {
                Id = attendance.Id,
                EmployeeId = attendance.EmployeeId,
                EmployeeName = attendance.Employee is null
                    ? string.Empty
                    : $"{attendance.Employee.FirstName} {attendance.Employee.LastName}",
                Date = attendance.Date,
                ClockIn = attendance.ClockIn,
                ClockOut = attendance.ClockOut,
            };
        }
    }
}