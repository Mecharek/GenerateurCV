using System;
using System.Data.Entity;
using System.Linq;

namespace GenerateurCV.Models
{
    public class MyContext : DbContext
    {
      
        public MyContext()
            : base("name=MyContext")
        {
        }

        public DbSet<CV> CVS { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<FormationDiplome> FormationDiplomes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loisir> Loisir { get; set; }
    }

  
}