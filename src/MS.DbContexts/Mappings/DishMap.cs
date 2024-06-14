using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class DishMap : IEntityTypeConfiguration<Dish>
    {
        public void Configure(EntityTypeBuilder<Dish> builder)
        {
            builder.ToTable("dish");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.HasIndex(e => e.Name).IsUnique();//指定索引，不能重复
            builder.Property(e => e.CategoryId).HasColumnName("category_id");
            builder.Property(e => e.Price).HasColumnName("price");
            builder.Property(e => e.Image).HasColumnName("image");
            builder.Property(e => e.Description).HasColumnName("description");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.CreateTime).HasColumnName("create_time");
            builder.Property(e => e.UpdateTime).HasColumnName("update_time");
            builder.Property(e => e.CreateUser).HasColumnName("create_user");
            builder.Property(e => e.UpdateUser).HasColumnName("update_user");

        }
    }
}
