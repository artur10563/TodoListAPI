using AutoMapper;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Mapper
{
	public class TodoTaskProfile : Profile
	{
		public TodoTaskProfile()
		{
			CreateMap<TodoTask, TodoTaskDTO>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status));
		}
	}
}
