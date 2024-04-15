using Todo.Domain.Entities.Shared;

namespace Todo.Domain.Entities
{
	public class TodoList : EntityBase
	{
		public string Title { get; set; }
		public int OwnerId { get; set; }

		public virtual User Owner { get; set; }
		public virtual ICollection<TodoTask> Tasks { get; set; }


		public bool IsListOwnedByUser(int userId) => Owner.Id == userId;

	}
}
