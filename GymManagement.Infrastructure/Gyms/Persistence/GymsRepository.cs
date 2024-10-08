﻿using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Gyms.Persistence
{
    internal sealed class GymsRepository : IGymsRepository
    {
        private readonly GymManagementDbContext _dbContext;

        public GymsRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Gym gym)
        {
            await _dbContext.Gyms.AddAsync(gym);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Gyms
                .AsNoTracking()
                .AnyAsync(gym => gym.Id == id);
        }

        public async Task<Gym?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Gyms
                .AsNoTracking()
                .FirstOrDefaultAsync(gym => gym.Id == id);
        }

        public async Task<List<Gym>> ListBySubscriptionIdAsync(Guid subscriptionId)
        {
            return await _dbContext.Gyms
                .Where(gym => gym.SubscriptionId == subscriptionId)
                .ToListAsync();
        }

        public Task RemoveAsync(Gym gym)
        {
            _dbContext.Remove(gym);

            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(List<Gym> gyms)
        {
            _dbContext.RemoveRange(gyms);

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Gym gym)
        {
            _dbContext.Update(gym);

            return Task.CompletedTask;
        }
    }
}
