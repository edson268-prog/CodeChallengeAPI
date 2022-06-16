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
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        
        [Range(0, 100)]
        public int? AgeRestriction { get; set; }
        
        [Required]
        public string Company { get; set; }
        
        [Required]
        [Range(1, 1000)]
        public decimal Price { get; set; }
        
        public int ProductTypeId { get; set; }
        //public ProductType ProductType { get; set; }
    }
}
