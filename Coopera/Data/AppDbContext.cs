using Coopera.Models;
using Microsoft.EntityFrameworkCore;

namespace Coopera.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }

        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<MiniJuego> MiniJuegos { get; set; }
        public DbSet<Recurso> Regursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relaciones uno a muchos
            modelBuilder.Entity<MiniJuego>()
                .HasOne(m => m.Recursos)
                .WithMany(r => r.MiniJuegos)
                .HasForeignKey(m => m.IdRecurso);

            modelBuilder.Entity<MiniJuego>()
                .HasOne(m => m.Jugador)
                .WithMany(j => j.MiniJuegos)
                .HasForeignKey(m => m.JugadorId);

            modelBuilder.Entity<MiniJuego>()
                .HasOne(m => m.Partida)
                .WithMany(p => p.MiniJuegos)
                .HasForeignKey(m => m.IdPartida);

            modelBuilder.Entity<Recurso>()
                .HasOne(r => r.Partida)
                .WithMany(p => p.Recursos)
                .HasForeignKey(r => r.PartidaId);

            modelBuilder.Entity<Jugador>()
                .HasOne(j => j.Partida)
                .WithMany(p => p.Jugadores)
                .HasForeignKey(j => j.PartidaId);
        }
    }
}
