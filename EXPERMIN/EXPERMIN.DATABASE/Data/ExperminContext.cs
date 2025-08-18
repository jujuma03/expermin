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

            // Banner 1:1 MediaFile
            modelBuilder.Entity<Banner>()
                .HasOne(b => b.MediaFile)
                .WithOne()
                .HasForeignKey<Banner>(b => b.MediaFileId)
                .OnDelete(DeleteBehavior.Restrict);

            // Producto 1:1 MediaFile
            modelBuilder.Entity<Product>()
                .HasOne(b => b.MediaFile)
                .WithOne()
                .HasForeignKey<Product>(b => b.MediaFileId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    // Aquí podrías setear CreatedBy desde el contexto del usuario
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    // Aquí podrías setear UpdatedBy desde el contexto del usuario
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
    }
}
