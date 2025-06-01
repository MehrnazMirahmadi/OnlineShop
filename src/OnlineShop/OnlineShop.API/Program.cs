using MediatR;
using Microsoft.OpenApi.Extensions;
using OnlineShop.API.Attributes;
using OnlineShop.API.Behaviours;
using OnlineShop.API.Data;
using OnlineShop.API.Features;
using OnlineShop.API.Helpers;
using OnlineShop.API.Middleware;
using OnlineShop.API.Repository;
using OnlineShop.Application.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// 🟢 Add services to the container

// DbContext
builder.Services.AddDbContext<OnlineShopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICityRepository,CityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
    options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// 🛠 Middleware setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseMiddleware<RateLimitMiddleware>();
app.MapGet("/Cities", async (IUnitOfWork unitOfWork, CancellationToken cancellationToken) =>
{
    var entities = await unitOfWork.cityRepository.GetListCitiesAsync(cancellationToken);
    return entities;
})
.WithTags("City");

var enumTypes = typeof(Program).Assembly
    .GetTypes()
    .Where(t => t.IsEnum
                && t.Namespace != null
                && t.Namespace.Contains("Enums")
                && t.GetCustomAttributes(typeof(EnumEndpointAttribute), false).Length != 0)
    .ToList();

foreach (var enumType in enumTypes)
{
    var attribute = (EnumEndpointAttribute)enumType.GetCustomAttribute(typeof(EnumEndpointAttribute))!;

    app.MapGet($"/Enums/{attribute.Route}", () =>
    {

        var enumValues = Enum.GetValues(enumType).Cast<Enum>();
  
        var viewModel = enumValues.ToViewModel();

        return BaseResult.Success();
    }).WithTags("Enums");
}


app.Run();
