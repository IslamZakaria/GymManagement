using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common.Persistence;

namespace GymManagement.Infrastructure.Subscriptions.Persistence
{
    internal sealed class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly GymManagementDbContext _dbContext;
        public SubscriptionRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddSubscriptionAsync(Subscription subscription)
        {
            await _dbContext.subscriptions.AddAsync(subscription);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Subscription?> GetByIdAsync(Guid id)
        {
            return await _dbContext.subscriptions.FindAsync(id);
        }
    }
}
