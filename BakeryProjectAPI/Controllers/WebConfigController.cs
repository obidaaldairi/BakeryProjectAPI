using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebConfigController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WebConfigController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost("AddWebConfigValue")]
        public ActionResult Post(WebConfigDTO webConfigDTO)
        {
            
            var checkExistkey = _unitOfWork.WebConfiguration.FindIsExistByCondition(q => q.ConfigKey == webConfigDTO.ConfigKey && q.IsDeleted==false);
            if (checkExistkey) { return BadRequest("The key is already exist."); }
            _unitOfWork.WebConfiguration.Insert(new WebConfiguration
            {
             IsDeleted=false,
             ConfigKey = webConfigDTO.ConfigKey,
             ConfigValue = webConfigDTO.ConfigValue,
             Description = webConfigDTO.ConfigDescription
            });
            _unitOfWork.Commit();
            return Ok(new { message="Added Succefully"});
        }


    }
}
