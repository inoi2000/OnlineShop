using System.Text.Json.Serialization;

namespace OnlineShopHttpApiClient.Models
{
    public class Product
    {
        public Product() { }
        public Product(string name, string description, decimal price, Uri imagUri)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            ImagUri = imagUri;
        }

        public Product(Guid id, string name, string description, decimal price, Uri imagUri)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImagUri = imagUri;
        }
        
        [JsonPropertyName("id")]
        public Guid Id { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("imagUri")]
        public Uri ImagUri { get; set; }
    }
}