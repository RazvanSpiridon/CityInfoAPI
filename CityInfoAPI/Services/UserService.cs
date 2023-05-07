using CityInfoAPI.Context;
using CityInfoAPI.Entities;
using CityInfoAPI.Models.Authentication;
using CityInfoAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CityInfoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly CityInfoDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserService(CityInfoDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> LoginUser(UserLoginDto userLoginDto)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username) ?? new User();
            if (user.Username == string.Empty)
                return "User not found";

            if (!VerifyPasswordHash(userLoginDto.Password, user.PasswordHash, user.PasswordSalt))
                return "Wrong password";

            string token = CreateToken(user);
            
            return token;
        }


        public async Task<User> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var setRole = await _dbContext.Users.AnyAsync();
            var userRole = string.Empty;
            if (setRole == true)
                userRole = "User";
            else
                userRole = "Admin";

            CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserId = Guid.NewGuid(),
                Username = userRegisterDto.Username,
                UserRole = userRole,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        private string CreateToken(User user)
        {

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("SecretKey:MySecretKey").Value ?? "" ));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);


            var token = new JwtSecurityToken(
                issuer: _configuration["SecretKey:Issuer"],
                audience: _configuration["SecretKey:Audience"],
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
