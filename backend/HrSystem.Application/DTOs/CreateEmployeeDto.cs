using System.ComponentModel.DataAnnotations;

namespace HrSystem.Application.DTOs;

public class CreateEmployeeDto
{
    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Position { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Salary { get; set; }

    public DateTime HireDate { get; set; }
}