using TodoApp.Application;
using TodoApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dodanie DI z warstwy aplikacji i infrastruktury
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

// konfiguracja CORS - pozwolenie na dost�p z dowolnego �r�d�a (WebAPI i aplikacja kliencka mog� by� na r�nych serwerach)
builder.Services.AddCors(policy =>
{
	policy.AddPolicy("CorsPolicy", opt => opt
	.AllowAnyOrigin()
	.AllowAnyHeader()
	.AllowAnyMethod());
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

// konfiguracja CORS
app.UseCors("CorsPolicy");

// konfiguracja statycznych plik�w (np. dla front-endu)
app.UseStaticFiles();

app.Run();
