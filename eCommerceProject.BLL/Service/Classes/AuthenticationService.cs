using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Interfaces;
using eCommerceProject.DAL.DTO.Request;
using eCommerceProject.DAL.DTO.Response;
using eCommerceProject.DAL.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceProject.BLL.Service.Classes
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager, IConfiguration configuration, IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("Invalid email or password.");
            }
            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Email is not confirmed.");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or password.");
            }
            return new UserResponse
            {
                Token = await CreateTokenAsync(user),
            };
        }

        public async Task<string> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirmed successfully.";
            }
            return "Email confirmation failed. Please try again later.";
        }
        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var user = new ApplicationUser()
            {
                FullName = request.FullName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };
            var Result = await _userManager.CreateAsync(user, request.Password);
            if (Result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapedToken = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7131/api/Identity/Account/ConfirmEmail?token={escapedToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(user.Email, "Welcome", $"Hello {user.UserName},Please confirm your email by clicking here: <a href='{emailUrl}'>Confirm</a>");
                return new UserResponse
                {
                    //Email
                    Token = user.Email,
                };
            }
            else
            {
                throw new Exception(string.Join(", ", Result.Errors.Select(e => e.Description)));

            }
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim("Email", user.Email),
                new Claim("FullName", user.FullName),
                new Claim("Id", user.Id),
            };
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim("Role", role));
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwt")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: Claims,
                expires: DateTime.Now.AddDays(15),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            var random = new Random();
            var code = random.Next(1000, 9999).ToString();
            user.CodeResetPassword = code;
            user.CodeResetPasswordExpiration = DateTime.Now.AddMinutes(10);
            await _userManager.UpdateAsync(user);
            await _emailSender.SendEmailAsync(request.Email, "Reset Password", $"<p> Your reset code is: {code}. It will expire in 10 minutes.</p>");
            return true;
        }
        public async Task<bool> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                throw new Exception("User not found.");
            }
            if (user.CodeResetPassword != request.Code || user.CodeResetPasswordExpiration < DateTime.UtcNow) return false;
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (result.Succeeded)
            {
                await _emailSender.SendEmailAsync(request.Email, "Change Password", "<h1> Your password has been reset successfully.</h1>");
            }
            return true;
        }
    }
}
