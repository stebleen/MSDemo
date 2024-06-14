using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("employee");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Name).HasColumnName("name");
            builder.Property(e => e.Username).HasColumnName("username");
            builder.Property(e => e.Password).HasColumnName("password");
            builder.Property(e => e.Phone).HasColumnName("phone");
            builder.Property(e => e.Sex).HasColumnName("sex");
            builder.Property(e => e.IdNumber).HasColumnName("id_number");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.CreateTime).HasColumnName("create_time");
            builder.Property(e => e.UpdateTime).HasColumnName("update_time");
            builder.Property(e => e.CreateUser).HasColumnName("create_user");
            builder.Property(e => e.UpdateUser).HasColumnName("update_user");
            builder.Property(e => e.AddressId).HasColumnName("address_id");

            builder.HasIndex(e => e.Username).IsUnique();
        }
    }
}
