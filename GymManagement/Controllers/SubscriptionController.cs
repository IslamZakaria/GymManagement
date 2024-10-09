﻿using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Queries.Get;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISender _mediator;
        public SubscriptionController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
        {
            if (!DomainSubscriptionType.TryFromName(request.SubscriptionType
                                                           .ToString(),
                                                    out var subscriptionType))
            {
                return Problem(statusCode: StatusCodes.Status400BadRequest,
                               detail: "Invalid Subscription Type.");
            }
            var command = new CreateSubscriptionCommand(
                subscriptionType,
                request.AdminId);

            var createSubscriptionResult = await _mediator.Send(command);

            return createSubscriptionResult.MatchFirst(
                guid => Ok(new SubscriptionResponse(guid.Id, request.SubscriptionType)),
                error => Problem());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetSubscription(Guid id)
        {
            var query = new GetSubscriptionQuery(id);

            var getSubscriptionResult = await _mediator.Send(query);

            return getSubscriptionResult.MatchFirst(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    Enum.Parse<Contracts.Subscriptions.SubscriptionType>(subscription.SubscriptionType.Name))),
                error => Problem());
        }
    }
}
