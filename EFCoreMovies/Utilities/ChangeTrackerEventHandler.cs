using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreMovies.Utilities
{
    public class ChangeTrackerEventHandler : IChangeTrackerEventHandler
    {
        private readonly ILogger<ChangeTrackerEventHandler> _logger;

        public ChangeTrackerEventHandler(ILogger<ChangeTrackerEventHandler> logger)
        {
            _logger = logger;
        }

        //executed when ef started tracking entity
        public void TrackedHandler(object sender, EntityTrackedEventArgs args)
        {
            var message = $"Entity: {args.Entry.Entity}, State: {args.Entry.State}";
            _logger.LogInformation(message);
        }

        //executed when the state of entity is changed
        public void StateChangedHandler(object sender, EntityStateChangedEventArgs args)
        {
            var message =  $"Entity: {args.Entry.Entity}, Previous State: {args.OldState} - New State: {args.NewState}";
            _logger.LogInformation(message);
        }

        //executed before save changes
        public void SavingChangesHandler(object sender, SavingChangesEventArgs args)
        {
            var entities = ((ApplicationDbContext)sender).ChangeTracker.Entries();

            foreach (var entity in entities)
            {
                var message = $"Entity: {entity.Entity} it's going to be {entity.State}";
                _logger.LogInformation(message);
            }
        }

        //executed after save changes
        public void SavedChangesHandler(object sender, SavedChangesEventArgs args)
        {
            var message = $"We processed {args.EntitiesSavedCount} entities";
        _logger.LogInformation(message);
        }

        //executed when save changes failed
        public void SaveChangesFailHandler(object sender, SaveChangesFailedEventArgs args)
        {
            _logger.LogError(args.Exception, "Error in SaveChanges");
        }
    }
}
