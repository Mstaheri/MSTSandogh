﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Config
{
    internal class BankSafeTransactionsConfig : IEntityTypeConfiguration<BankSafeTransactions>
    {
        public void Configure(EntityTypeBuilder<BankSafeTransactions> builder)
        {
            builder.HasKey(p => p.Code);

            builder.Property(p => p.NameBankSafe)
                .HasMaxLength(50)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.AccountNumber)
                .HasMaxLength(16)
                .IsUnicode(true)
                .IsRequired(true);

            builder.Property(p => p.Deposit)
                .HasMaxLength(12)
                .IsRequired(true);

            builder.Property(p => p.Withdrawal)
                .HasMaxLength(12)
                .IsRequired(true);
        }

        
    }
}
