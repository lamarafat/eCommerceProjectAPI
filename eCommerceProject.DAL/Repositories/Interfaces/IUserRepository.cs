using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model;

namespace eCommerceProject.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<ApplicationUser>> GetAllAsync();
        public Task<ApplicationUser> GetByIdAsync(string userId);
        Task<bool> BlockUserAsync(string userId, int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> isBlockUserAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, string roleName);

    }
}
