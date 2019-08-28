using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.Optus.WebApi.Domains
{
    public partial class OptusContext : DbContext
    {
        public OptusContext()
        {
        }

        public OptusContext(DbContextOptions<OptusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artistas> Artistas { get; set; }
        public virtual DbSet<Estilos> Estilos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Optus;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artistas>(entity =>
            {
                entity.HasKey(e => e.IdArtista);

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Artistas__7D8FE3B276E0256F")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdEstiloNavigation)
                    .WithMany(p => p.Artistas)
                    .HasForeignKey(d => d.IdEstilo)
                    .HasConstraintName("FK__Artistas__IdEsti__3B75D760");
            });

            modelBuilder.Entity<Estilos>(entity =>
            {
                entity.HasKey(e => e.IdEstilo);

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Estilos__7D8FE3B22D0D4522")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuarios__A9D1053422619414")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Permissao)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
