using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.DbContexts
{
    public class OrdersMap : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.ToTable("orders");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Number).HasColumnName("number");
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.UserId).HasColumnName("user_id");
            builder.Property(e => e.AddressBookId).HasColumnName("address_book_id");
            builder.Property(e => e.OrderTime).HasColumnName("order_time");
            builder.Property(e => e.CheckoutTime).HasColumnName("checkout_time");
            builder.Property(e => e.PayMethod).HasColumnName("pay_method");
            builder.Property(e => e.PayStatus).HasColumnName("pay_status");
            builder.Property(e => e.Amount).HasColumnName("amount");
            builder.Property(e => e.Remark).HasColumnName("remark");
            builder.Property(e => e.Phone).HasColumnName("phone");
            builder.Property(e => e.Address).HasColumnName("address");
            builder.Property(e => e.UserName).HasColumnName("user_name");
            builder.Property(e => e.Consignee).HasColumnName("consignee");
            builder.Property(e => e.CancelReason).HasColumnName("cancel_reason");
            builder.Property(e => e.RejectionReason).HasColumnName("rejection_reason");
            builder.Property(e => e.CancelTime).HasColumnName("cancel_time");
            builder.Property(e => e.EstimatedDeliveryTime).HasColumnName("estimated_delivery_time");
            builder.Property(e => e.DeliveryStatus).HasColumnName("delivery_status");
            builder.Property(e => e.DeliveryTime).HasColumnName("delivery_time");
            builder.Property(e => e.PackAmount).HasColumnName("pack_amount");
            builder.Property(e => e.TablewareNumber).HasColumnName("tableware_number");
            builder.Property(e => e.TablewareStatus).HasColumnName("tableware_status");
        }
    }
}
