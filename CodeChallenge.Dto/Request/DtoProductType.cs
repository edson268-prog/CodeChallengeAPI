using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Dto.Request
{
    //public class DtoProductType
    //{
    //    public string? Name { get; set; }
    //    public string? Description { get; set; }
    //}

    public record DtoProductType
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
