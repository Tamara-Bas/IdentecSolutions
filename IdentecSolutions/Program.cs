using FluentValidation;
using FluentValidation.AspNetCore;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Core.Queries.Dispatcher;
using IdentecSolutions.Application.Queries.GetAllEquipment;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Application.Validation;
using IdentecSolutions.EF;
using IdentecSolutions.EF.Repository;
using IdentecSolutions.EF.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//add services
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEquipmentByStatusHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEquipmentByIdHandler).Assembly));


builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped<IEquipmentServiceRepository, EquipmentService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(GetEquipmentByIdValidator));

//register Validator
builder.Services.AddValidatorsFromAssembly(typeof(GetEquipmentByIdValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//builder.Services.AddControllers().AddFluentValidation();



var app = builder.Build();

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
