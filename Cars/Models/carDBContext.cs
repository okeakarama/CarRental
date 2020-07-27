using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Cars.Models
{
    public class carDBContext : DbContext
    {
        public DbSet<car> cars { get; set; }
        public DbSet<log> logs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}