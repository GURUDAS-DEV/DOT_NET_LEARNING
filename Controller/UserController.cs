using FirstProject.Data;
using FirstProject.Dtos;
using FirstProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstProject.Controller
{
    [ApiController]
    [Route("Users")]
    public class UserController(AppDbContext _db) : ControllerBase
    {
        private readonly AppDbContext db = _db;

        [HttpGet("GetUserDetails")]    
        public async Task<IResult> GetAllUsers()
        {
            Console.WriteLine("Hello bro");
            var Users = await db.Users.ToListAsync();
            if(Users == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(Users);
        }

        [HttpGet("{Id:int}")]
        [ActionName("GetUser")]
        public async Task<IResult> GetUserById(int Id)
        {
            var user = await db.Users.FindAsync(Id);
            if(user == null)
                return Results.NotFound();

            return Results.Ok(user);   
        }

        [HttpPost]
        public async Task<IResult> CreateUser(CreateUser NewUser)
        {
            var ConstructedUser = new User
            {
                Name = NewUser.Name,
                Gmail = NewUser.Gmail,
                Password = NewUser.Password
            };
            await db.AddAsync(ConstructedUser);
            await db.SaveChangesAsync();
            return Results.CreatedAtRoute(nameof(GetUserById), new  {id = ConstructedUser.Id}, ConstructedUser );
        }
    }
}
