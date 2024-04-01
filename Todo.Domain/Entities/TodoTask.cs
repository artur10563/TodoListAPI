using Todo.Domain.Entities.Shared;
using Todo.Domain.Enums;

namespace Todo.Domain.Entities
{
	public class TodoTask : EntityBase
	{
		public string Title { get; set; }
		public TodoTaskStatus Status { get; set; }
		public int TodoListId { get; set; }
		public virtual TodoList TodoList { get; set; }
	}
}
