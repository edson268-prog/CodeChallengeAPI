using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Dto.Request
{
    public class DtoProduct
    {
        //TODO: Take a look at fluentValidations for NotNull and InclusiveBetween
        //ANSWER: Fluent validations were implemented instead of dataAnnotations
        //RuleFor(x => x.Height).Must((model, height) => height >= model.Min && height <= model.Max);
        //[Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        
        //[Range(0, 100)]
        public int? AgeRestriction { get; set; }
        
        //[Required]
        public string Company { get; set; }
        
        //[Required]
        //[Range(1, 1000)]
        public decimal Price { get; set; }
        
        public int ProductTypeId { get; set; }
        //public ProductType ProductType { get; set; }
    }

    public class DtoProductValidator : AbstractValidator<DtoProduct>
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
