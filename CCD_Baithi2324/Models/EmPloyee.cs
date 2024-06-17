
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCD_Baithi2324.Models;
[Table("EmPloyee")]
public class EmPloyee : Person{
    [Key]
    public string Ten { get; set;}
    public string TenName { get; set;}
}
