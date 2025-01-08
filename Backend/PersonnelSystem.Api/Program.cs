using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using PersonnelSystem.Api;
using PersonnelSystem.Application;
using PersonnelSystem.Application.Services;
using PersonnelSystem.Data;
using PersonnelSystem.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

//ÊÎÐÑ
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//ÁÄ
builder.Services.AddDbContext<PersonnelDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Ñåðâèñû
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ISubdivisionRepository, SubdivisionRepository>();
builder.Services.AddScoped<IFindService, FindService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDateRepository, DateRepository>();
builder.Services.AddScoped<IDateService, DateService>();



var app = builder.Build();

app.UseCors("AllowReactApp");

app.UseMiddleware<AuthMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
