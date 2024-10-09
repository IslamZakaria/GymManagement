using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Application.Subscriptions.Commands.Create;
using GymManagement.Domain.Subscriptions;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.CreateSubscription
{
    internal sealed class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IAdminsRepository _adminsRepository;

        private readonly IUnitOfWork _unitOfWork;
        public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IAdminsRepository adminsRepository, IUnitOfWork unitOfWork)
        {
            _subscriptionRepository = subscriptionRepository;
            _adminsRepository = adminsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var admin = await _adminsRepository.GetByIdAsync(request.AdminId);

            if (admin is null)
            {
                return Error.NotFound(description: "Admin not found.");
            }

            if (admin.SubscriptionId is not null)
            {
                return Error.Conflict(description: "Admin already has an active subscription.");
            }

            var subscription = new Subscription(
                subscriptionType: request.SubscriptionType,
                adminId: request.AdminId);

            admin.SetSubscription(subscription);

            await _subscriptionRepository.AddSubscriptionAsync(subscription);
            await _adminsRepository.UpdateAsync(admin);
            await _unitOfWork.CommitChabgesAsync();
            return subscription;
        }
    }
}
