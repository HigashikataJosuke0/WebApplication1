using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApplication1.auth;
using WebApplication1.config;
using WebApplication1.repository;
using WebApplication1.setvice;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// настройка swagger
services.AddSwaggerDocumentation();


// Добавляем контроллеры
services.AddControllers();
services.AddEndpointsApiExplorer();
var configuration = builder.Configuration;


// Добавляем репозитории
services.AddSingleton<RabbitMqService>(sp => 
    new RabbitMqService("amqp://guest:guest@localhost"));
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IRecordOfExecutionRepository, RecordOfExecutionRepository>();
services.AddScoped<IHabitsRepository, HabitsRepository>();
services.AddAutoMapper(typeof(HabitsProfile));

services.AddScoped<IJwtProvaider, JwtProvider>();
services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IUserService, UserService>();

services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
services.AddSingleton<JwtProvider>();

ApiExtensions.AddApiAuthentication(builder.Services,
    builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtOptions>>());

// Настройка подключения к PostgreSQL
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

var app = builder.Build();
var scope = app.Services.CreateScope();

//включаем swagger в приложении
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = string.Empty;
    });
}

UserEndPoints.MapEndpoints(app);
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.EnsureCreated();

// Конфигурация HTTP запроса
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.AddMappedEndPoints();

// Запуск приложения
app.Run();