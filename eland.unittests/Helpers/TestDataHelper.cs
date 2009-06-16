using System;
using System.Web;
using System.Security.Principal;
using eland.model;
using eland.model.Enums;
using Moq;

namespace eland.unittests.Helpers
{
    public class TestDataHelper
    {
        public const string OPEN_ID = "http://jamief00.myopenid.com/";
        public const string FIRST_NAME = "Jamie";
        public const string LAST_NAME = "Fraser";
        public const string EMAIL = "jamie.fraser@gmail.com";

        public static HttpContextBase SetupHttpContextMocks(bool isAuthenticated, string IdentityName)
        {
            var mockedhttpContext = new Mock<HttpContextBase>();
            var mockedUser = new Mock<IPrincipal>();
            var mockedIdentity = new Mock<IIdentity>();

            mockedhttpContext.Setup(x => x.User).Returns(mockedUser.Object);
            mockedUser.Setup(x => x.Identity).Returns(mockedIdentity.Object);
            mockedIdentity.Setup(x => x.IsAuthenticated).Returns(isAuthenticated);
            mockedIdentity.Setup(x => x.Name).Returns(IdentityName);

            return mockedhttpContext.Object;
        }

        public static GameSession CreateGameSession()
        {
            var race = new Race { Name = "Default Race" };
            var nation = new Nation { Name = "Default Nation", Race = race };
            var user = new User { Email = "jamie.fraser@gmail.com", FirstName = "Jamie", LastName = "Fraser", OpenId = "http://jamief00.myopenid.com/" };
            var world = new World { Height = 100, Width = 100, Name = "Default World" };
            var game = new Game { Name = "Default Game", Started = DateTime.Now, GameWorld = world};
            var gameSession = new GameSession { EnteredGame = DateTime.Now, Nation = nation, Game = game, User = user };

            for (var y = 1; y <= world.Width; y++)
            {
                for (var x = 1; x <= world.Height; x++)
                {
                    world.AddHex(new Hex { World = world, HexType = HexType.Grass, X = x, Y = y });
                }
            }

            return gameSession;

        }

    }
}