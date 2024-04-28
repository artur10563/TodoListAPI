using System.ComponentModel.DataAnnotations;

namespace Todo.Application.DTOs.Auth
{
	public class UserRegisterDTO
	{
		public string Nickname { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required, EmailAddress, StringLength(maximumLength: 50)]
		public string Email { get; set; } = string.Empty;

	}
}
