using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DL
{
    public partial class ESantiagoExamenMarzamContext : DbContext
    {
        public ESantiagoExamenMarzamContext()
        {
        }

        public ESantiagoExamenMarzamContext(DbContextOptions<ESantiagoExamenMarzamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Medicamento> Medicamentos { get; set; } = null!;
        public virtual DbSet<MedicamentoPedido> MedicamentoPedidos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.; Database= ESantiagoExamenMarzam; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamento)
                    .HasName("PK__Medicame__AC96376EBA6D4C2E");

                entity.ToTable("Medicamento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<MedicamentoPedido>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MedicamentoPedido");

                entity.Property(e => e.Total).HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.IdMedicamentoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdMedicamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Medicamen__IdMed__145C0A3F");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Medicamen__IdPed__1367E606");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PK__Pedido__9D335DC3D896D228");

                entity.ToTable("Pedido");

                entity.Property(e => e.Comprador)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Total).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF97BD3135BA");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F28456B8A509FA")
                    .IsUnique();

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
