using Microsoft.Extensions.Hosting;
using System;

namespace Slack.SDK.Helpers
{
    public static class SlackHelpers
    {
        public static long ToTimestamp(this DateTime dateTime)
        {
            // Unix timestamp is seconds past epoch
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = dateTime.ToUniversalTime() - origin;
            return Convert.ToInt64(Math.Floor(diff.TotalSeconds));
        }

        public static string GetEnvironmentName()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (environmentName == Environments.Production)
            {
                environmentName = "prod";
            }
            else if (environmentName == Environments.Staging)
            {
                environmentName = "staging";
            }
            else if (environmentName == Environments.Development)
            {
                environmentName = "development";
            }
            if (string.IsNullOrWhiteSpace(environmentName))
            {
                environmentName = "prod";
            }
            return environmentName;
        }
    }
}
