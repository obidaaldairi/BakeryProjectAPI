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
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public ProductController(IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "Provider")]
        public ActionResult AddProduct(ProductDTO DTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Check = _unitOfWork.Product.FindIsExistByCondition(x => x.ArabicProductName == DTO.ArabicProductName
                    && x.EnglishProductName == DTO.EnglishProductName && x.IsDeleted == false);
                    if (Check)
                    {
                        return Ok("The Product Is Exist");
                    }

                    //add product
                    var product = _unitOfWork.Product.Insert(new Product
                    {
                        ArabicProductName = DTO.ArabicProductName,
                        EnglishProductName = DTO.EnglishProductName,
                        ArabicDescription = DTO.ArabicDescription,
                        EnglishDescription = DTO.EnglishDescription,
                        CategoryID = DTO.CategoryID,
                        Price = DTO.Price,
                        IsDeleted = false
                    });
                    _unitOfWork.Commit();


                    //get provider ID
                    var provider = _unitOfWork.Provider.FindByCondition(x => x.UserID == Guid.Parse(User.FindFirst("Id").Value));
                    if(provider == null)
                    {
                        return BadRequest("Please Login Again");
                    }
                    //add Product Provider
                    _unitOfWork.ProductProvider.Insert(new ProductProvider
                    {
                        ProductID = product.ID,
                        IsDeleted = false,
                        ProviderID = provider.ID
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
