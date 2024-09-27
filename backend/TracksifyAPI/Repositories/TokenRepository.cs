using Microsoft.AspNetCore.Identity;
using TracksifyAPI.Interfaces;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using TracksifyAPI.Models;

// Definition of the namespace for the repositories used by the TracksifyAPI
namespace TracksifyAPI.Repositories
{
    // Definition of a repository for handling tokens, implementing the ITokenRepository interface
    public class TokenRepository : ITokenRepository
    {
        // Declaring a private readonly field for the IConfiguration object
        private readonly IConfiguration _configuration;

        // Defining a constructor that takes an IConfiguration object and assigns it to the _configuration field
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Definition of a method for creating a JWT (JSON Web Token) for a given user
        public string CreateJWTToken(User user)
        {
            // Declaring a list of claims to be added to the JWT which includes addition of the user's email, ID and role as a claim
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));

            // Creating a symmetric security key from the JWT key in the configuration
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // Creating signing credentials using the symmetric security key and the HMAC SHA256 algorithm
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Creating a JWT with the issuer, audience, claims, expiration time, and signing credentials from the configuration
            var token = new JwtSecurityToken(
               _configuration["Jwt:Issuer"],
               _configuration["Jwt:Audience"],
               claims,
               expires: DateTime.UtcNow.AddDays(1),
               signingCredentials: credentials);

            // Writing the JWT as a string and returning it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

