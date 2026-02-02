using System.Globalization;
using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Endpoints;

public static class UserEndpoint
{
    private static int Count = 4;
    private const string GetUserEndpointName = "GetUser";

    private static readonly List<Record> UserDetails = [
        new(1, "Gurudas", "Gurudas@gmail.com", "GurudasPassword"),
        new(2, "Luffy", "Luffy@gmail.com", "LuffyPassword"),
        new(3, "RoronoaZoro", "RoronoaZoro@gmail.com", "RoronoaZoroPassword"),
        new(4, "Thorfinn", "Thorfinn@gmail.com", "ThorfinnPassword"),
    ];



    public static void UserMapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/Users");

        group.MapGet("/", () => "How are you! Hello Bro");

        group.MapGet("/GetUserDetails", async(AppDbContext db) => {
            return await db.Users.ToListAsync();
        });
        
        group.MapGet("/GetUserDetails/{id:int}", async(int id, AppDbContext db) =>
        {
            var user = db.Users.Find(id);
            if(user == null)
                return Results.NotFound(new { Message = "User Not Found!!" });
            return Results.Ok(user);
        }).WithName(GetUserEndpointName);

        group.MapPost("/CreateUser", async (CreateUser newUser, AppDbContext db) =>
        {
            var newUserEntry = new User
            {
                Name = newUser.Name,
                Gmail = newUser.Gmail,
                Password = newUser.Password
            };

            db.Users.Add(newUserEntry);
            await db.SaveChangesAsync();

            return Results.CreatedAtRoute(GetUserEndpointName, new { id = newUserEntry.Id }, newUserEntry);

        });

        group.MapPut("/UpdateUserDetails/{Id:int}", async (int Id, UpdateUser payload, AppDbContext db) =>
        {
           var user = await db.Users.FindAsync(Id);
           if(user == null) return Results.NotFound("Details Not Found!!");

            user.Name = payload.Name;
            user.Password = payload.Password;
            user.Gmail = payload.Gmail;

            await db.SaveChangesAsync();
           return Results.NoContent();
        });

        group.MapDelete("/DeleteUser/{Id:int}", async (int Id, AppDbContext db) =>
        {

             var user = await db.Users.FindAsync(Id);
            if (user == null) return Results.NotFound();


            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });
    }

}
