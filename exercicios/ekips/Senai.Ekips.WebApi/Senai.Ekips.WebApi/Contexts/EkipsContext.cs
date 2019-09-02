using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.Ekips.WebApi.Domains
{
    public partial class EkipsContext : DbContext
    {
        public EkipsContext()
        {
        }

        public EkipsContext(DbContextOptions<EkipsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Departamentos> Departamentos { get; set; }
        public virtual DbSet<Funcionarios> Funcionarios { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=Ekips;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cargos>(entity =>
            {
                entity.HasKey(e => e.CargoId);

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Cargos__7D8FE3B2C75FB87A")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Departamentos>(entity =>
            {
                entity.HasKey(e => e.DepartamentoId);

                entity.HasIndex(e => e.Nome)
                    .HasName("UQ__Departam__7D8FE3B262892A4A")
                    .IsUnique();

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Funcionarios>(entity =>
            {
                entity.HasKey(e => e.FuncionarioId);

                entity.HasIndex(e => e.Cpf)
                    .HasName("UQ__Funciona__C1F89731A963F5D8")
                    .IsUnique();

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("UQ__Funciona__2B3DE7B94C683347")
                    .IsUnique();

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Salario).HasColumnType("money");

                entity.HasOne(d => d.Cargo)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.CargoId)
                    .HasConstraintName("FK__Funcionar__Cargo__48CFD27E");

                entity.HasOne(d => d.Departamento)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.DepartamentoId)
                    .HasConstraintName("FK__Funcionar__Depar__47DBAE45");

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.Funcionarios)
                    .HasForeignKey<Funcionarios>(d => d.UsuarioId)
                    .HasConstraintName("FK__Funcionar__Usuar__49C3F6B7");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.UsuarioId);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuarios__A9D10534FA6F374A")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Permissao)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
        }
    }
}
