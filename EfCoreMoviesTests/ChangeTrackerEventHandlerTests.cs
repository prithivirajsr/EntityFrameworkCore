using EFCoreMovies.Utilities;
using EfCoreMoviesTests.Mocks;
using Microsoft.EntityFrameworkCore;

namespace EfCoreMoviesTests
{
    [TestClass]
    public class ChangeTrackerEventHandlerTests
    {
        [TestMethod]
        public void SavedChangesHandler_Send3AsAmountOfentries_LogsCorrectMessage()
        {
            //Prepatation
            var loggerFake = new LoggerFake<ChangeTrackerEventHandler>();
            var changeTrackerEventHandler = new ChangeTrackerEventHandler(loggerFake);

            //Testing
            var savedChangesEventArgs = new SavedChangesEventArgs(acceptAllChangesOnSuccess: false, entitiesSavedCount: 3);
            changeTrackerEventHandler.SavedChangesHandler(null, savedChangesEventArgs);

            //Verification
            Assert.AreEqual("We processed 3 entities", loggerFake.LastLog);
            Assert.AreEqual(1, loggerFake.CountLogs);
        }
    }
}
