namespace OnlineShop.Domain.Entities
{
    public class Product : IEntity
    {
        public Guid Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Uri ImagUri { get; set; }

        public Product(string name, string description, decimal price, Uri imagUri)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            ImagUri = imagUri;
        }

        private Product() { }
    }
}