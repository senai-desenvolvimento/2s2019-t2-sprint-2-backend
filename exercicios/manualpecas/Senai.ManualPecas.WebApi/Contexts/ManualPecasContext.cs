using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.ManualPecas.WebApi.Domains
{
    public partial class ManualPecasContext : DbContext
    {
        public ManualPecasContext()
        {
        }

        public ManualPecasContext(DbContextOptions<ManualPecasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Fornecedores> Fornecedores { get; set; }
        public virtual DbSet<Pecas> Pecas { get; set; }
        public virtual DbSet<FornecedoresPecas> FornecedoresPecas { get; set; }


        // Unable to generate entity type for table 'dbo.ListaFornecedoresPecas'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=ManualPecas; Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<FornecedoresPecas>().HasKey(p => new { p.FornecedorId, p.PecaId });

            modelBuilder.Entity<FornecedoresPecas>()
            .HasOne(sc => sc.Fornecedor)
            .WithMany(sc => sc.ListaFornecedoresPecas)
            .HasForeignKey(sc => sc.FornecedorId);


            modelBuilder.Entity<FornecedoresPecas>()
                .HasOne(sc => sc.Peca)
                .WithMany(sc => sc.ListaFornecedoresPecas)
                .HasForeignKey(sc => sc.PecaId);

            modelBuilder.Entity<Fornecedores>(entity =>
            {
                entity.HasKey(e => e.FornecedorId);

                entity.HasIndex(e => e.Cnpj)
                    .HasName("UQ__Forneced__AA57D6B4B96F2AE2")
                    .IsUnique();

                entity.Property(e => e.Cnpj)
                    .IsRequired()
                    .HasColumnName("CNPJ")
                    .HasMaxLength(18)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Pecas>(entity =>
            {
                entity.HasKey(e => e.PecaId);

                entity.HasIndex(e => e.Codigo)
                    .HasName("UQ__Pecas__06370DAC9C650715")
                    .IsUnique();

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnType("text");
            });
        }
    }
}
