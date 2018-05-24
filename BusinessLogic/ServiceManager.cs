using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using BusinessLogic.Models;
using BusinessLogic.Requests;
using BusinessLogic.Responses;
using DataAccess;

namespace BusinessLogic
{
    public class ServiceManager : IServiceManager
    {
        private IUnitOfWork uow;

        public ServiceManager()
        {
            this.uow = new UnitOfWork();
        }
        public CreateUserResponse CreatUser(CreateUserRequest request)
        {
            string encryptedPassword = HashPassword(request.User.Password);
            var response = new CreateUserResponse();

            var dbUser = new UserEntity
            {
                FirstName = request.User.FirstName,
                LastName = request.User.LastName,
                Email = request.User.Email,
                Password = encryptedPassword,
                Username = request.User.UserName
            };

            try
            {
                uow.Repository<UserEntity>().Add(dbUser);
                uow.Save();
                response.Success = true;
                response.UserId = dbUser.UserId;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            var response = new GetUserResponse();

            try
            {
                var dbUser = uow.Repository<UserEntity>().GetDetails(x => x.Username == request.User.UserName);
                if (dbUser == null)
                {
                    throw new Exception("User doesn't exist in database");
                }

                string encryptedPassword = HashPassword(request.User.Password);
                if (encryptedPassword != dbUser.Password)
                {
                    //TODO: later try login count additional column in db and increase each time when password is wrong.
                    throw new Exception("Password is incorrect");
                }

                response.User = new User
                {
                    UserName = dbUser.Username,
                    Email = dbUser.Email,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName
                };

                response.Success = true;
                response.UserId = dbUser.UserId;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.Success = false;
            }

            return response;
        }

        private string HashPassword(string password)
        {
            byte[] hash;
            var data = Encoding.UTF8.GetBytes(password);

            using (SHA512 shaM = new SHA512Managed())
            {
               hash = shaM.ComputeHash(data);
            }

            StringBuilder stringBuilder = new StringBuilder(128);

            foreach (var b in hash)
            {
                stringBuilder.Append(b.ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
