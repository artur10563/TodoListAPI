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

builder.Services.AddCors(options=> options.AddPolicy("AngularFrontEnd", policy=>
{
	policy.WithOrigins("http://localhost:4200")
	.AllowAnyMethod()
	.AllowAnyHeader();
}));

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

app.UseCors("AngularFrontEnd");

app.RegisterAuthEndpoints();
app.RegisterUserEndpoints();
app.RegisterTodoListEndpoints();

app.Run();
