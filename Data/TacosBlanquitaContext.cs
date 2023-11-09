using System;
using System.Collections.Generic;
using BlanquitaAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BlanquitaAPI.Data;

public partial class TacosBlanquitaContext : DbContext
{
    public TacosBlanquitaContext()
    {
    }

    public TacosBlanquitaContext(DbContextOptions<TacosBlanquitaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<Orden> Ordens { get; set; }

    public virtual DbSet<OrdenCombo> OrdenCombos { get; set; }

    public virtual DbSet<Perfil> Perfils { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductoCombo> ProductoCombos { get; set; }

    public virtual DbSet<TipoProducto> TipoProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Tacos_Blanquita;Trusted_Connection=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.IdCombo).HasName("PK__Combo__D65BF2C815C03565");

            entity.ToTable("Combo");

            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.IdOrden).HasName("PK__Orden__C38F300D34BE0783");

            entity.ToTable("Orden");

            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Ordens)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orden__IdUsuario__46E78A0C");
        });

        modelBuilder.Entity<OrdenCombo>(entity =>
        {
            entity.HasKey(e => e.IdOrdenCombo).HasName("PK__OrdenCom__6BEFCB8BCB7F8FFB");

            entity.ToTable("OrdenCombo");

            entity.HasOne(d => d.IdComboNavigation).WithMany(p => p.OrdenCombos)
                .HasForeignKey(d => d.IdCombo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenComb__IdCom__4AB81AF0");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.OrdenCombos)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdenComb__IdOrd__49C3F6B7");
        });

        modelBuilder.Entity<Perfil>(entity =>
        {
            entity.HasKey(e => e.IdPerfil).HasName("PK__Perfil__C7BD5CC155B74450");

            entity.ToTable("Perfil");

            entity.Property(e => e.Clave)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__0988921066AB7130");

            entity.ToTable("Producto");

            entity.Property(e => e.Descripcion).HasMaxLength(50);

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Producto__IdTipo__3D5E1FD2");
        });

        modelBuilder.Entity<ProductoCombo>(entity =>
        {
            entity.HasKey(e => e.IdProductoCombo).HasName("PK__Producto__87A1E8325DD8FD27");

            entity.ToTable("ProductoCombo");

            entity.HasOne(d => d.IdComboNavigation).WithMany(p => p.ProductoCombos)
                .HasForeignKey(d => d.IdCombo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductoC__IdCom__403A8C7D");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoCombos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductoC__IdPro__412EB0B6");
        });

        modelBuilder.Entity<TipoProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto).HasName("PK__TipoProd__A974F920722C3432");

            entity.ToTable("TipoProducto");

            entity.Property(e => e.Clave).HasMaxLength(3);
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97ECAF2CBF");

            entity.ToTable("Usuario");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPerfilNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPerfil)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Usuario__IdPerfi__440B1D61");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
