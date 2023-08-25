using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryProjectAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost("AddCategory")]
        public ActionResult AddCategory(CategoryDTO Dto)
        {
            try
            {
                var categories = _unitOfWork.Category.FindByCondition(x => x.ArabicTitle == Dto.ArabicTitle && x.EnglishTitle == Dto.EnglishTitle);
                if (categories is not null)
                {
                    return Ok(new { message = "Is Exist" });
                }
                if (string.IsNullOrEmpty(Dto.ArabicTitle) || string.IsNullOrEmpty(Dto.EnglishTitle))
                {
                    return BadRequest("Please Enter Title");
                }
                _unitOfWork.Category.Insert(new Category
                {
                    ArabicTitle = Dto.ArabicTitle,
                    EnglishTitle = Dto.EnglishTitle,
                    IsDeleted = false
                });
                _unitOfWork.Commit();
                return Ok(new { message = "Added Successfully" });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateCategory")]
        public ActionResult UpdateCategory(CategoryDTO Dto)
        {
            try
            {
                if ((Dto.CategoryID != Guid.Empty) && ( !string.IsNullOrEmpty(Dto.ArabicTitle) || !string.IsNullOrEmpty(Dto.EnglishTitle)))
                {
                    var categories = _unitOfWork.Category.FindByCondition(x => x.ID == Dto.CategoryID && x.IsDeleted == false);
                    if (categories is null)
                    {
                        return NoContent();
                    }
                    if (!string.Equals(Dto.ArabicTitle , categories.ArabicTitle) && !string.IsNullOrEmpty(Dto.ArabicTitle))
                    {
                        categories.ArabicTitle = Dto.ArabicTitle;
                        _unitOfWork.Category.Update(categories);
                        _unitOfWork.Commit();
                        //return Ok(new { message = "Updated Successfully" });

                    }
                    if (!string.Equals(Dto.EnglishTitle, categories.EnglishTitle) &&  !string.IsNullOrEmpty(Dto.EnglishTitle))
                    {
                        categories.EnglishTitle = Dto.EnglishTitle;
                        _unitOfWork.Category.Update(categories);
                        _unitOfWork.Commit();
                        return Ok(new { message = "Updated Successfully" });
                    }
                    return Ok(new { message = "No data Updated" });
                }
                return BadRequest("Please Select Item");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("DeleteCategory")]
        public ActionResult DeleteCategory(CategoryDTO Dto)
        {
            try
            {
                if (string.IsNullOrEmpty(Dto.CategoryID.ToString()))
                {
                    var categories = _unitOfWork.Category.FindByCondition(x => x.ID == Dto.CategoryID && x.IsDeleted == false);
                    if (categories is null)
                    {
                        return NoContent();
                    }

                    categories.IsDeleted = true;
                    _unitOfWork.Category.Update(categories);
                    _unitOfWork.Commit();
                    return Ok(new { message = "Deleted Successfully" });
                }
                return BadRequest();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
