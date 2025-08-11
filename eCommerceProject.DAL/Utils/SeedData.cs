using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model;
using eCommerceProject.DAL.Model.Brand;
using eCommerceProject.DAL.Model.Category;
using eCommerceProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Utilts
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManger;
        private readonly UserManager<ApplicationUser> _userManger;

        public SeedData(ApplicationDbContext context, RoleManager<IdentityRole> roleManger, UserManager<ApplicationUser> userManger)
        {
            _context = context;
            _roleManger = roleManger;
            _userManger = userManger;
        }
        public async Task DataSeedingAsync()
        {
            if (!(await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
                if (!await _context.CategoryAlts.AnyAsync())
                {
                    await _context.CategoryAlts.AddRangeAsync(
                        new CategoryAlt { Name = "Electronics" },
                        new CategoryAlt { Name = "Mobiles" }
                    );
                }
                if (!await _context.BrandAlts.AnyAsync())
                {
                    await _context.BrandAlts.AddRangeAsync(
                        new BrandAlt { Name = "Samsung" },
                        new BrandAlt { Name = "Apple" },
                        new BrandAlt { Name = "Nike" }
                    );
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!(await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
                if (!await _roleManger.Roles.AnyAsync())
                {
                    await _roleManger.CreateAsync(new IdentityRole { Name = "Admin" });
                    await _roleManger.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                    await _roleManger.CreateAsync(new IdentityRole { Name = "Customer" });
                }
                if (!await _userManger.Users.AnyAsync())
                {
                    var user1 = new ApplicationUser()
                    {
                        Email = "Lama@gmail.com",
                        FullName = "Lama",
                        UserName = "lama rafat",
                        PhoneNumber = "01012345678",
                        City = "Cairo",
                        Street = "Street 1",
                        EmailConfirmed = true,
                    };
                    var user2 = new ApplicationUser()
                    {
                        Email = "nemeh@gmail.com",
                        FullName = "Nemeh",
                        UserName = "nemeh fayyad",
                        PhoneNumber = "01012345678",
                        City = "Cairo",
                        Street = "Street 2",
                        EmailConfirmed = true,
                    };
                    var user3 = new ApplicationUser()
                    {
                        Email = "layal@gmail.com",
                        FullName = "Layal",
                        UserName = "layal rafat",
                        PhoneNumber = "01012345678",
                        City = "Cairo",
                        Street = "Street 3",
                        EmailConfirmed = true,
                    };

                    await _userManger.CreateAsync(user1, "La@000");
                    await _userManger.CreateAsync(user2, "Ly@000");
                    await _userManger.CreateAsync(user3, "na@000");

                    await _userManger.AddToRoleAsync(user1, "Admin");
                    await _userManger.AddToRoleAsync(user2, "SuperAdmin");
                    await _userManger.AddToRoleAsync(user3, "Customer");


                }
                //await _context.SaveChangesAsync();

            }
        }
    }
}
    
