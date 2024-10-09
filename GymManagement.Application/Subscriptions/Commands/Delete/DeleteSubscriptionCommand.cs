using ErrorOr;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.Delete
{
    public record DeleteSubscriptionCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;
}
