using NLog.Web;
using SimpleShop.Application;
using SimpleShop.Infrastructure;
using SimpleShop.WebApi.Middlewares;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);
builder.Logging.AddNLogWeb();

builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsPolicy", opt => opt
	.AllowAnyOrigin()
	.AllowAnyHeader()
	.AllowAnyMethod());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

StripeConfiguration.ApiKey = builder.Configuration["Stripe:ApiKey"];

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();