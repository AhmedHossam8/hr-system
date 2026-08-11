using HrSystem.Application.DTOs;
using HrSystem.Application.Interfaces;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        var departments = await _repository.GetAllAsync();
        return departments.Select(ToDto);
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);
        return department is null ? null : ToDto(department);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            Code = dto.Code
        };

        await _repository.AddAsync(department);
        return ToDto(department);
    }

    public async Task UpdateAsync(int id, UpdateDepartmentDto dto)
    {
        var department = new Department
        {
            Id = id,
            Name = dto.Name,
            Code = dto.Code
        };

        await _repository.UpdateAsync(department);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    private static DepartmentDto ToDto(Department department)
    {
        return new DepartmentDto
        {
            Id = department.Id,
            Name = department.Name,
            Code = department.Code,
        };
    }
}
