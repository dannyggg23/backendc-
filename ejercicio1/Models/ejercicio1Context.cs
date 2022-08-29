using System;
using ejercicio1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Models
{
    public partial class ejercicio1Context : DbContext
    {
        public ejercicio1Context()
        {
        }

        public ejercicio1Context(DbContextOptions<ejercicio1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Cuentas> Cuentas { get; set; }
        public virtual DbSet<Movimientos> Movimientos { get; set; }
        public virtual DbSet<Personas> Personas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                String conn = MyConfig.GetValue<string>("ConnectionStrings:SqlConnection");

                optionsBuilder.UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("clientes");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(50);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdPersona).HasColumnName("id_persona");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_clientes_personas1");
            });

            modelBuilder.Entity<Cuentas>(entity =>
            {
                entity.HasKey(e => e.IdCuenta);

                entity.ToTable("cuentas");

                entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.NumeroCuenta)
                    .IsRequired()
                    .HasColumnName("numero_cuenta")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.SaldoInicial)
                    .HasColumnName("saldo_inicial")
                    .HasColumnType("decimal(12, 2)");

                entity.Property(e => e.TipoCuenta)
                    .IsRequired()
                    .HasColumnName("tipo_cuenta")
                    .HasMaxLength(15)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Movimientos>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);

                entity.ToTable("movimientos");

                entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");

                entity.Property(e => e.Saldo)
                    .HasColumnName("saldo")
                    .HasColumnType("decimal(12, 0)");

                entity.Property(e => e.TipoMovimiento)
                    .IsRequired()
                    .HasColumnName("tipo_movimiento")
                    .HasMaxLength(20);

                entity.Property(e => e.Valor)
                    .HasColumnName("valor")
                    .HasColumnType("decimal(12, 0)");
            });

            modelBuilder.Entity<Personas>(entity =>
            {
                entity.HasKey(e => e.IdPersona);

                entity.ToTable("personas");

                entity.Property(e => e.IdPersona).HasColumnName("id_persona");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasColumnName("direccion")
                    .HasMaxLength(50);

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Genero)
                    .HasColumnName("genero")
                    .HasMaxLength(20);

                entity.Property(e => e.Identificacion)
                    .HasColumnName("identificacion")
                    .HasMaxLength(13);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnName("telefono")
                    .HasMaxLength(15);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
