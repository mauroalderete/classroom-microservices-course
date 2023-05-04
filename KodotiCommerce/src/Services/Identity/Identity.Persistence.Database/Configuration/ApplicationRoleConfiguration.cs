using Identity.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Persistence.Database.Configuration
{
    public class ApplicationRoleConfiguration
    {
        public ApplicationRoleConfiguration(EntityTypeBuilder<ApplicationRole> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(r => r.Id);
            const string user = "Admin";
            entityTypeBuilder.HasData(new ApplicationRole
            {
                Id = Guid.NewGuid().ToString().ToLower(),
                Name = user,
                NormalizedName = user.ToUpper()
            });

            entityTypeBuilder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
