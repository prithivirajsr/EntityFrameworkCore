using EFCoreMovies.Utilities;

namespace EfCoreMoviesTests
{
    [TestClass]
    public class UserServiceFakeTests
    {
        [TestMethod]
        public void GetUserId_DoesNotReturnNull()
        {
            //Preparation
            var userService = new UserServiceFake();

            //Testing
            var result = userService.GetUserId();

            //Verification
            Assert.IsNotNull(result); //pass when the result is not null
            Assert.AreNotEqual("", result); //pass when the result is not ""
        }
    }
}