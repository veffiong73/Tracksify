using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TracksifyAPI.Dtos;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Mappers;
using TracksifyAPI.Models;

namespace TracksifyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    //implementing the interface of IUserRepository and ITokenRepository
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        public AuthController(IUserRepository UserRepository, ITokenRepository TokenRepository)
        {
            _userRepository = UserRepository;
            _tokenRepository = TokenRepository;
        }
        // implementing the login method which is a POST method 
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDTO.Email); //this a method in the userrepository
            if (user == null)
                return BadRequest("Invalid Credentials");

            var hashedPassword = user.Password;
            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, hashedPassword)) 
                return BadRequest("Invalid Credentials");

            //token creation using Json Web Tokens
            var token = _tokenRepository.CreateJWTToken(user);
            var userDto = user.ToUserDto();

            return Ok(new
            {
                message = "LogIn Successful",
                token,
                user = userDto
            });

        }
    }
}
