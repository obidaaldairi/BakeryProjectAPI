using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BakeryProjectAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _unitOfWork.DbInitializer.Initialize();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }



        [HttpGet("Seeding")]
        public ActionResult Seeding()
        {
            if(!_unitOfWork.User.FindIsExistByCondition(q => q.Email== "Test@email.com"))
            {
                // Add Role
                var role = _unitOfWork.Role.Insert(new Domin.Entity.Role
                {
                    ArabicRoleName = "Provider",
                    EnglishRoleName = "Provider",
                    IsDeleted = false
                });
                _unitOfWork.Commit();


                // Add Category
                _unitOfWork.Category.Insert(new Domin.Entity.Category
                {
                    ArabicTitle = "Electric",
                    EnglishTitle = "Electric",
                    IsDeleted = false
                });
                _unitOfWork.Commit();


                // Add User 
                var user = _unitOfWork.User.Insert(new Domin.Entity.User
                {
                    EnglishUserName = "Test",
                    ArabicBio = "",
                    ArabicUserName = "Test",
                    BirthDate = new DateTime(),
                    CreatedAt = DateTime.Now,
                    Email = "Test@email.com",
                    EnglishBio = "",
                    LastLoginDate = new DateTime(),
                    Password = BCrypt.Net.BCrypt.HashPassword("Test1234*"),
                    PhoneNumber = "0796431984",
                    PhoneNumberConfirmed = false,
                    EmailConfirmed = false,
                    IsActive = false,
                    IsDeleted = false,
                    Avatar = $"https://ui-avatars.com/api/?name=Test&length=1"
                });
                _unitOfWork.Commit();

                // Add User Roles
                _unitOfWork.UserRole.Insert(new Domin.Entity.UserRole
                {
                    RoleId = role.ID,
                    UserId = user.ID,
                    IsDeleted = false,
                });
                _unitOfWork.Commit();

                // Find the 
                var Userrole = _unitOfWork.Role.FindByCondition(q => q.ID == role.ID).EnglishRoleName;

                switch (Userrole)
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
                }
            }



            return Ok();
        }
    }
}