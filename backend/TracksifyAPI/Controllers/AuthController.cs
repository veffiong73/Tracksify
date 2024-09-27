using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TracksifyAPI.Dtos.Login;
using TracksifyAPI.Dtos.User;
using TracksifyAPI.Interfaces;
using TracksifyAPI.Mappers;

namespace TracksifyAPI.Controllers
{
    [Route("tracksify/[controller]")]
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

        [HttpPost]
        [Route("ChangePassword")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            // Retrieving user based on the current user's identity 
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return BadRequest("Unauthorized");

            var userId = Guid.Parse(userIdClaim.Value);

            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
                return NotFound("User not found");

            // Verifying the old password
            var oldPassword = changePasswordDTO.OldPassword;
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                return BadRequest("Old password is incorrect");

            // Hashing and updating the new password
            var newPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDTO.NewPassword);
            user.Password = newPassword;

            // Updating the user in the repository
            await _userRepository.UpdateUserAync(userId, new UpdateUserDto { });

            return Ok(new
            {
                message = "Password changed successfully"
            });
        }
    }
}
