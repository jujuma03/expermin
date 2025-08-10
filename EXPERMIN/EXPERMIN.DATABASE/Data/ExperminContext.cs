using EXPERMIN.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.DATABASE.Data
{
    public class ExperminContext : IdentityDbContext<
                            ApplicationUser,
                            ApplicationRole,
                            string,
                            IdentityUserClaim<string>,
                            ApplicationUserRole,
                            IdentityUserLogin<string>,
                            IdentityRoleClaim<string>,
                            IdentityUserToken<string>>
    {
        public ExperminContext(DbContextOptions<ExperminContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().Property(c => c.Email).HasMaxLength(255);

            // Configurar la clave primaria compuesta de UserRole
            modelBuilder.Entity<ApplicationUserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Configurar las relaciones para evitar problemas de duplicación
            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<RevokedToken> RevokedTokens { get; set; }
    }
}
