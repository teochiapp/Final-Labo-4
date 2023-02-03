using WebApplicationLabo4.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplicationLabo4.Data
{
    
        public class ApplicationDbContext : IdentityDbContext
    {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            modelBuilder.Entity<JugadorClub>().HasKey(am => new
            {                
                am.idClub,
                am.idJugador
            });

            modelBuilder.Entity<JugadorClub>().HasOne(m => m.Club).WithMany(am => am.jugadorClub)
                .HasForeignKey(m => m.idClub);

            modelBuilder.Entity<JugadorClub>().HasOne(m => m.Jugador).WithMany(am => am.jugadorClub)
                .HasForeignKey(m => m.idJugador);
        }

        public DbSet<Jugadores> Jugadores { get; set; }
        public DbSet<Clubes> Clubes { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<JugadorClub> JugadorClub { get; set; }

    }
}
