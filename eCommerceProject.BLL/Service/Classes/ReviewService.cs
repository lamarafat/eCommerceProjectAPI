using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.Model.Reviews;
using eCommerceProject.DAL.Repositories.Classes;
using eCommerceProject.DAL.Repositories.Interfaces;
using Mapster;

namespace eCommerceProject.BLL.Service.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly IOrderRepository _orderRepository;

        public ReviewService(ReviewRepository reviewRepository, IOrderRepository orderRepository)
        {
            _reviewRepository = reviewRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> AddReviewAsync(ReviewRequest request, string userId)
        {
            var hasOrder = await _orderRepository.UserHasApprovedOrderForProductAsync(userId, request.ProductId);
            if (!hasOrder) return false;

            var alreadyReviewed = await _reviewRepository.HasUserReviewedProduct(userId, request.ProductId);
            if (!alreadyReviewed) return false;
            var review = request.Adapt<Review>();
            await _reviewRepository.AddReviewAsync(review, userId);
            return true;
        }
    }
}
