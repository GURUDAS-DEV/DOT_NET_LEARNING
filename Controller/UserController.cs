using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Controller
{
    [ApiController]
    [Route("Users")]
    public class UserController(AppDbContext _db) : ControllerBase
    {
        private readonly AppDbContext db = _db;

        [HttpGet("GetUser")]
        public async Task<IResult> GetAllUsers()
        {
            try{
                var Users = await db.Users.ToListAsync();
                if (Users == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(Users);
            }
            catch(Exception e){
                return Results.InternalServerError(new {Message = e.Message, Type = e.GetType().Name});
            }
        }

        [HttpGet("GetUser/{Id:int}", Name = "GetUserById")]
        public async Task<IResult> GetUserById(int Id)
        {
            try{
                var user = await db.Users.FindAsync(Id);
                if (user == null)
                    return Results.NotFound();

                return Results.Ok(user);
            }
            catch(Exception e){
                return Results.InternalServerError(new {Message = e.Message, Type = e.GetType().Name});
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IResult> CreateUser(CreateUser NewUser)
        {
            try
            {
                var ConstructedUser = new User
                {
                    Name = NewUser.Name,
                    Gmail = NewUser.Gmail,
                    Password = NewUser.Password
                };
                await db.AddAsync(ConstructedUser);
                await db.SaveChangesAsync();
                return Results.CreatedAtRoute(nameof(GetUserById), new { id = ConstructedUser.Id }, ConstructedUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType());
                return Results.InternalServerError(new { error = e.Message, type = e.GetType().Name });
            }
        }

        [HttpPut("UpdateUser/{Id:int}")]
        public async Task<IResult> UpdateUser(int Id, UpdateUser payload)
        {
            try
            {
                var user = await db.Users.FindAsync(Id);
                if (user == null) return Results.NotFound("Details Not Found!!");

                user.Name = payload.Name;
                user.Password = payload.Password;
                user.Gmail = payload.Gmail;

                await db.SaveChangesAsync();
                return Results.NoContent();
            }
            catch (Exception e)
            {
                return Results.InternalServerError(new { error = e.Message, Type = e.GetType().Name });
            }
        }

        [HttpDelete("DeleteUser/{Id:int}")]
        public async Task<IResult> DeleteUser(int Id)
        {
            try
            {
                var User = await db.Users.FindAsync(Id);
                if (User == null)
                    return Results.NotFound(new { Message = "User Not Found For Given Id" });

                db.Users.Remove(User);
                await db.SaveChangesAsync();

                return Results.NoContent();
            }
            catch (Exception e)
            {
                return Results.InternalServerError(new { error = e.Message, Type = e.GetType().Name });
            }
        }
    }
}
