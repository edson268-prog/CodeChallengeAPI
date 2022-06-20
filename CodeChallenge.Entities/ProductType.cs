namespace CodeChallenge.Entities
{
    public class ProductType : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Properties
        public List<Product>? Products { get; set; }
    }
}