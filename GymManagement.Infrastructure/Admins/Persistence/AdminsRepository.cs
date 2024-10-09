using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Admins.Persistence
{
    internal sealed class AdminsRepository : IAdminsRepository
    {
        private readonly GymManagementDbContext _dbContext;
        public AdminsRepository(GymManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Admin?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(admin => admin.Id == id);
        }

        public Task UpdateAsync(Admin admin)
        {
            _dbContext.Update(admin);

            return Task.CompletedTask;
        }
    }
}
