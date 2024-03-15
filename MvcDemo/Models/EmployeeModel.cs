using System.ComponentModel.DataAnnotations;

namespace MvcDemo.Models;

public class Employee : Person
{
    public string EmployeeID {get;set;}
    public int Age {get;set;}
}