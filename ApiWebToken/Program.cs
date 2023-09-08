using Persistencia;
using Microsoft.EntityFrameworkCore;
using ApiWebToken.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureCors();

builder.Services.AddDbContext<ApiWebTokenContext>(options=>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMySql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
//Authentication always first than Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
