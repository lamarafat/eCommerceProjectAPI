using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceProject.DAL.DTO.Response
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public string FullName { get; set; }
    }
}
