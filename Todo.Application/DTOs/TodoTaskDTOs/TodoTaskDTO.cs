using Todo.Domain.Enums;

namespace Todo.Application.DTOs.TodoTaskDTOs
{
    public class TodoTaskDTO
    {
        public string Title { get; set; }
        public TodoTaskStatus Status { get; set; }
    }
}
