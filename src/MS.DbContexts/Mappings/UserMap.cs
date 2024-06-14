using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;


namespace MS.DbContexts
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.OpenId).HasColumnName("openid");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Phone).HasColumnName("phone");
            builder.Property(e => e.Sex).HasColumnName("sex");
            builder.Property(e => e.IdNumber).HasColumnName("id_number");
            builder.Property(e => e.Avatar).HasColumnName("avatar");
            builder.Property(e => e.CreateTime).HasColumnName("create_time");
        }
    }
}
/*
 * public class YourDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OpenId).HasColumnName("openid");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.IdNumber).HasColumnName("id_number");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.CreateTime).HasColumnName("create_time");
        });
    }
}
*/