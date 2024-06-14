using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class AddressBookMap : IEntityTypeConfiguration<AddressBook>
    {
        public void Configure(EntityTypeBuilder<AddressBook> builder)
        {
            builder.ToTable("address_book");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.Consignee).HasColumnName("consignee");
            builder.Property(e => e.Sex).HasColumnName("sex");
            builder.Property(e => e.Phone).HasColumnName("phone");
            builder.Property(e => e.Domitory).HasColumnName("domitory");
            builder.Property(e => e.IsDefault).HasColumnName("is_default")
                    .HasConversion<bool>(); // 数据库中是tinyint类型，转换为C# bool类型
            builder.Property(e => e.AddressId).HasColumnName("address_id");
        }
    }
}
