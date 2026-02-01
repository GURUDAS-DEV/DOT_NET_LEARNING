using FirstProject.Dtos;

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

        app.MapGet("/", () => "How are you! Hello Bro");

        app.MapGet("/GetUserDetails", () => UserDetails);
        app.MapGet("/GetUserDetails/{id:int}", (int id) =>
        {
            var user = UserDetails.Find(user => user.Id == id);
            if (user == null)
                return Results.NotFound();
            return Results.Ok(user);
        }).WithName(GetUserEndpointName);

        app.MapPost("/CreateUser", (CreateUser newUser) =>
        {
            Record newUserEntry = new(
            Id: ++Count,
            Name: newUser.Name,
            Gmail: newUser.Gmail,
            Password: newUser.Password
            );

            UserDetails.Add(newUserEntry);

            foreach (var ele in UserDetails)
            {
                Console.WriteLine(ele);
            }

            return Results.CreatedAtRoute(GetUserEndpointName, new { id = newUserEntry.Id }, newUserEntry);

        });

        app.MapPut("/UpdateUserDetails/{Id:int}", (int Id, UpdateUser payload) =>
        {
            var index = UserDetails.FindIndex((user) => user.Id == Id);
            UserDetails[index] = new Record(
              Id,
              Name: payload.Name,
              Gmail: payload.Gmail,
              Password: payload.Password
            );

            return Results.NoContent();
        });

        app.MapDelete("/DeleteUser/{Id:int}", (int Id) =>
        {
            int userIndex = UserDetails.RemoveAll((user) => user.Id == Id);

            return Results.NoContent();

        });
    }

}
