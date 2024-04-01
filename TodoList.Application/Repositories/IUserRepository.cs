using TodoList.Application.DTOs.Auth;
using TodoList.Domain.Entities;

namespace TodoList.Application.Repositories
{
	public interface IUserRepository
	{
		Task<RegistrationResponse> RegisterAsync(UserRegisterDTO userRegisterDTO);
		Task<LoginResponse> LoginAsync(UserLoginDTO userLoginDTO);

		Task<User?> GetByEmailAsync(string email);
	}
}
