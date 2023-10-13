﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using interviewer.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace interviewer.Services
{
    // UserService 实现
    public class UserService : IUserService<InterviewerUser>
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<InterviewerUser> _userManager;
        private readonly SignInManager<InterviewerUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly InterviewerDbContext _dbContext;
        private readonly IUserStore<InterviewerUser> _store;
        private readonly IPasswordHasher<InterviewerUser> _passwordHasher;

        public UserService(JwtSettings jwtSettings, UserManager<InterviewerUser> userManager,
            SignInManager<InterviewerUser> signInManager, IConfiguration configuration,
            IUserStore<InterviewerUser> store, IPasswordHasher<InterviewerUser> passwordHasher)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _store = store;
            _passwordHasher = passwordHasher;
            _dbContext = new InterviewerDbContext(_configuration);
        }

        public async Task<TokenResult> RegisterAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                return new TokenResult
                {
                    ErrorMessages = new[] { "用户已存在" },
                };
            }

            var newUser = new InterviewerUser() { UserName = username };
            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                return new TokenResult
                {
                    ErrorMessages = isCreated.Errors.Select(p => p.Description).ToArray()
                };
            }

            return GenerateJwtToken(newUser);
        }

        public async Task<TokenResult> WeChatLoginAsync(string id)
        {
            using SHA256 sha256 = SHA256.Create();

            string username = "wx_";

            // 计算给定字符串的哈希值
            byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(id));
            // 将字节数组转换为字符串格式
            foreach (byte b in hashValue)
            {
                username += $"{b:X2}";
            }

            hashValue = sha256.ComputeHash(
                Encoding.UTF8.GetBytes(id + _configuration["Authentication:WeChat:PasswordSecret"]));
            var password = "wx@";
            foreach (byte b in hashValue)
            {
                password += $"{b:X2}";
            }

            var existingUser = await _userManager.FindByIdAsync(id);
            if (existingUser != null)
            {
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
                if (!isCorrect)
                {
                    return new TokenResult
                    {
                        ErrorMessages = new[] { "用户名或密码错误" },
                    };
                }

                return GenerateJwtToken(existingUser);
            }

            var newUser = new InterviewerUser() { UserName = username, Id = id };
            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                return new TokenResult
                {
                    ErrorMessages = isCreated.Errors.Select(p => p.Description).ToArray()
                };
            }

            var isAdded = await _userManager.AddToRoleAsync(newUser, "Student");
            if (!isAdded.Succeeded)
            {
                return new TokenResult()
                {
                    ErrorMessages = isAdded.Errors.Select(p => p.Description).ToArray()
                };
            }

            return GenerateJwtToken(newUser);
        }

        public async Task<TokenResult> RegisterStudentAsync([RegularExpression(
                @"^(13[0-9]|14[01456879]|15[0-35-9]|16[2567]|17[0-8]|18[0-9]|19[0-35-9])\d{8}$",
                ErrorMessage = "手机号格式不正确")]
            string phoneNumber, string password)
        {
            string username = "stu_" + phoneNumber;
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
            {
                return new TokenResult
                {
                    ErrorMessages = new[] { "用户已存在" },
                };
            }

            var newUser = new InterviewerUser() { UserName = username };
            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                return new TokenResult
                {
                    ErrorMessages = isCreated.Errors.Select(p => p.Description).ToArray()
                };
            }

            await _userManager.AddToRoleAsync(newUser, "Student");
            return GenerateJwtToken(newUser);
        }

        public async Task<TokenResult> LoginAsync(string username, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
            {
                return new TokenResult
                {
                    ErrorMessages = new[] { "用户不存在" },
                };
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!isCorrect)
            {
                return new TokenResult
                {
                    ErrorMessages = new[] { "用户名或密码错误" },
                };
            }

            return GenerateJwtToken(existingUser);
        }

        public async Task<EditResult> ResetPasswordAsync(string username, string newPassword)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
            {
                return new EditResult
                {
                    ErrorMessages = new[] { "用户不存在" },
                };
            }

            existingUser.PasswordHash = _passwordHasher.HashPassword(existingUser, newPassword);
            var identityResult = await _userManager.UpdateAsync(existingUser);
            
            if (!identityResult.Succeeded)
            {
                return new EditResult
                {
                    ErrorMessages = identityResult.Errors.Select(p => p.Description).ToArray()
                };
            }
            
            return new EditResult();
        }

        public Task<TokenResult> WeChatRegisterAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ExternalLoginInfo?> GetExternalLoginInfoAsync()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignInAsync(ExternalLoginInfo info)
        {
            return await _signInManager.ExternalLoginSignInAsync(
                info.LoginProvider, info.ProviderKey, false);
        }

        public async Task<IdentityResult> CreateUser(InterviewerUser user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> AddLoginAsync(InterviewerUser user, ExternalLoginInfo info)
        {
            return await _userManager.AddLoginAsync(user, info);
        }

        public async Task<EditResult> AddToRoleAsync(string username, string password, string role)
        {
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser == null)
            {
                return new EditResult
                {
                    ErrorMessages = new[] { "用户不存在" },
                };
            }

            var isCorrect = await _userManager.CheckPasswordAsync(existingUser, password);
            if (!isCorrect)
            {
                return new EditResult
                {
                    ErrorMessages = new[] { "用户名或密码错误" },
                };
            }

            var isAdded = await _userManager.AddToRoleAsync(existingUser, role);
            if (!isAdded.Succeeded)
            {
                return new EditResult
                {
                    ErrorMessages = isAdded.Errors.Select(p => p.Description).ToArray()
                };
            }

            if (role == "Interviewer")
            {
                _dbContext.Interviewers.Add(new Interviewer
                {
                    Name = username,
                    Department = ElcDepartment.All,
                    Id = existingUser.Id
                });
                await _dbContext.SaveChangesAsync();
            }

            return new EditResult();
        }

        private string GenerateToken(InterviewerUser user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecurityKey);
            var roleClaims = new List<Claim>();
            foreach (var role in _userManager.GetRolesAsync(user).Result)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                }.Concat(roleClaims)),
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.Add(_jwtSettings.ExpiresIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(securityToken);
            return token;
        }

        private TokenResult GenerateJwtToken(InterviewerUser user)
        {
            var token = GenerateToken(user);
            return new TokenResult
            {
                AccessToken = token,
                TokenType = "Bearer"
            };
        }
    }
}