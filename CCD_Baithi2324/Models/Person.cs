using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CCD_Baithi2324.Models;
[Table ("Person")]
public class Person {
    [Key]
    public int Id { get; set;}
    public string Name { get; set;}
    public char Tuoi { get; set;}

}