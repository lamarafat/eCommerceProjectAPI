using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;

namespace eCommerceProject.BLL.Service.Interfaces
{
    public interface IAuthenticationService
    {
        Task <UserResponse> RegisterAsync(RegisterRequest request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<UserResponse> LoginAsync(LoginRequest request);

        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);
    }
}
