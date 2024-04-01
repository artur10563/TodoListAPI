using Todo.Application.DTOs.Auth;
using Todo.Domain.Entities;

namespace Todo.Application.Repositories
{
	public interface IUserRepository
	{
		Task<RegistrationResponse> RegisterAsync(UserRegisterDTO userRegisterDTO);
		Task<LoginResponse> LoginAsync(UserLoginDTO userLoginDTO);

		Task<User?> GetByEmailAsync(string email);
	}
}
