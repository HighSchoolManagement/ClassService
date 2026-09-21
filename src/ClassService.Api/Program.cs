using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Classes.GetClasses;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Interfaces;
using ClassService.Infrastructure.Mapping;
using ClassService.Infrastructure.Persistence;
using ClassService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ClassService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ISchoolYearRepository, SchoolYearRepository>();
builder.Services.AddScoped<IRequestHandler<CreateClassCommand, CreateClassResponse>, CreateClassHandle>();
builder.Services.AddScoped<IRequestHandler<GetClassesQuery, PageResult<GetClassesResponse>>, GetClassesHandle>();
// ISchoolRepository gọi sang SchoolService qua HTTP, nên đăng ký bằng AddHttpClient
// (thay vì AddScoped) để dùng IHttpClientFactory, tránh socket exhaustion.
builder.Services.AddHttpClient<ISchoolRepository, SchoolHttpRepository>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["Services:SchoolService:BaseUrl"]!);
});
builder.Services.AddSchoolServiceClient(builder.Configuration);
// AutoMapper: quét assembly chứa các Profile (SchoolYearMappingProfile, ClassMappingProfile...).
builder.Services.AddAutoMapper(cfg => { },
    typeof(SchoolYearMappingProfile).Assembly);
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
