using ErrorOr;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.Create
{
    public record CreateSubscriptionCommand(
        SubscriptionType SubscriptionType,
        Guid AdminId) : IRequest<ErrorOr<Subscription>>;
}
