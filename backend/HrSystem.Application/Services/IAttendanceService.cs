using HrSystem.Application.DTOs;

namespace HrSystem.Application.Services
{
    public interface IAttendanceService
    {
        Task<IEnumerable<AttendanceDto>> GetAllAsync();
        Task<AttendanceDto?> GetByIdAsync(int id);
        Task<AttendanceDto> CreateAsync(CreateAttendanceDto dto);
        Task UpdateAsync(int id, UpdateAttendanceDto dto);
        Task DeleteAsync(int id);
    }
}