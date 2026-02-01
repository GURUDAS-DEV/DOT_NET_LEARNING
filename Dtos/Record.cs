using System.ComponentModel.DataAnnotations;

namespace FirstProject.Dtos;

public record Record(
    int Id,
    [Required][StringLength(20)] string Name, 
    [Required] string Gmail,
    [Required] string Password
);


public record CreateUser(
    [Required][StringLength(20)] string Name, 
    [Required] string Gmail,
    [Required] string Password
);


public record UpdateUser(
    [Required][StringLength(20)] string Name, 
    [Required] string Gmail,
    [Required] string Password
);
