using System.Web;
using System.Security.Principal;
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

            //SetupResult.For(mockedhttpContext.User).Return(mockedUser);
            //SetupResult.For(mockedUser.Identity).Return(mockedIdentity);
            //SetupResult.For(mockedIdentity.IsAuthenticated).Return(isAuthenticated);
            //SetupResult.For(mockedIdentity.Name).Return(IdentityName);

            return mockedhttpContext.Object;
        }

    }
}