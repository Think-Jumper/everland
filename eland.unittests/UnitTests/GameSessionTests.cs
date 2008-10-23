using System;
using eland.api;
using MbUnit.Framework;
using eland.api.Interfaces;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class GameSessionTests
    {
        private IDataContext dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            dataContext = IoC.Resolve<IDataContext>();
        }

        [Test]
        public void Get_GameSession()
        {
            var gameSession = dataContext.GameSessionRepository.Get(new Guid("ECDEB934-2251-43D0-9D79-6D5EC7176EA9"));

            Assert.IsNotNull(gameSession);
            Assert.IsNotNull(gameSession.User.Email);
            Assert.IsNotNull(gameSession.Game);

        }

    }
}
