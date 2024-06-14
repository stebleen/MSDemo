using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;

namespace MS.DbContexts
{
    internal class DishFlavorMap : IEntityTypeConfiguration<DishFlavor>
    {
        public void Configure(EntityTypeBuilder<DishFlavor> builder)
        {
            builder.ToTable("dish_flavor");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.DishId).HasColumnName("dish_id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Value).HasColumnName("value");
        }
    }
}
