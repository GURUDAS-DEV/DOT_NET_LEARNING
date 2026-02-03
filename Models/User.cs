
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstProject.Models;


public class User
{
    public int Id {get; set;}

    [Required]
    [Column(TypeName = "varchar(200)")]
    public string Name {get; set;} = null!; 

    [Required]
    public string Gmail {get; set;} = null!;

    [Required]
    public string Password {get; set;} = null!;

}
