using System.Text.Json.Serialization;

namespace OnlineShop.HttpModels.Models
{
    public class ProductResponse
    {        
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
        
        public ProductResponse() { }
        public ProductResponse(string name, string description, decimal price, Uri imagUri)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            ImagUri = imagUri;
        }

        public ProductResponse(Guid id, string name, string description, decimal price, Uri imagUri)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            ImagUri = imagUri;
        }
    }
}