using ClassService.Application.Interfaces;
using ClassService.Infrastructure.Persistence;
using ClassService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IClassRepository, ClassRepository>();

builder.Services.AddDbContext<ClassDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClassServiceDbConnectionString")));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
