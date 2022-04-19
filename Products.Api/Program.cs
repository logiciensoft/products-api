using Products.Api.Extensions;
using Products.Api.Middlewares;
using Products.Models;
using Products.Providers;
using Products.Providers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.ConfigureLogging(logging =>
{
    logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logs"));
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddSingleton<IAppHelper, AppHelper>();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings")
);

builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
