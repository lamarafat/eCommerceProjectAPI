using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace eCommerceProject.DAL.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string ? CodeResetPassword { get; set; }
        public DateTime? CodeResetPasswordExpiration { get; set; }

    }
}
