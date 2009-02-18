using System;
using System.Collections.Generic;
using eland.model;
using eland.unittests.Helpers;
using MbUnit.Framework;

using eland.api;
using eland.api.Interfaces;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class UserTests
    {
        private List<Guid> createdUsers;
        private IDataContext dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            dataContext = IoC.Resolve<IDataContext>();
            createdUsers = new List<Guid>();
        }

        [TestFixtureTearDown]
        public void Teardown_Tests()
        {
            using (var tran = dataContext.UserRepository.Session.BeginTransaction())
            {
                foreach (var g in createdUsers)
                {
                    dataContext.UserRepository.Delete(g);
                }

                tran.Commit();
            }
        }

        [Test]
        public void Get_Null_User_By_OpenId()
        {
            var user = ((UserRepository)dataContext.UserRepository).FindByOpenId(TestDataHelper.OPEN_ID + "abcdef");

            Assert.AreEqual(null, user);
        }

        [Test]
        public void Get_User_By_OpenId()
        {
            var user = ((UserRepository)dataContext.UserRepository).FindByOpenId(TestDataHelper.OPEN_ID);

            Assert.AreEqual(user.OpenId, TestDataHelper.OPEN_ID);
            Assert.AreEqual(user.FirstName, TestDataHelper.FIRST_NAME);
            Assert.AreEqual(user.LastName, TestDataHelper.LAST_NAME);
        }

        [Test]
        public void foo()
        {
            var gamesession = new GameSession();
            var user = new User();
            var game = new Game();
            var nation = new Nation();

            gamesession.Game = game;
            gamesession.User = user;
            gamesession.Nation = nation;
            

        }

    }
}
