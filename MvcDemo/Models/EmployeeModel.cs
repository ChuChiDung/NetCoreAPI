using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcDemo.Models;
    [Table ("Employee")]
public class Employee : Person
{
    [Key]
    public string? EmployeeID {get;set;}
    public int Age {get;set;}
}