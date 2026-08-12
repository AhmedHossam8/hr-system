using System.ComponentModel.DataAnnotations;

namespace HrSystem.Application.DTOs;

public class UpdateDepartmentDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Code { get; set; } = string.Empty;
}