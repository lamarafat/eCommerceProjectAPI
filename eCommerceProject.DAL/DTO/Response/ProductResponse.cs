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
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public string ImageUrl { get; set; }
        public string MainImageUrl => $"https://localhost:7131/images/{ImageUrl}"; 
    }
}
