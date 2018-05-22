using System;
using System.Text;
using BusinessLogic;
using DataAccess;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class BusinessLogicTests
    {
        [Test]
        public void ShouldAddNewUser()
        {
            var uow = new UnitOfWork();
            var lastUserId = uow.Repository<User>().GetDetails(x => x.Id < 0);
            var randomUser = GenerateUser(lastUserId.Id);
            Assert.Greater(0, randomUser.Id);
        }

        private User GenerateUser(int id)
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

            return new User
            {
                Id = id,
                Active = true,
                Password = "TestPass",
                Username = str.ToString()
            };
        }
    }
}
