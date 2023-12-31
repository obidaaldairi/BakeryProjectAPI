﻿using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.PortableExecutable;
using System;
using System.Runtime.Intrinsics.X86;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DataAccess.Implementation;

namespace BakeryProjectAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailSender _emailSender;
        private readonly ICloudinaryServices _cloudinaryServices;


        public AuthController(IUnitOfWork unitOfWork
            , ITokenGenerator tokenGenerator
            , IEmailSender emailSender
            , ICloudinaryServices cloudinaryServices)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _emailSender = emailSender;
            _cloudinaryServices = cloudinaryServices;
        }


        [HttpPost("Register")]
        public ActionResult Register(RegisterDTO registerDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Save in User Table
                    var user = new User
                    {
                        ArabicUserName = registerDTO.EnglishUserName,
                        EnglishUserName = registerDTO.EnglishUserName,
                        Email = registerDTO.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                        ArabicBio = "",
                        EnglishBio = "",
                        BirthDate = registerDTO.BirthDate,
                        CreatedAt = DateTime.Now,
                        EmailConfirmed = false,
                        IsActive = false,
                        IsDeleted = false,
                        LastLoginDate = new DateTime(),
                        PhoneNumber = registerDTO.PhoneNumber,
                        PhoneNumberConfirmed = false,
                        Avatar = $"https://ui-avatars.com/api/?name={registerDTO.EnglishUserName}&length=1",
                    };
                    var check = _unitOfWork.User.FindIsExistByCondition(x => x.Email == registerDTO.Email);
                    if (check)
                    {
                        return BadRequest("This Email is Already Exist");
                    }
                    _unitOfWork.User.Insert(user);
                    _unitOfWork.Commit();

                    // Save in User Roles Table
                    _unitOfWork.UserRole.Insert(new UserRole
                    {
                        UserId = user.ID,
                        RoleId = _unitOfWork.Role.FindByCondition(x=>x.EnglishRoleName == "Member").ID,
                    });
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
                else if (string.IsNullOrEmpty(loginDTO.Password))
                {
                    return BadRequest("Please enter your password.");
                }

                // chcek if the email exist 
                var user = _unitOfWork.User.FindByCondition(x => x.Email == loginDTO.Email);
                //if(user is null) { return NotFound("The email you entered does not exist or is registered."); }
                if (user is null) { return NotFound("Your email is incorrect."); }

                // check password match
                var passValidate = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                if(!passValidate) { return BadRequest("Password not matching"); }


                // Generate token
                var token = _tokenGenerator.CreateToken(user); 

                // udpate user logged in date 
                user.IsActive = true;
                user.LastLoginDate = DateTime.Now;
                _unitOfWork.User.Update(user);
                _unitOfWork.Commit();


                //// Set the token in a cookie
                //Response.Cookies.Append("AuthToken", token, new CookieOptions
                //{
                //    HttpOnly = true,
                //    SameSite = SameSiteMode.Strict,
                //    Expires = DateTime.UtcNow.AddHours(1) // Set the expiration time for the cookie
                //});

                //// Add the token to the response headers
                //Response.Headers.Add("Authorization", "Bearer " + token);

                return Ok( new { Token= token });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Logout()
        {
            try
            {
                string UserIDClaim = User.FindFirst("Id").Value;
                var user = _unitOfWork.User.FindByCondition(x => x.ID.ToString() == UserIDClaim);
                user.IsActive = false;
                _unitOfWork.User.Update(user);
                _unitOfWork.Commit();

                //// Remove the authentication token cookie
                //Response.Cookies.Delete("AuthToken");
                //// Remove the Authorization header
                //Response.Headers.Remove("Authorization");

                return Ok("Logged out successfully");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost("VerfiyEmail")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult VerfiyEmail(VerfiyDTO verfiyDTO)
        {
            try
            {
                string UserIDClaim = User.FindFirst("Id").Value;
                var user = _unitOfWork.User.FindByCondition(x => x.ID.ToString() == UserIDClaim);
                var userVerifiy = _unitOfWork.UserVerification.VerfiyUserVerficationCode(UserIDClaim, verfiyDTO.VerificationCode);
                if (userVerifiy is null)
                {
                    return BadRequest(new { status = "error", message = "Your verification code is not matching." });

                }
                //Update  user
                user.EmailConfirmed = true;
                _unitOfWork.User.Update(user);
                _unitOfWork.Commit();
                //Update  userVerification
                userVerifiy.IsVerify = true;
                userVerifiy.IsDeleted = true;
                _unitOfWork.UserVerification.Update(userVerifiy);
                _unitOfWork.Commit();
                return Ok(new { status = "success", message = "Email Confirmed successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SendVerificationEmailCode")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult SendVerificationEmailCode()
        {
            try
            {
                var UserIDClaim = Guid.Parse(User.FindFirst("Id").Value);
                string UserEmailClaim = User.FindFirst("email").Value;

                // Save in User Verification Table
                var VCode = Math.Abs(Guid.NewGuid().GetHashCode()).ToString().Substring(0, 5);

                _unitOfWork.UserVerification.Insert(new UserVerification
                {
                    UserID = UserIDClaim,
                    CreationDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(1),
                    IsDeleted = false,
                    IsVerify = false,
                    VerificationCode = VCode
                });
                _unitOfWork.Commit();
                string body = $"Thank you for registering on our website. Here is your verification code: {VCode}. Please enter it to confirm your email.";
                _emailSender.SendEmailAsync(UserEmailClaim, "Verification Code", body);
                return Ok(new { status = "success", message = "The verification has been sent successfully" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetUserInfo")]
        public async Task<ActionResult> GetUserInfo([FromQuery]string userId)
        {
            try
            {
                var userIds = _unitOfWork.User.GetCurrentLoggedInUserID();
                var userInfo =await _unitOfWork.User.GetUserInfo(userIds.ToString());
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("EditUserProfile")]
        public ActionResult EditUserProfile(LoginDTO DTO)
        {
            try
            {
                var user = _unitOfWork.User.FindByCondition(x => x.ID == DTO.UserID && x.IsDeleted == false && x.EmailConfirmed == true);
                if (user == null)
                {
                    return NotFound();
                }
                user.Email = DTO.Email;
                user.PhoneNumber = DTO.PhoneNumber;
                _unitOfWork.User.Update(user);
                _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("UpdateUserImage")]
        public ActionResult UpdateUserImage([FromForm]LoginDTO DTO)
        {
            try
            {
                var file = HttpContext.Request.Form.Files;
                var user = _unitOfWork.User.FindByCondition(x => x.ID == DTO.UserID && x.IsDeleted == false && x.EmailConfirmed == true);
                if (user == null )
                {
                    return NotFound();
                }
                if (file.Count() == 0)
                {
                    return BadRequest(new { message = "You haven't select any file.." });
                }
                var image = _cloudinaryServices.SaveImage(file);
                user.Avatar = image;
                _unitOfWork.User.Update(user);
                _unitOfWork.Commit();
                return Ok(new { message="You have updated your image successfully."});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
