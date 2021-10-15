using Data.Entities;
using Data.Entities.EntidadesNoPersistidas;
using Data.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.EF
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<EquipoMedicion> EquipoMedicion { get; set; }
        public DbSet<TipoLicencia> TipoLicencia { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<Licencia> Licencia { get; set; }
        public DbSet<TasaConversionTipoLicencia> TasaConversionTipoLicencia { get; set; }
        public DbSet<Establecimiento> Establecimiento { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<Plan> Plan { get; set; }
        public DbSet<PlanEstablecimiento> PlanEstablecimiento { get; set; }
        public DbSet<Ubicacion> Ubicacion { get; set; }
        public DbSet<Visita> Visita { get; set; }
        public DbSet<Irregularidad> Irregularidad { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Familia> Familia { get; set; }
        public DbSet<UsuarioFamilia> UsuarioFamilia { get; set; }
        public DbSet<Patente> Patente { get; set; }
        public DbSet<Partido> Partido { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<EstadoVisita> EstadoVisita { get; set; }
        public DbSet<PrecioLicencia> PrecioLicencia { get; set; }
        public DbSet<TotalizadoVisitasPorEstado> TotalizadoVisitasPorEstado { get; set; }

        public AplicationDbContext(DbContextOptions<AplicationDbContext> options):base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TotalizadoVisitasPorEstado>().HasNoKey();
            modelBuilder.Entity<PlanEstablecimiento>().HasKey(sc => new { sc.PlanId, sc.EstablecimientoId });
            modelBuilder.Entity<Familia>(entity =>
            {
                entity.HasKey(e => e.IdFamilia)
                    .HasName("PK__Familia__751F80CFB2BE0BA2");

                entity.ToTable("Familia", "Seguridad");

                entity.Property(e => e.IdFamilia)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<FamiliaFamilia>(entity =>
            {
                entity.HasKey(e => new { e.IdFamilia, e.IdFamiliaHijo })
                    .HasName("PK__Familia___ABFCC67E4660EA48");

                entity.ToTable("Familia_Familia", "Seguridad");

                entity.Property(e => e.IdFamilia)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.IdFamiliaHijo)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdFamiliaNavigation)
                    .WithMany(p => p.FamiliaFamiliaIdFamiliaNavigation)
                    .HasForeignKey(d => d.IdFamilia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Familia_F__IdFam__300424B4");

                entity.HasOne(d => d.IdFamiliaHijoNavigation)
                    .WithMany(p => p.FamiliaFamiliaIdFamiliaHijoNavigation)
                    .HasForeignKey(d => d.IdFamiliaHijo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Familia_A__Famil__37A5467C");
            });

            modelBuilder.Entity<FamiliaPatente>(entity =>
            {
                entity.HasKey(e => new { e.IdFamilia, e.IdPatente })
                    .HasName("PK__FamiliaE__166FEEA61367E606");

                entity.ToTable("Familia_Patente", "Seguridad");

                entity.Property(e => e.IdFamilia)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.IdPatente)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdFamiliaNavigation)
                    .WithMany(p => p.FamiliaPatente)
                    .HasForeignKey(d => d.IdFamilia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Familia_Patente_Familia");

                entity.HasOne(d => d.IdPatenteNavigation)
                    .WithMany(p => p.FamiliaPatente)
                    .HasForeignKey(d => d.IdPatente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FamiliaElement_Patente");
            });

            modelBuilder.Entity<Patente>(entity =>
            {
                entity.HasKey(e => e.IdPatente)
                    .HasName("PK__Patente__9F4EF95C6B5370D7");

                entity.ToTable("Patente", "Seguridad");

                entity.Property(e => e.IdPatente)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__5B65BF9736E01C24");

                entity.ToTable("Usuario", "Seguridad");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Contraseña).IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<UsuarioFamilia>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdFamilia })
                    .HasName("PK__Usuario___BC34479B236F871A");

                entity.ToTable("Usuario_Familia", "Seguridad");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.IdFamilia)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdFamiliaNavigation)
                    .WithMany(p => p.UsuarioFamilia)
                    .HasForeignKey(d => d.IdFamilia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario_P__Famil__35BCFE0A");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioFamilia)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario_F__IdUsu__33D4B598");
            });

            modelBuilder.Entity<UsuarioPatente>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdPatente });

                entity.ToTable("Usuario_Patente", "Seguridad");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.IdPatente)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .HasColumnName("timestamp")
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasOne(d => d.IdPatenteNavigation)
                    .WithMany(p => p.UsuarioPatente)
                    .HasForeignKey(d => d.IdPatente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Patente_Patente");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuarioPatente)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usuario_Patente_Usuario");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
