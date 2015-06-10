using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LoginExample.Models
{
    public class UserProfile : IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }

    public class AppContext : IdentityDbContext<UserProfile>
    {
        public AppContext() : base("DefaultConnection") { }

        public static AppContext Create()
        {
            return new AppContext();
        }

        //public DbSet<UserProfile> UserProfile { get; set; }

        public DbSet<Estado> Estado { get; set; }
        public DbSet<Municipio> Municipio { get; set; }
    }
}
