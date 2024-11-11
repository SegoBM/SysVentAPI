using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProgramacionWebApiRest.Models.DB;

public partial class PuntoDeVentaWebContext : DbContext
{
    public PuntoDeVentaWebContext()
    {
    }

    public PuntoDeVentaWebContext(DbContextOptions<PuntoDeVentaWebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }


    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost; Database=PuntoDeVentaWeb; User Id=sa; Password=Expecto89$; Trusted_Connection=False; Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A74790D507");

            entity.Property(e => e.ClienteId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ClienteID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });



        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.PedidoId).HasName("PK__Pedidos__09BA14103C78D2F4");

            entity.Property(e => e.PedidoId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("PedidoID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID"); // Asegúrate de que esta línea esté presente
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83A58ECA3E");

            entity.Property(e => e.ProductoId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProductoID");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProveedorId).HasColumnName("ProveedorID");

        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.ProveedorId).HasName("PK__Proveedo__61266BB92025E93C");

            entity.Property(e => e.ProveedorId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("ProveedorID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE79899979E77");

            entity.Property(e => e.UsuarioId)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("UsuarioID");
            entity.Property(e => e.Contraseña).HasMaxLength(100);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Rol).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
