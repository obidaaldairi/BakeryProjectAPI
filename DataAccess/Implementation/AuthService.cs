using Domin.Entity;
using Domin.Repository;
using Domin.Responses;
using Domin.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenGenerator _token;

        public AuthService(IUnitOfWork _unitOfWork , ITokenGenerator token)
        {
            this._unitOfWork = _unitOfWork;
            this._token = token;    
        }
        public AuthResponse Login(LoginResponse request)
        {
            try
            {
                var user = _unitOfWork.User.FindByCondition(x => x.Email == request.Email);
                if (user == null)
                {
                    return new AuthResponse { Message = "Email is not Exist" , Token = "" };
                }
                    bool pass = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                if (!pass)
                {
                    return new AuthResponse { Message = "Password is InCorrect"  , Token = ""};
                }
                return new AuthResponse { Token = _token.CreateToken(user) , Message = "Logged in Seccess"};
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Register(User request)
        {
            //try
            //{
            //    var user = _unitOfWork.User
            //    .FindIsExistByCondition(request.Email);
            //    if (user)
            //    {
            //        return "This Email Is Already Exist";
            //    }
            //    else
            //    {
            //        _unitOfWork.User.Insert(new User
            //        {
            //            UserName = request.Email,
            //            Email = request.Email,
            //            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            //            Avatar = request.Avatar,
            //            EmailConfirmed = false,
            //            IsActive = false,
            //            IsDeleted = false,
            //            PhoneNumber = request.PhoneNumber,
            //            PhoneNumberConfirmed = false,
            //            ID = Guid.NewGuid(),
            //            CreatedAT = DateTime.Now,
            //            Bio = request.Bio,
            //            BirthDate = request.BirthDate,
            //            LastLoginDate = new DateTime(),
            //            Role = request.Role
            //        });
            //        _unitOfWork.Commit();
            //        return "User Added Successfully";

            //    }
            //}
            //catch (Exception ex)
            //{

            //    return ex.Message;
            //}
            return "";
        }
    }
}
