using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryProjectAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(Roles ="Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailSender _emailSender;


        public AdminController(IUnitOfWork unitOfWork, ITokenGenerator tokenGenerator, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _tokenGenerator = tokenGenerator;
            _emailSender = emailSender;
        }

        [HttpPost("AddRole")]
        public ActionResult AddRole(RoleDTO DTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Check = _unitOfWork.Role.FindIsExistByCondition(x => x.ArabicRoleName == DTO.ArabicRoleName && x.EnglishRoleName == DTO.EnglishRoleName && x.IsDeleted == false);
                    if (Check)
                    {
                        return Ok("The Role Is Exist");
                    }
                    _unitOfWork.Role.Insert(new Role
                    {
                        ArabicRoleName = DTO.ArabicRoleName,
                        EnglishRoleName = DTO.EnglishRoleName,
                        IsDeleted = false
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
        [HttpGet("GetAllRole")]
        public ActionResult GetAllRole()
        {
            try
            {
                var Roles = _unitOfWork.Role.FindAllByCondition(x => x.IsDeleted == false);
                if (!Roles.Any())
                {
                    return NoContent();
                }
                return Ok(Roles);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("AddAdminUser")]
        public ActionResult AddAdminUser(RegisterDTO DTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Save in User Table
                    var user = new User
                    {
                        ArabicUserName = DTO.ArabicUserName,
                        EnglishUserName = DTO.EnglishUserName,
                        Email = DTO.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(DTO.Password),
                        ArabicBio = "",
                        EnglishBio = "",
                        BirthDate = DTO.BirthDate,
                        CreatedAt = DateTime.Now,
                        EmailConfirmed = false,
                        IsActive = false,
                        IsDeleted = false,
                        LastLoginDate = new DateTime(),
                        PhoneNumber = DTO.PhoneNumber,
                        PhoneNumberConfirmed = false,
                        Avatar = $"https://ui-avatars.com/api/?name={DTO.EnglishUserName}&length=1",
                    };
                    //Check User
                    var check = _unitOfWork.User.FindIsExistByCondition(x => x.Email == DTO.Email);
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
                        RoleId = DTO.RoleID,
                    });
                    _unitOfWork.Commit();

                    var role = _unitOfWork.Role.FindByCondition(q => q.ID == DTO.RoleID).EnglishRoleName;

                    switch (role)
                    {
                        case "Provider":
                            // Provider
                            _unitOfWork.Provider.Insert(new Provider
                            {
                                UserID = user.ID,
                                IsDeleted = false,
                            });
                            _unitOfWork.Commit();
                            break;

                        case "Admin":
                            // Admin
                            _unitOfWork.Admin.Insert(new Admin
                            {
                                UserID = user.ID,
                                IsDeleted = false,
                            });
                            _unitOfWork.Commit();
                            break;
                        default:
                            break;
                    }


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

        [HttpGet("DeleteAdminUser")]
        public ActionResult DeleteAdminUser(Guid UserID)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Check User
                    var user = _unitOfWork.User.FindByCondition(x => x.ID == UserID && x.IsDeleted == false);
                    if (user == null)
                    {
                        return BadRequest("Something Went Wrong");
                    }
                    // delete Main User
                    user.IsDeleted = true;
                    _unitOfWork.User.Update(user);
                    _unitOfWork.Commit();
                    // delete  User Role
                    var userRole = _unitOfWork.UserRole.FindByCondition(x => x.UserId == user.ID);
                    userRole.IsDeleted = true;
                    _unitOfWork.UserRole.Update(userRole);
                    _unitOfWork.Commit();

                    var role = _unitOfWork.UserRole.FindByConditionWithIncludes(q => q.UserId == UserID, x=>x.Role).Role.EnglishRoleName;
                    if (string.IsNullOrEmpty(role)) 
                    {
                        return BadRequest();
                    }
                    switch (role)
                    {
                        case "Provider":
                            // delete Provider
                            var provider = _unitOfWork.Provider.FindByCondition(x => x.UserID == UserID);
                            provider.IsDeleted = true;
                            _unitOfWork.Provider.Update(provider);
                            _unitOfWork.Commit();
                            var productProvider = _unitOfWork.ProductProvider.FindAllByCondition(x => x.ProviderID == provider.ID);
                            productProvider.ForEach((item) =>
                            {
                                //delete ProductProvider
                                item.IsDeleted = true;
                                _unitOfWork.ProductProvider.Update(item);
                                _unitOfWork.Commit();

                                //delete Product
                                var product = _unitOfWork.Product.FindByCondition(x => x.ID == item.ProductID);
                                product.IsDeleted = true;
                                _unitOfWork.Product.Update(product);
                                _unitOfWork.Commit();

                                var productImage = _unitOfWork.ProductImage.FindByCondition(x => x.ProductID == product.ID);
                                productImage.IsDeleted = true;
                                _unitOfWork.ProductImage.Update(productImage);
                                _unitOfWork.Commit();
                            });
                            break;

                        case "Admin":
                            // delete Admin
                            var admin = _unitOfWork.Admin.FindByCondition(x => x.UserID == UserID);
                            admin.IsDeleted = true;
                            _unitOfWork.Admin.Update(admin);
                            _unitOfWork.Commit();
                            break;
                        default:
                            break;
                    }


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
    }
}
