using Mapster;
using MediatR;
using Microsoft.Extensions.Options;
using OnlineShop.API;
using OnlineShop.API.Attributes;
using OnlineShop.API.Behaviours;
using OnlineShop.API.Data;
using OnlineShop.API.Features;
using OnlineShop.API.Helpers;
using OnlineShop.API.Middleware;
using OnlineShop.API.Proxies;
using OnlineShop.API.Repository;
using OnlineShop.API.ViewModel;
using OnlineShop.Application.Services;
using Polly;
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
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITrackingCodeProxy, TrackingCodeProxy>();

TypeAdapterConfig<User, UserViewModel>.NewConfig()
    .Map(dest => dest.FullName, src => $"{src.FirstName} {src.LastName}");

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
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

/*builder.Services.AddHttpClient<ITrackingCodeProxy, TrackingCodeProxy>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<Settings>>();
    client.BaseAddress = new Uri(options.Value.TrackingCode.BaseURL);
})
.AddPolicyHandler(Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .OrResult(r => !r.IsSuccessStatusCode)
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(2 * attempt),
        onRetry: (response, delay, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} after {delay.TotalSeconds}s due to {response.Exception?.Message ?? response.Result?.StatusCode.ToString()}");
        })); */

/***************Polly Policies for HttpClient****************/

var retryPolicy = Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(2));

var circuitBreakerPolicy = Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));

var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

builder.Services.AddHttpClient<ITrackingCodeProxy, TrackingCodeProxy>((serviceProvider, client) =>
{
    var options = serviceProvider.GetRequiredService<IOptions<Settings>>();
    client.BaseAddress = new Uri(options.Value.TrackingCode.BaseURL);
})
.AddPolicyHandler(Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy));
/*******************************/
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
                             && t.Namespace.Contains("OnlineShop.API.Enums")
                             && t.GetCustomAttributes(typeof(EnumEndpointAttribute), false).Length != 0);
//var enumTypes = Assembly.GetExecutingAssembly()
//                        .GetTypes()
//                        .Where(t => t.IsEnum)
//                        .ToList();


foreach (var enumType in enumTypes)
{
    var route = $"{enumType.Name}";
    var attribute = (enumType.GetCustomAttribute(typeof(EnumEndpointAttribute)) as EnumEndpointAttribute)!;
    app.MapGet(attribute.Route, () =>
    {
        var enumValues = Enum.GetValues(enumType).Cast<Enum>();
        var viewModel = enumValues.ToViewModel();
        var response = new EnumResponse<EnumViewModel>
        {
            Color = attribute.Color, 
            Values = viewModel
        };
        return Results.Ok(response);
    }).WithTags("Enums");

}

app.Run();
