using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerceProject.DAL.DTO.Response
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> SubImagesUrls { get; set; }
        public List<ReviewResponse> Reviews { get; set; } = new List<ReviewResponse>();
    }
}
