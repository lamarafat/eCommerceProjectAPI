﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceProject.DAL.DTO.Request
{
    public class ReviewRequest
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }

    }
}
