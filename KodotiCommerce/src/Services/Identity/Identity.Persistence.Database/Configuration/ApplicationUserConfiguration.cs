using Identity.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Persistence.Database.Configuration
{
    public class ApplicationUserConfiguration
    {
        public ApplicationUserConfiguration(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(r => r.Id);

            entityTypeBuilder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entityTypeBuilder.Property(u => u.LastName).IsRequired().HasMaxLength(100);

            entityTypeBuilder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
