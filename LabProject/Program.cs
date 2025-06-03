using LabProject.Infrastructure.Interfaces;
using LabProject.Infrastructure;
using Microsoft.OpenApi.Models;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Repositories;
using LabProject.Domain.Interfaces;
using LabProject.Application.Interfaces;
using LabProject.Application.Services;
using LabProject.Application.Profiles;
using LabProject.Application.Dtos.AppontmentDtos;
using LabProject.Application.Dtos.DiscountDtos;
using LabProject.Application.Dtos.LocationDtos;
using LabProject.Application.Dtos.ServiceDtos;
using LabProject.Application.Dtos.RoleDtos;
using LabProject.Application.Dtos.PaymentDiscountDtos;
using LabProject.Application.Dtos.ReviewDtos;
using LabProject.Application.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using LabProject.Presentation;
using System.Reflection;
using LabProject.Application.Features.Users;
using LabProject.Application.Features.Users.Commands;
using MediatR;
using LabProject.Application.Features.Users.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});
builder.Services.AddApplicationServiceRegistration();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetAllUsersQueryHandler).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .Select(x => new
            {
                Field = x.Key,
                Errors = x.Value?.Errors.Select(e => e.ErrorMessage)
            });

        return new BadRequestObjectResult(new { Errors = errors });
    };
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
