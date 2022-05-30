using Domain.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Data
{
    public class SqlDbContext : IdentityDbContext<ApplicationUser>
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RELEVE_EAU>()
                .HasKey(nameof(RELEVE_EAU.DATE_REL), nameof(RELEVE_EAU.NUM_CTR), nameof(RELEVE_EAU.CODCT));
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<COMPTEUR_H> compteurs_h { get; set; }
        public DbSet<RELEVE_EAU> releves_eau { get; set; }
    }
}
