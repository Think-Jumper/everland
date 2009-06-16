using eland.unittests.Helpers;
using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class GameSessionTests
    {
        //private IDataContext _dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            //_dataContext = IoC.Resolve<IDataContext>();
        }

        [Test]
        public void Get_GameSession()
        {
            var gameSession = TestDataHelper.CreateGameSession();

            Assert.IsNotNull(gameSession);
            Assert.IsNotNull(gameSession.User.Email);
            Assert.IsNotNull(gameSession.Game);
            Assert.IsTrue(gameSession.Game.GameWorld.Hexes.Count > 0);
        }

        [Test]
        public void Get_GameSession_Persist()
        {
            
        }

    }
}
