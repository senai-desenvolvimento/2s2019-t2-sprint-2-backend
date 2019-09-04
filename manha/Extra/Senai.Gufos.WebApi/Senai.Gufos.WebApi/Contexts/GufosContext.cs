using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.Gufos.WebApi.Domains
{
    public partial class GufosContext : DbContext
    {
        public GufosContext()
        {
        }

        public GufosContext(DbContextOptions<GufosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Eventos> Eventos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        public virtual DbSet<Presencas> Presencas { get; set; }

        // Unable to generate entity type for table 'dbo.Presencas'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Gufos; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presencas>().HasKey(p => new { p.IdUsuario, p.IdEvento });

            modelBuilder.Entity<Presencas>()
            .HasOne<Usuarios>(sc => sc.Usuario)
            .WithMany(s => s.Presencas)
            .HasForeignKey(sc => sc.IdUsuario);


            modelBuilder.Entity<Presencas>()
                .HasOne<Eventos>(sc => sc.Evento)
                .WithMany(s => s.Presencas)
                .HasForeignKey(sc => sc.IdEvento);

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Categori__7D8FE3B255CE6A0B")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Eventos>(entity =>
            {
                entity.HasKey(e => e.IdEvento);

                entity.Property(e => e.Ativo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DataEvento).HasColumnType("datetime");

                entity.Property(e => e.Descricao).HasColumnType("text");

                entity.Property(e => e.Localizacao)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Eventos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Eventos__IdCateg__5165187F");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuarios__A9D10534CD78F315")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Permissao)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });
        }
    }
}
