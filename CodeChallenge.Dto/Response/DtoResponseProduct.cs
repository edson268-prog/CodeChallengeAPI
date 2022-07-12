namespace CodeChallenge.Dto.Response
{
    public class DtoResponseProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? AgeRestriction { get; set; }
        public string Company { get; set; }
        public decimal Price { get; set; }
        public string ProductTypeDesc { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductTypeId { get; set; }
        public bool SoldOut { get; set; }
    }
}
