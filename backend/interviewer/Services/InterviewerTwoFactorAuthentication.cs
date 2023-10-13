using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using interviewer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace interviewer.Services;

public class InterviewerTwoFactorAuthentication : IUserTwoFactorTokenProvider<InterviewerUser>
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<InterviewerUser> _userManager;

    public InterviewerTwoFactorAuthentication(JwtSettings jwtSettings, UserManager<InterviewerUser> userManager)
    {
        _jwtSettings = jwtSettings;
        _userManager = userManager;
    }

    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<InterviewerUser>? manager, InterviewerUser? user)
    {
        if (manager != null && user != null)
        {
            return Task.FromResult(true);
        }
        else
        {
            return Task.FromResult(false);
        }
    }

    // Genereates a simple token based on the user id, email and another string.
    private string GenerateToken(InterviewerUser user, string purpose)
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

    public Task<string> GenerateAsync(string purpose, UserManager<InterviewerUser> manager, InterviewerUser user)
    {
        return Task.FromResult(GenerateToken(user, purpose));
    }

    public Task<bool> ValidateAsync(string purpose, string token, UserManager<InterviewerUser> manager,
        InterviewerUser user)
    {
        return Task.FromResult(token == GenerateToken(user, purpose));
    }
}