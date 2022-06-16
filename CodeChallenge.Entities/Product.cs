using System.ComponentModel.DataAnnotations;

namespace CodeChallenge.Entities
{
    public class Product : EntityBase
    {
        //[MaxLength(50)]
        public string Name { get; set; }

        //[MaxLength(100)]
        public string? Description { get; set; }

        [Range(0, 100)]
        public int? AgeRestriction { get; set; }

        //[MaxLength(50)]
        public string Company { get; set; }

        [Range(1, 1000)]
        public decimal Price { get; set; }
        public int ProductTypeId { get; set; } //FORANEO
        public ProductType ProductType { get; set; }
        public bool SoldOut { get; set; }
    }
}