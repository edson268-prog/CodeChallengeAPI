using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Entities
{
    public class Product : EntityBase
    {
        
        public string Name { get; set; }

        public string? Description { get; set; }

        //TODO: Ranges can be added also as a constraint in FluentAPI
        //ANSWER: Fluent validations were implemented instead of dataAnnotations
        //[Range(0, 100)]
        public int? AgeRestriction { get; set; }

        public string Company { get; set; }

        //[Range(1, 1000)]
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; } //FORANEO
        
        public bool SoldOut { get; set; }


        //TODO: There is a clean code practice to have Navigation properties on a separated section inside the class
        //so they can be easily identified
        //ANSWER: Good practice implemented
        //Navigation Properties
        public ProductType ProductType { get; set; }
    }

    public class DtoProductValidator : AbstractValidator<Product>
    {
        public DtoProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required");
            RuleFor(x => x.AgeRestriction).Must((age) => age >= 0 && age <= 100).WithMessage("Data out of range");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Company Name is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Product Price is required");
            RuleFor(x => x.Price).Must((price) => price >= 1 && price <= 1000).WithMessage("Data out of range");
        }
    }
}