using AutoMapper;
using MediatR;
using Todo.Application.DTOs.TodoListDTOs;
using Todo.Application.Repositories;
using Todo.Domain.Errors;
using Todo.Domain.Primitives;

namespace Todo.Application.CQ.TodoList.Queries.GetTodoListById
{
    internal class GetTodoListByIdQueryHandler : IRequestHandler<GetTodoListByIdQuery, Result<TodoListDTO>>
    {
        private readonly ITodoListRepository _todoRepository;
        private readonly IMapper _mapper;

        public GetTodoListByIdQueryHandler(ITodoListRepository todoRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<Result<TodoListDTO>> Handle(GetTodoListByIdQuery request, CancellationToken cancellationToken)
        {
            var list = await _todoRepository.GetByIdAsync(request.ListId);
            if (list == null)
            {
                return TodoListErrors.ListNotFound;
            }
            if (list.OwnerId != request.UserId)
            {
                return TodoListErrors.UserNotListOwner;
            }
            var listDTO = _mapper.Map<TodoListDTO>(list);
            return Result.Success(listDTO);
        }
    }
}
