using backend.Dtos;
using backend.Models;
using backend.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace backend.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<object> Login(UserRequest request)
        {
            var user = await _userRepository.GetByUsername(request.Username);
            if (user == null)
            {
                throw new AuthenticationException("Username doesn't exits");
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                throw new AuthenticationException("Wrong Password");
            }

            var token = CreateToken(user);

            return new { token, username = request.Username };
        }

        public async Task<int> Register(UserRequest request)
        {
            var existingName = await _userRepository.GetByUsername(request.Username);
            if (existingName != null)
            {
                throw new InvalidOperationException("Username already exist");
            }
            var user = new User()
            {
                Username = request.Username
            };
            var hashedPassword = new PasswordHasher<User>()
             .HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;
            return await _userRepository.Register(user);

        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("userId", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings: Issuer"),
                audience: _configuration.GetValue<string>("AppSettings: Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public int Authorize(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            var userIdClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("No user id found in token");
            }
            return int.Parse(userIdClaim);
        }
    }
}

            /*
            public async Task<string> GetAccessTokenAsync()
            {
            var tokenEndpoint = "https://login.microsoftonline.com/{tenantId}/oauth2/v2.0/token";
            var clientId = "your-client-id";
            var clientSecret = "your-client-secret";
            var scope = "https://graph.microsoft.com/.default";

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", client); 
        }
    }*/

