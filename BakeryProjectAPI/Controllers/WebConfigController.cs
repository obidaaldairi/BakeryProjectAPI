using BakeryProjectAPI.DTOs;
using BCrypt.Net;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryProjectAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WebConfigController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public WebConfigController(IUnitOfWork unitOfWork, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost("AddWebConfigValue")]
        public ActionResult Post(WebConfigDTO webConfigDTO)
        {

            var checkExistkey = _unitOfWork.WebConfiguration.FindIsExistByCondition(q => q.ConfigKey == webConfigDTO.ConfigKey && q.IsDeleted == false);
            if (checkExistkey) { return BadRequest("The key is already exist."); }
            _unitOfWork.WebConfiguration.Insert(new WebConfiguration
            {
                IsDeleted = false,
                ConfigKey = webConfigDTO.ConfigKey,
                ConfigValue = webConfigDTO.ConfigValue,
                Description = webConfigDTO.ConfigDescription
            });
            _unitOfWork.Commit();
            return Ok(new { message = "Added Succefully" });
        }


        [HttpGet("Encrypted")]
        public ActionResult Encrypted(string encrypt = "")
        {
            if (encrypt.ToLower().Equals("true"))
            {
                string key = _unitOfWork.WebConfiguration.FindByCondition(q => q.ConfigKey == "EncryptionKey" && q.IsDeleted == false).ConfigValue;
                string connectionstring = _configuration.GetSection("Defult").Value;
                if (connectionstring is not null)
                {
                    string appSettingsPath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "appsettings.json");
                    string encryptedText = _unitOfWork.CypherServices.EncryptText(connectionstring, key);
                    string fileContent = System.IO.File.ReadAllText(appSettingsPath);
                    string modifiedContent = fileContent.Replace(connectionstring, encryptedText);
                    System.IO.File.WriteAllText(appSettingsPath, modifiedContent);
                }
            }
            return Ok();
        }



        [HttpGet("Decrypted")]
        public ActionResult Decrypted(string decrypt = "")
        {
            if (decrypt.ToLower().Equals("true"))
            {
                string key = _unitOfWork.WebConfiguration.FindByCondition(q => q.ConfigKey == "EncryptionKey" && q.IsDeleted == false).ConfigValue;
                string connectionstring = _configuration.GetSection("Defult").Value;
                if (connectionstring is not null)
                {
                    string appSettingsPath = System.IO.Path.Combine(_webHostEnvironment.ContentRootPath, "appsettings.json");
                    string encryptedText = _unitOfWork.CypherServices.DecryptText(connectionstring, key);
                    string fileContent = System.IO.File.ReadAllText(appSettingsPath);
                    string modifiedContent = fileContent.Replace(connectionstring, encryptedText);
                    System.IO.File.WriteAllText(appSettingsPath, modifiedContent);
                }
            }
            return Ok();
        }
    }
}
