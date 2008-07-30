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
      [Ignore]
      public void User_exists()
      {
         Assert.IsTrue(userRepository.Exists(new Guid("D3ECE5C4-7F66-4114-BB02-98B270523BA5")));
         Assert.IsFalse(userRepository.Exists(new Guid("D3ECE5C9-7F66-4114-BB02-98B270523BA5")));
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
