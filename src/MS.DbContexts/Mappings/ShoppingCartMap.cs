using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class ShoppingCartMap : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("shopping_cart");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Image).HasColumnName("image");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.DishId).HasColumnName("dish_id");
            builder.Property(e => e.SetmealId).HasColumnName("setmeal_id");
            builder.Property(e => e.DishFlavor).HasColumnName("dish_flavor");
            builder.Property(e => e.Number).HasColumnName("number");
            builder.Property(e => e.Amount).HasColumnName("amount");
            builder.Property(e => e.CreateTime).HasColumnName("create_time");
        }
    }
}
