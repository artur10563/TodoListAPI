﻿using Todo.Application.Repositories;
using Todo.Domain.Entities;
using Todo.Infrastructure.Data;
using Todo.Infrastructure.Repositories.Shared;

namespace Todo.Infrastructure.Repositories
{
    public class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
    {
        public TodoListRepository(AppDbContext context) : base(context) { }
    }
}