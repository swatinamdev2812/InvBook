using InvBook.API.Middlewares;
using InvBook.Application.Common.Mapping;
using InvBook.Application.Interfaces;
using InvBook.Infrastructure.Persistence;
using InvBook.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {Message}{NewLine}{Exception}")
    .WriteTo.File("logs/api-log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IUploadService, UploadService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply database migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();  // Ensure all migrations are applied
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
