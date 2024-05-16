using Microsoft.EntityFrameworkCore;


using MvcDemo.Models;

namespace MvcDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}
        public DbSet<Person> Person {get; set;}
       public DbSet<Test1> Test1 {get; set;}
       
    }
   
}