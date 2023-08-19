using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;
using System;

namespace BakeryProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        public AuthController(IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
        }


        [HttpPost("Register")]
        public ActionResult Register(RegisterDTO registerDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User
                    {
                        UserName = registerDTO.UserName,
                        Email = registerDTO.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                        Bio = "",
                        BirthDate = registerDTO.BirthDate,
                        CreatedAT = DateTime.Now,
                        EmailConfirmed = false,
                        IsActive = false,
                        IsDeleted = false,
                        LastLoginDate = new DateTime(),
                        PhoneNumber = registerDTO.PhoneNumber,
                        Role = "",
                        PhoneNumberConfirmed = false,
                        Avatar = $"https://ui-avatars.com/api/?name={registerDTO.UserName}"
                    };
                    var check = _unitOfWork.User.FindIsExistByCondition(x => x.Email == registerDTO.Email);
                    if (check)
                    {
                        return BadRequest("This Email is Already Exist");
                    }
                    _unitOfWork.User.Insert(user);
                    _unitOfWork.Commit();
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState.Select(x => x.Value.Errors).Where(y => y.Count > 0).ToList());
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Login")]
        public ActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                // chcek if the user send request with empty email or pass exist 
                if (string.IsNullOrEmpty(loginDTO.Email) )
                {
                    return BadRequest("Please enter your email.");
                }
                else if (string.IsNullOrEmpty(loginDTO.Email))
                {
                    return BadRequest("Please enter your password.");
                }

                // chcek if the email exist 
                var user = _unitOfWork.User.FindByCondition(x => x.Email == loginDTO.Email);
                if(user is null) { return NotFound("The email you entered does not exist or is registered."); }

                // check password match
                var passValidate = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                if(!passValidate) { return BadRequest("Password not matching"); }

                // udpate user logged in date 
                user.IsActive = true;
                user.LastLoginDate = DateTime.Now;
                _unitOfWork.User.Update(user);
                _unitOfWork.Commit();

                // Generate token
                var token = _tokenGenerator.CreateToken(user);

                // Set the token in a cookie
                Response.Cookies.Append("AuthToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddHours(1) // Set the expiration time for the cookie
                });

                // Add the token to the response headers
                Response.Headers.Add("Authorization", "Bearer " + token);
                return Ok( new { Token= token });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Remove the authentication token cookie
            Response.Cookies.Delete("AuthToken");

            // Remove the Authorization header
            Response.Headers.Remove("Authorization");

            return Ok("Logged out successfully");
        }


        [HttpPost("Test")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Test()
        {

            return Ok("Logged out successfully");
        }

    }
}
