using AutoMapper;
using MediatR;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Queries.GetAllTodos
{
	internal class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, Result<ICollection<TodoListInfoDTO>>>
	{
		private readonly ITodoListRepository _todoRepository;
		private readonly IMapper _mapper;

		public GetAllTodosQueryHandler(ITodoListRepository todoRepository, IMapper mapper)
		{
			_todoRepository = todoRepository;
			_mapper = mapper;
		}

		public Task<Result<ICollection<TodoListInfoDTO>>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
		{
			var lists = _todoRepository.GetAllWithIncludes(list => list.OwnerId == request.UserId);
			var mappedLists = _mapper.Map<List<TodoListInfoDTO>>(lists);

			var result = Result.Success<ICollection<TodoListInfoDTO>>(mappedLists);
			return Task.FromResult(result);

		}
	}
}
