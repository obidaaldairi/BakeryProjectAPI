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


        [HttpGet("GetCategories")]
        public ActionResult GetCategories()
        {
            try
            {
                var categories = _unitOfWork.Category.FindAllByCondition(x => x.IsDeleted == false);
                if (!categories.Any())
                {
                    return NoContent();
                }
                return Ok(categories);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetCategory")]
        public ActionResult GetCategory(Guid ID)
        {
            try
            {
                if (ID == Guid.Empty)
                {
                    return BadRequest();
                }
                var categories = _unitOfWork.Category.FindByCondition(x => x.IsDeleted == false && x.ID == ID);
                if (categories is null)
                {
                    return NoContent(); 
                }
                return Ok(categories);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FilterCategories")]
        public ActionResult FilterCategories(string  filter = "")
        {
            try
            {
                if (string.IsNullOrEmpty(filter))
                {
                    var categories = _unitOfWork.Category.FindAllByCondition(x => x.IsDeleted == false);
                    if (!categories.Any())
                    {
                        return NoContent();
                    }
                    return Ok(categories);
                }
                else
                {
                    var categories = _unitOfWork.Category.Search(filter);
                    if (!categories.Any())
                    {
                        return NoContent();
                    }
                    return Ok(categories);
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
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
                if (Dto.CategoryID != Guid.Empty)
                {
                    var categories = _unitOfWork.Category.FindByCondition(x => x.ID == Dto.CategoryID && x.IsDeleted == false);
                    if (categories is not null)
                    {
                        if ((!string.IsNullOrEmpty(Dto.EnglishTitle) && !string.Equals(categories.EnglishTitle, Dto.ArabicTitle)) || (!string.IsNullOrEmpty(Dto.ArabicTitle) && !string.Equals(categories.EnglishTitle, Dto.ArabicTitle)))
                        {
                            if (!string.Equals(Dto.EnglishTitle, categories.EnglishTitle) && !string.Equals(Dto.ArabicTitle, categories.ArabicTitle))
                            {
                                categories.ArabicTitle = Dto.ArabicTitle;
                                categories.EnglishTitle = Dto.EnglishTitle;
                                _unitOfWork.Category.Update(categories);
                                _unitOfWork.Commit();
                                return Ok(new { message = "Updated Successfully" });
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(Dto.EnglishTitle) && !string.Equals(Dto.EnglishTitle, categories.EnglishTitle))
                                {
                                    categories.EnglishTitle = Dto.EnglishTitle;
                                    _unitOfWork.Category.Update(categories);
                                    _unitOfWork.Commit();
                                    return Ok(new { message = "Updated Successfully" });
                                }
                                if (!string.IsNullOrEmpty(Dto.ArabicTitle) && !string.Equals(Dto.ArabicTitle, categories.ArabicTitle))
                                {
                                    categories.ArabicTitle = Dto.ArabicTitle;
                                    _unitOfWork.Category.Update(categories);
                                    _unitOfWork.Commit();
                                    return Ok(new { message = "Updated Successfully" });
                                }
                            }
                        }
                        else
                        {
                            return BadRequest("Please Select Item this same data");
                        }
                    }
                        return NoContent();
                }
                else
                {
                    return NotFound();
                }
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
                if (Dto.CategoryID != Guid.Empty)
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
