using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Customer.Domain;

namespace Customer.Persistence.Database.Configuration
{
    public class ClientConfiguration
    {
        public ClientConfiguration(EntityTypeBuilder<Client> entityBuilder)
        {
            entityBuilder.HasIndex(p => p.ClientId);
            entityBuilder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            var clients = new List<Client>();

            for (var i = 1; i <= 20; i++)
            {
                clients.Add(new Client
                {
                    ClientId = i,
                    Name = $"Client {i}",
                });
            }

            entityBuilder.HasData(clients);
        }
    }
}
