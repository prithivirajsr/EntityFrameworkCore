using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreMovies.Utilities
{
    public interface IChangeTrackerEventHandler
    {
        void SaveChangesFailHandler(object sender, SaveChangesFailedEventArgs args);
        void SavedChangesHandler(object sender, SavedChangesEventArgs args);
        void SavingChangesHandler(object sender, SavingChangesEventArgs args);
        void StateChangedHandler(object sender, EntityStateChangedEventArgs args);
        void TrackedHandler(object sender, EntityTrackedEventArgs args);
    }
}
