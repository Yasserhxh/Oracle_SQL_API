using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Authentication;
using Domain.Entities;

namespace Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<RELEVE_EAU>()
                .HasKey(nameof(RELEVE_EAU.DATE_REL), nameof(RELEVE_EAU.NUM_CTR), nameof(RELEVE_EAU.CODCT));
            modelBuilder.Entity<INSTALLATION>()
                .HasKey(nameof(INSTALLATION.NUM_INST),  nameof(INSTALLATION.CODCT));
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<COMPTEUR_H> compteurs_h { get; set; }
        public DbSet<RELEVE_EAU> releves_eau { get; set; }
        public DbSet<CENTRE> centres { get; set; }
        public DbSet<UTILISATEUR> utilisateurs { get; set; }
        public DbSet<INSTALLATION> installations { get; set; }
       // public DbSet<RELEVE_EAU> releves_eau { get; set; }

    }
}
