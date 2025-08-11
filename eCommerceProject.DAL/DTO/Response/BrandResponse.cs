using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace eCommerceProject.DAL.DTO.Response
{
    public class BrandResponse
    {
        
        public string Name { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public string MainImageUrl => $"https://localhost:7131/images/{ImageUrl}";
    }
}
