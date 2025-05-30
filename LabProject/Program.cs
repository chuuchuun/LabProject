using LabProject.Infrastructure.Interfaces;
using LabProject.Infrastructure;
using Microsoft.OpenApi.Models;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Repositories;
using LabProject.Domain.Interfaces;
using LabProject.Application.Interfaces;
using LabProject.Application.Services;

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

builder.Services.AddScoped<IDbConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
builder.Services.AddScoped<IRepository<Discount>, DiscountRepository>();
builder.Services.AddScoped<IRepository<Location>, LocationRepository>();
builder.Services.AddScoped<IRepository<Payment>, PaymentRepository>();
builder.Services.AddScoped<IRepository<PaymentDiscount>, PaymentDiscountRepository>();
builder.Services.AddScoped<IRepository<Review>, ReviewRepository>();
builder.Services.AddScoped<IRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IRepository<Service>, ServiceRepository>();
builder.Services.AddScoped<IRepository<User>,  UserRepository>();

builder.Services.AddScoped<IBaseService<Appointment>, AppointmentService>();
builder.Services.AddScoped<IBaseService<Discount>, DiscountService>();
builder.Services.AddScoped<IBaseService<Location>, LocationService>();
builder.Services.AddScoped<IBaseService<Payment>, PaymentService>();
builder.Services.AddScoped<IBaseService<PaymentDiscount>, PaymentDiscountService>();
builder.Services.AddScoped<IBaseService<Review>, ReviewService>();
builder.Services.AddScoped<IBaseService<Role>, RoleService>();
builder.Services.AddScoped<IBaseService<Service>, ServiceService>();
builder.Services.AddScoped<IBaseService<User>, UserService>();

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
