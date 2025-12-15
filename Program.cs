using Microsoft.EntityFrameworkCore;
using Serilog;
using InventarioAPI.Middlewares;
using API_de_Inventario.Models;
using API_de_Inventario.Services;
using API_de_Inventario.DALs;

var builder = WebApplication.CreateBuilder(args);

// =======================
// SERILOG
// =======================
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

// =======================
// DATABASE
// =======================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =======================
// SERVICES
// =======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<IMovimientoService, MovimientoService>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();

var app = builder.Build();

// =======================
// MIDDLEWARES
// =======================
app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
