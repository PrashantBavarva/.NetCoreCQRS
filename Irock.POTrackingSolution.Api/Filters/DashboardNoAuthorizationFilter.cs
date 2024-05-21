using Hangfire.Dashboard;

namespace Irock.POTrackingSolution.Api.Filters
{
    public class DashboardNoAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
