using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Queries;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
            var command = new CreateSubscriptionCommand(
                request.SubscriptionType.ToString(),
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
                    Enum.Parse<SubscriptionType>(subscription.SubscriptionType))),
                error => Problem());
        }
    }
}
