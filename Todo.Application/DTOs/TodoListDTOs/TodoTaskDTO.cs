using Todo.Domain.Enums;

namespace Todo.Application.DTOs.TodoListDTOs
{
	public class TodoTaskDTO
	{
		public string Title { get; set; }
		public TodoTaskStatus Status { get; set; }
	}
}
