using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forenzika.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Personi> Personi { get; set; }
        public DbSet<Rasti> Rasti { get; set; }
        public DbSet<Foto> Foto { get; set; }
        public DbSet<Kategoria> Kategoria { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
