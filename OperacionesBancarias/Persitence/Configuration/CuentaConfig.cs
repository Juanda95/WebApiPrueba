﻿using Domain.Common;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persitence.Configuration
{
    public class CuentaConfig : IEntityTypeConfiguration<Cuenta>
    {
        public void Configure(EntityTypeBuilder<Cuenta> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Cuentas__3214EC07546653EE");

            builder.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            builder.Property(e => e.NumeroCuenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Numero_cuenta");
            builder.Property(e => e.SaldoInicial)
                .HasColumnType("int")
                .HasColumnName("Saldo_inicial");
            builder.Property(e => e.TipoCuenta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tipo_cuenta");

            builder.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Cuenta_Cliente");
        }
    }
}
