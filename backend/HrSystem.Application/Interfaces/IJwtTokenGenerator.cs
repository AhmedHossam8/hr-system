using HrSystem.Domain.Entities;

namespace HrSystem.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}