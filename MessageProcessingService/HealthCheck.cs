using Nancy;

namespace MessageProcessingService
{
    public class HealthCheck : NancyModule
    {
        public HealthCheck()
        {
            Get["/healthcheck"] =
            Get["/health/check"] =
            Get["/health/validate"] = _ => "healthy";
        }
    }
}