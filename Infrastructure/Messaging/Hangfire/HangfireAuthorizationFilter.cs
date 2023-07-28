using Hangfire.Dashboard;

namespace VendorBoilerplate.Infrastructure.Messaging.Hangfire
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // all user forced to see dashboard (for dev only)
            return true;
        }
    }
}
