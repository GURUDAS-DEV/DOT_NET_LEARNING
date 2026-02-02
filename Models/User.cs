
using System.ComponentModel.DataAnnotations;

namespace FirstProject.Models;


public class User
{
    public int Id {get; set;}

    [Required]
    public string Name {get; set;} = null!; 

    [Required]
    public string Gmail {get; set;} = null!;

    [Required]
    public string Password {get; set;} = null!;

}
