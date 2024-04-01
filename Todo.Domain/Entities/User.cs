using Todo.Domain.Entities.Shared;

namespace Todo.Domain.Entities
{
	public class User : EntityBase
	{
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public string Email { get; set; }

		public virtual ICollection<TodoList> Lists { get; set; }

		public User() { }
	}
}
