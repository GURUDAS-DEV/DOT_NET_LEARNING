using System.ComponentModel.DataAnnotations;
using FirstProject.Dtos;
using FirstProject.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddValidation();
var app = builder.Build();

app.UserMapEndpoints();

app.Run();

