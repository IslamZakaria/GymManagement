namespace GymManagement.Domain.Subscriptions
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public Guid _adminId;
        public SubscriptionType SubscriptionType { get; private set; }

        public Subscription(SubscriptionType subscriptionType,
                            Guid adminId,
                            Guid? id)
        {
            SubscriptionType = subscriptionType;
            _adminId = adminId;
            Id = id ?? Guid.NewGuid();
        }

        private Subscription()
        {

        }
    }
}
