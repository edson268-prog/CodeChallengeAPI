using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Entities
{
    public class Product : EntityBase
    {
        
        public string Name { get; set; }

        public string? Description { get; set; }

        //TODO: Ranges can be added also as a constraint in FluentAPI
        [Range(0, 100)]
        public int? AgeRestriction { get; set; }

        public string Company { get; set; }

        [Range(1, 1000)]
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; } //FORANEO
        
        public bool SoldOut { get; set; }


        //TODO: There is a clean code practice to have Navigation properties on a separated section inside the class
        //so they can be easily identified
        //Navigation Properties
        public ProductType ProductType { get; set; }
    }
}