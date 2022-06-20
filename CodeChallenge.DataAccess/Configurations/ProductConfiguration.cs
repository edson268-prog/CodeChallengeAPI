using CodeChallenge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeChallenge.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Conventions
            // DataAnnotations
            // FluentAPI

            builder.HasKey(x => x.Id);


            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(100);

            builder.Property(p => p.Company)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasPrecision(5, 2)
                .IsRequired();

            builder.Property(p => p.Active)
                .HasDefaultValue(true);

            builder.HasQueryFilter(p => p.Active);

            //Manual configuration of FK
            builder.HasOne(x => x.ProductType).WithMany(x => x.Products);          
            
            //builder.HasCheckConstraint("CK_RangeX", "")

            builder.HasData(Get());
        }
        private ICollection<Product> Get()
        {
            return new List<Product>()
            {
                new Product() { Id = 1, Name = "Optimus Prime", Description = "Juguete de robot autobot que hace sonidos", AgeRestriction = 0, Company = "Hasbro", Price = 100, ProductTypeId = 1 },
            };
        }
    }
}
