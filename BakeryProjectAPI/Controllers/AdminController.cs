using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryProjectAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
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
    }
}
