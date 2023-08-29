﻿using BakeryProjectAPI.DTOs;
using Domin.Entity;
using Domin.Repository;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IWebHostEnvironment _hosting;
        public ProductController(IUnitOfWork unitOfWork, IEmailSender emailSender, IWebHostEnvironment hosting)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _hosting = hosting;
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
                    var providerID = _unitOfWork.Provider.GetCurrentLoggedInUserID();
                    if(providerID == Guid.Empty)
                    {
                        return BadRequest("Please Login Again");
                    }
                    //add Product Provider
                    _unitOfWork.ProductProvider.Insert(new ProductProvider
                    {
                        ProductID = product.ID,
                        IsDeleted = false,
                        ProviderID = providerID
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


        [HttpPost("AddProductImage")]
        [Authorize(Roles = "Provider")]
        public ActionResult AddProductImage(ProductImageDTO model)
        {
            try
            {
                // for test
                var headerTokenAuth = Request.Headers["Authorization"];
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(model.Image))
                    {
                        return BadRequest("Please Choose File.");
                    }
                   var productImages= _unitOfWork.ProductImage.Insert(new ProductImages
                    {
                        ProductID= model.ProductID,
                        IsDeleted= false,
                    });
                    UploadImage(productImages);
                    _unitOfWork.Commit();
                    return Ok("You have uploaded file successfully.");
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



        private void UploadImage(ProductImages model)
        {
            // API Files Uplaod Function 
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                string directoryPath = Path.Combine(_hosting.ContentRootPath, "Uploads", "Images");
                string filePath = Path.Combine(directoryPath, ImageName);
                var filestream = new FileStream(filePath, FileMode.Create);
                file[0].CopyTo(filestream);
                model.Image = ImageName;
            }
            else if (model.Image == null)
            {
                model.Image = "default.png";
            }
            else
            {
                model.Image = model.Image;
            }
        }

    }

}
