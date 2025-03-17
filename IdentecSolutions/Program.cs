using FluentValidation;
using FluentValidation.AspNetCore;
using IdentecSolutions.Application.Core.Commands;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Core.Queries.Dispatcher;
using IdentecSolutions.Application.Queries.GetAllEquipment;
using IdentecSolutions.Application.Queries.GetEquipmentById;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Application.Services.ExceptionResponseMapper;
using IdentecSolutions.Application.Validation;
using IdentecSolutions.EF;
using IdentecSolutions.EF.Repository;
using IdentecSolutions.EF.UnitOfWork;
using IdentecSolutions.WebApi.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
//add services
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllEquipmentByStatusHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEquipmentByIdHandler).Assembly));


builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
//builder.Services.AddMediatR(typeof(Program));

builder.Services.AddScoped<IEquipmentServiceRepository, EquipmentService>();
builder.Services.AddScoped<IExceptionResponseMapper, ExceptionResponseMapperService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(GetEquipmentByIdValidator));

//validator
//builder.Services.AddControllers().AddFluentValidation();
//builder.Services.AddValidatorsFromAssemblyContaining<GetEquipmentByIdValidator>(); // Registers all validators

//builder.Services.AddValidatorsFromAssemblyContaining<GetEquipmentByIdValidator>(); // Specify the assembly containing your validators
// OR if validators are in another project:


builder.Services.AddValidatorsFromAssembly(typeof(GetEquipmentByIdValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

//builder.Services.AddControllers().AddFluentValidation();


builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); // ✅ Required for [SwaggerOperation] to work
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
}); 
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

//app.UseExceptionMiddelware();

app.Run();
