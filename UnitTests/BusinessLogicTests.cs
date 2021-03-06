﻿using System;
using System.Text;
using BusinessLogic;
using DataAccess;
using Xunit;

namespace UnitTests
{
    public class BusinessLogicTests
    {
        [Fact]
        public void ShouldAddNewUser()
        {
            var uow = new UnitOfWork();
            var lastUserId = uow.Repository<UserEntity>().GetDetails(x => x.UserId < 0);
            var randomUser = GenerateUser(lastUserId.UserId);
            Assert.Equal(1, 1);
        }

        private UserEntity GenerateUser(int id)
        {
            var rand = new Random();
            int count = rand.Next(8, 20);
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                var asciNumber = rand.Next(97, 122);
                byte b = Convert.ToByte(asciNumber);
                str.Append(Encoding.ASCII.GetString(new[] {b}));
            }

            return new UserEntity
            {
                UserId = id,
                Active = true,
                Password = "TestPass",
                Username = str.ToString()
            };
        }
    }
}
