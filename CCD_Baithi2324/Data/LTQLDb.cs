using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CCD_Baithi2324.Models;

    public class LTQLDb : DbContext
    {
        public LTQLDb (DbContextOptions<LTQLDb> options)
            : base(options)
        {
        }

        public DbSet<CCD_Baithi2324.Models.Person> Person { get; set; } = default!;
          public DbSet<CCD_Baithi2324.Models.EmPloyee> EmPloyee { get; set; } = default!;
            public DbSet<CCD_Baithi2324.Models.Student> student { get; set; } = default!;
    }
