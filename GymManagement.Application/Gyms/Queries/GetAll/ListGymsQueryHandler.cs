﻿using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Gyms;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetAll
{
    internal sealed class ListGymsQueryHandler : IRequestHandler<ListGymsQuery, ErrorOr<List<Gym>>>
    {
        private readonly IGymsRepository _gymsRepository;
        private readonly ISubscriptionRepository _subscriptionsRepository;

        public ListGymsQueryHandler(IGymsRepository gymsRepository, ISubscriptionRepository subscriptionsRepository)
        {
            _gymsRepository = gymsRepository;
            _subscriptionsRepository = subscriptionsRepository;
        }

        public async Task<ErrorOr<List<Gym>>> Handle(ListGymsQuery query, CancellationToken cancellationToken)
        {
            if (!await _subscriptionsRepository.ExistsAsync(query.SubscriptionId))
            {
                return Error.NotFound(description: "Subscription not found");
            }

            return await _gymsRepository.ListBySubscriptionIdAsync(query.SubscriptionId);
        }
    }
}