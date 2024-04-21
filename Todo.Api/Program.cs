using Todo.Api.Endpoints;
using Todo.Api.Extensions;
using Todo.Application;
using Todo.Infrastructure.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddStorage(builder.Configuration)
	.AddApplication()
	.AddJwtAuth(builder.Configuration)
	.AddSwaggerWithJwtAuth();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterAuthEndpoints();
app.RegisterUserEndpoints();
app.RegisterTodoListEndpoints();

app.Run();
