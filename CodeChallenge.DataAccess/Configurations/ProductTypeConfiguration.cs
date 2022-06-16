using CodeChallenge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.DataAccess.Configurations
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Active)
                .HasDefaultValue(true);

            builder.HasQueryFilter(p => p.Active);

            builder.HasData(Get());
        }
        private ICollection<ProductType> Get()
        {
            return new List<ProductType>()
            {
                new ProductType() { Id = 1, Name = "Juguete de Baterias", Description = "El juguete usa baterias" },
                new ProductType() { Id = 2, Name = "Juguete de Plastico", Description = "El juguete esta fabricado de plastico" },
                new ProductType() { Id = 3, Name = "Juguete a Control Remoto", Description = "El juguete es manejado a travez de un control remoto" }
            };
        }
    }
}
