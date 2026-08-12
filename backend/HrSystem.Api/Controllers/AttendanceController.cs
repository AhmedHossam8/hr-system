using HrSystem.Application.DTOs;
using HrSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HrSystem.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _service;

    public AttendanceController(IAttendanceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttendanceDto>>> GetAttendances()
    {
        var attendances = await _service.GetAllAsync();
        return Ok(attendances);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AttendanceDto>> GetAttendance(int id)
    {
        var attendance = await _service.GetByIdAsync(id);

        if (attendance is null)
        {
            return NotFound();
        }

        return Ok(attendance);
    }

    [HttpPost]
    public async Task<ActionResult<AttendanceDto>> CreateAttendance(CreateAttendanceDto dto)
    {
        var attendance = await _service.CreateAsync(dto);

        return CreatedAtAction(nameof(GetAttendance), new { id = attendance.Id }, attendance);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttendance(int id, UpdateAttendanceDto dto)
    {
        await _service.UpdateAsync(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttendance(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}
