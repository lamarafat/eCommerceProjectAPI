using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.Model.Reviews;

namespace eCommerceProject.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task<bool> HasUserReviewedProduct(string userId, int productId);
        Task AddReviewAsync(Review review, string userId);
    }
}
