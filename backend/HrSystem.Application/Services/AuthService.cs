using HrSystem.Application.DTOs;
using HrSystem.Application.Exceptions;
using HrSystem.Application.Interfaces;
using HrSystem.Domain.Entities;

namespace HrSystem.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 6)
            {
                throw new BusinessRuleException("Password must be at least 6 characters long.");
            }

            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing is not null)
            {
                throw new BusinessRuleException("Email is already registered.");
            }

            var user = new User
            {
                Name = dto.Name.Trim(),
                Email = dto.Email.Trim().ToLower(),
                PasswordHash = _passwordHasher.Hash(dto.Password),
                Role = 1,
            };

            await _userRepository.AddAsync(user);

            return ToAuthResponse(user);
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user is null || !_passwordHasher.Verify(dto.Password, user.PasswordHash))
            {
                throw new BusinessRuleException("Invalid email or password.");
            }

            return ToAuthResponse(user);
        }

        private static AuthResponseDto ToAuthResponse(User user)
        {
            return new AuthResponseDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                Token = string.Empty,
            };
        }
    }
}