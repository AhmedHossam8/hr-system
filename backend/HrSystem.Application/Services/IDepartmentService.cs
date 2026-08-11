using HrSystem.Application.DTOs;

namespace HrSystem.Application.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDto>> GetAllAsync();
        Task<DepartmentDto?> GetByIdAsync(int id);
        Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto);
        Task UpdateAsync(int id, UpdateDepartmentDto dto);
        Task DeleteAsync(int id);
    }
}