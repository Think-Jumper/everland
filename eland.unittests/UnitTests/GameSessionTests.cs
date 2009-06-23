using eland.api;
using eland.api.Interfaces;
using eland.unittests.Helpers;
using MbUnit.Framework;

namespace eland.unittests.UnitTests
{
    [TestFixture]
    public class GameSessionTests
    {
        private IDataContext _dataContext;

        [TestFixtureSetUp]
        public void Setup_Tests()
        {
            _dataContext = IoC.Resolve<IDataContext>();
        }

        [Test]
        public void Create_GameSession()
        {
            var gameSession = TestDataHelper.CreateGameSession();

            Assert.IsNotNull(gameSession);
            Assert.IsNotNull(gameSession.User.Email);
            Assert.IsNotNull(gameSession.Game);
            Assert.IsTrue(gameSession.Game.GameWorld.Hexes.Count > 0);
        }

        [Test]
        public void Create_GameSession_Persist()
        {
            var gameSession = TestDataHelper.CreateGameSession();

            using (var tran = _dataContext.WorldRepository.Session.BeginTransaction())
            {
                _dataContext.WorldRepository.Save(gameSession.Game.GameWorld);
                _dataContext.RaceRepository.Save(gameSession.Nation.Race);
                _dataContext.GameSessionRepository.Save(gameSession);
                tran.Commit();
            }

            using (var tran = _dataContext.WorldRepository.Session.BeginTransaction())
            {
                _dataContext.GameSessionRepository.Delete(gameSession);

                _dataContext.GameRepository.Delete(gameSession.Game);
                _dataContext.WorldRepository.Delete(gameSession.Game.GameWorld);

                tran.Commit();
            }
        }

    }
}
