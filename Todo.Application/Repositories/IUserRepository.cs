using Todo.Application.DTOs.Auth;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Repositories
{
	public interface IUserRepository
	{
		Task<Result> RegisterAsync(UserRegisterDTO userRegisterDTO);
		Task<Result<string>> LoginAsync(UserLoginDTO userLoginDTO);

		Task<User?> GetByEmailAsync(string email);
	}
}
