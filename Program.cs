using FirstProject.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<Record> UserDetails = [
  new(1, "Gurudas", "Gurudas@gmail.com", "GurudasPassword"),  
  new(2, "Luffy", "Luffy@gmail.com", "LuffyPassword"),  
  new(3, "RoronoaZoro", "RoronoaZoro@gmail.com", "RoronoaZoroPassword"),  
  new(4, "Thorfinn", "Thorfinn@gmail.com", "ThorfinnPassword"),  
];


app.MapGet("/", () => "How are you! Hello Bro");

app.MapGet("/GetUserDetails", ()=>UserDetails);
app.MapGet("/GetUserDetails/{id}", (int id)=>UserDetails.Find(user => user.Id == id));

app.Run();

