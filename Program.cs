using System.ComponentModel.DataAnnotations;
using FirstProject.Dtos;
using FirstProject.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UserMapEndpoints();

app.Run();

