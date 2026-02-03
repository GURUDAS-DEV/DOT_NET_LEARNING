using System.ComponentModel.DataAnnotations;
using FirstProject.Data;
using FirstProject.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddControllers();
builder.Services.AddValidation();

var app = builder.Build();

app.MapControllers();

app.Run();

