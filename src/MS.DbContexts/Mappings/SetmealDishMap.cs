using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class SetmealDishMap : IEntityTypeConfiguration<SetmealDish>
    {
        public void Configure(EntityTypeBuilder<SetmealDish> builder)
        {
            builder.ToTable("setmeal_dish");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.SetmealId).HasColumnName("setmeal_id");
            builder.Property(e => e.DishId).HasColumnName("dish_id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Price).HasColumnName("price");
            builder.Property(e => e.Copies).HasColumnName("copies");
        }
    }
}
