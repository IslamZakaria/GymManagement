﻿using GymManagement.Domain.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common.Persistence
{
    public class GymManagementDbContext : DbContext
    {
        public DbSet<Subscription> subscriptions { get; set; } = null!;

        public GymManagementDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}