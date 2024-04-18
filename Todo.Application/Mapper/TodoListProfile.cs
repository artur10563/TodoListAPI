﻿using AutoMapper;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Domain.Entities;

namespace Todo.Application.Mapper
{
	public class TodoListProfile : Profile
	{
		public TodoListProfile()
		{
			CreateMap<TodoList, TodoListDTO>()
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
				.ForMember(dest => dest.OwnerInfo, opt => opt.MapFrom(src => src.Owner))
				.ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));

			CreateMap<TodoList, TodoListInfoDTO>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
				.ForMember(dest => dest.TasksCount, opt => opt.MapFrom(src => src.Tasks.Count));

		}
	}
}
