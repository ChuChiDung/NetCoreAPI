using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcDemo.Models;
    [Table ("Test1")]
public class Test1

{
    [Key]
    public string? Test1ID {get;set;}
    public int Age {get;set;}
    public decimal Price { get; set; }
}