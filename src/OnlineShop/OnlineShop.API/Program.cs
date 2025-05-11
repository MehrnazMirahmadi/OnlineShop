using OnlineShop.API.Data;
using OnlineShop.API.Middleware;
using OnlineShop.API.Repository;

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
app.Run();
