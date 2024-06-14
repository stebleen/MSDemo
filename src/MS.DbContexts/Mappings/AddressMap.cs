using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.CampusCode).HasColumnName("campus_code");
            builder.Property(e => e.CampusName).HasColumnName("campus_name");
            builder.Property(e => e.BuildingCode).HasColumnName("building_code");
            builder.Property(e => e.BuildingName).HasColumnName("building_name");
        }
    }
}
