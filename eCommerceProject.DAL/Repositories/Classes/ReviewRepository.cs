using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.Model.Reviews;
using eCommerceProject.DAL.Repositories.Interfaces;
using eCommerceProject.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.DAL.Repositories.Classes
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasUserReviewedProduct(string userId, int productId)
        {
           return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }
        public async Task AddReviewAsync(Review review, string userId)
        {
            review.UserId = userId;
            review.ReviewDate = DateTime.UtcNow;
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }
    }
}
