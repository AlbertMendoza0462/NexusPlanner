using Microsoft.EntityFrameworkCore;
using NexusPlanner.BLL;
using NexusPlanner.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<UsuarioBLL>();
builder.Services.AddScoped<ProyectoBLL>();
builder.Services.AddScoped<SolicitudBLL>();
builder.Services.AddScoped<TareaBLL>();

builder.Services.AddDbContext<Contexto>(
    con => con.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || 1==1)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
