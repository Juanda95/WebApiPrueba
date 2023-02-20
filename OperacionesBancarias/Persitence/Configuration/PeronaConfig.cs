﻿using Domain.Common;
using Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persitence.Configuration
{
    public class PeronaConfig : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {           

            builder.HasKey(e => e.Id).HasName("PK__Persona__234er45676677");

            builder.Property(e => e.Direccion)
                .HasMaxLength(120)
                .IsRequired();


            builder.Property(e => e.Genero)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(e => e.Identificacion)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Telefono)
                .HasMaxLength(15)

                .IsRequired();

        }
    }
}
