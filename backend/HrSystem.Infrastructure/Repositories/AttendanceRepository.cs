using HrSystem.Application.Interfaces;
using HrSystem.Domain.Entities;
using HrSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HrSystem.Infrastructure.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .Include(a => a.Employee)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Attendance> AddAsync(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();

            return await _context.Attendances
                .Include(a => a.Employee)
                .FirstAsync(a => a.Id == attendance.Id);
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Entry(attendance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance is not null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
        }
    }
}
