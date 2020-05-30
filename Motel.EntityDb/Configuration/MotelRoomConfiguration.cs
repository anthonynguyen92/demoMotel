﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motel.EntityDb.Entities;

namespace Motel.EntityDb.Configuration
{
    public class MotelRoomConfiguration : IEntityTypeConfiguration<MotelRoom>
    {
        public void Configure(EntityTypeBuilder<MotelRoom> builder)
        {
            builder.HasKey(p => p.idMotel);
            builder.Property(m => m.BedRoom).IsRequired();
            builder.Property(m => m.Area).IsRequired();
            builder.Property(m => m.Status).IsRequired();
            builder.Property(m => m.Payment).IsRequired();
            builder.HasMany(m => m.InforBills).WithOne(i => i.MotelRoom);

            builder.HasOne(p => p.Rent).WithOne(r => r.MotelRoom).HasForeignKey<Rent>(p => p.idMotel);
        }
    }
}
