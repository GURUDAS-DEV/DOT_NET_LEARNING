using System;

namespace FirstProject.Middleware;

public class Authentication
{
    private readonly RequestDelegate _next;

    public Authentication(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Started !!");
        await _next(context);
        Console.WriteLine("Ended !!");
    }
}
