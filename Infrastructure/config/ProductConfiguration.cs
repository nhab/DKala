﻿using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>

    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(180);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne(b => b.ProductBrands).WithMany().
            HasForeignKey(p => p.ProductBrandId);
            builder.HasOne(p => p.ProductTypes).WithMany().
            HasForeignKey(p => p.ProductTypeId);
        }
    }
}
