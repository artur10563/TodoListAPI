using Todo.Application.DTOs.TodoTaskDTOs;
using Todo.Application.DTOs.UserDTOs;

namespace Todo.Application.DTOs.TodoListDTOs
{
    public class TodoListDTO
	{
		public string Title { get; set; }
		public UserInfoDTO OwnerInfo { get; set; }
		public ICollection<TodoTaskDTO> Tasks { get; set; }
	}
}
