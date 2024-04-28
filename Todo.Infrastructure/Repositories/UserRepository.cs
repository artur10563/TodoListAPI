using Todo.Application.DTOs.Auth;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Repositories.Shared;
using Todo.Domain.Primitives;
using Todo.Domain.Errors;

namespace Todo.Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly JwtTokenProvider _jwtTokenProvider;

		public UserRepository(AppDbContext context, JwtTokenProvider jwtTokenProvider) : base(context)
		{
			_jwtTokenProvider = jwtTokenProvider;
		}

		public async Task<Result<string>> LoginAsync(UserLoginDTO userLoginDTO)
		{
			var user = await GetByEmailAsync(userLoginDTO.Email);

			if (user is null)
			{
				return UserErrors.UserNotFound;
			}

			var checkPassword = BCrypt.Net.BCrypt.Verify(userLoginDTO.Password, user.PasswordHash);
			if (checkPassword)
			{
				var token = _jwtTokenProvider.Generate(user);
				return Result.Success(token);
			}
			else
				return new Error(StatusCode.BadRequest, "Invalid password");
		}

		public async Task<Result> RegisterAsync(UserRegisterDTO userRegisterDTO)
		{

			Add(new User()
			{
				Email = userRegisterDTO.Email.ToLower(),
				Name = userRegisterDTO.Nickname,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(userRegisterDTO.Password)
			});

			await _context.SaveChangesAsync();
			return Result.Success();

		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _context.Users
				.FirstOrDefaultAsync(u =>
				u.Email.Equals(email.ToLower()));
		}

	}
}
