using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using eland.Controllers;
using System.Web.Mvc;
using eland.api;
using eland.model;

namespace eland.unittests.UnitTests
{
   [TestFixture]
   public class UserTests
   {
      private const string OPEN_ID = "http://jamie.shortbet.org";
      private const string FIRST_NAME = "Jamie";
      private const string LAST_NAME = "Fraser";
      private const string EMAIL = "jamie.fraser@gmail.com";

      private List<Guid> createdUsers;
      private UserRepository userRepository;

      [TestFixtureSetUp]
      public void Setup_Tests()
      {
         userRepository = new UserRepository();
         createdUsers = new List<Guid>();
      }

      [TestFixtureTearDown]
      public void Teardown_Tests()
      {
         foreach (Guid g in createdUsers)
         {
            userRepository.Delete(userRepository.Get(g));
         }
      }

      [Test]
      public void Get_Null_User_By_OpenId()
      {
         User user = userRepository.FindByOpenId(OPEN_ID + "abcdef");

         Assert.AreEqual(null, user);
      }

      [Test]
      public void Get_User_By_OpenId()
      {
         User user = new User();

         user.OpenId = OPEN_ID;
         user.FirstName = FIRST_NAME;
         user.LastName = LAST_NAME;
         user.Email = EMAIL;

         userRepository.Save(user);

         Assert.AreNotEqual(Guid.Empty, user.Id);

         createdUsers.Add(user.Id);

         user = userRepository.FindByOpenId(OPEN_ID);

         Assert.AreEqual(user.OpenId, OPEN_ID);
         Assert.AreEqual(user.FirstName, FIRST_NAME);
         Assert.AreEqual(user.LastName, LAST_NAME);
      }

   }
}
