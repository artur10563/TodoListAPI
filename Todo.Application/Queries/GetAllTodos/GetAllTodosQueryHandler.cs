using AutoMapper;
using MediatR;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Queries.GetAllTodos
{
	internal class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, Result<ICollection<TodoListDTO>>>
	{
		private readonly ITodoListRepository _todoRepository;
		private readonly IMapper _mapper;

		public GetAllTodosQueryHandler(ITodoListRepository todoRepository, IMapper mapper)
		{
			_todoRepository = todoRepository;
			_mapper = mapper;
		}

		public Task<Result<ICollection<TodoListDTO>>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
		{
			var lists = _todoRepository.GetAllWithIncludes();
			var mappedLists = _mapper.Map<List<TodoListDTO>>(lists);

			var result = Result.Success<ICollection<TodoListDTO>>(mappedLists);
			return Task.FromResult(result);

		}
	}
}
