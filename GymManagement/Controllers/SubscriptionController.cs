using GymManagement.Contracts.Subscriptions;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateSubscription(CreateSubscriptionRequest request)
        {
            return Ok(request);
        }
    }
}
