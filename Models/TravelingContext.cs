using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace travelApp.Models;

public partial class TravelingContext : DbContext
{
    public TravelingContext()
    {
    }

    public TravelingContext(DbContextOptions<TravelingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudad> Ciudades { get; set; }

    public virtual DbSet<TipoV> TiposV { get; set; }

    public virtual DbSet<Vehiculo> Vehiculos { get; set; }

    public virtual DbSet<Viaje> Viajes { get; set; }

    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-URF6P4H\\SQLEXPRESS;Initial Catalog=traveling;integrated security=true;TrustServerCertificate=True"); */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoV>(entity =>
        {
            entity.ToTable("TiposV");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Vehiculo>(entity =>
        {
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Patente)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Vehiculos)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Vehiculos_TiposV");
        });

        modelBuilder.Entity<Viaje>(entity =>
        {
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdCiudadNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdCiudad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viajes_Ciudades");

            entity.HasOne(d => d.IdVehiculoNavigation).WithMany(p => p.Viajes)
                .HasForeignKey(d => d.IdVehiculo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Viajes_Vehiculos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
