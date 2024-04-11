using MediatR;
using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Domain.Primitives;

namespace Todo.Application.Queries.GetAllTodos
{
	internal class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, Result<ICollection<TodoList>>>
	{
		private readonly ITodoListRepository _todoRepository;

		public GetAllTodosQueryHandler(ITodoListRepository todoRepository)
		{
			_todoRepository = todoRepository;
		}

		public Task<Result<ICollection<TodoList>>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
		{

			var todos = _todoRepository.GetAll().ToList();
			var result = Result.Success<ICollection<TodoList>>(todos);
			return Task.FromResult(result);

		}
	}
}
