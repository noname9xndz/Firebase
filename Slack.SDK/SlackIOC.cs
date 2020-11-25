using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slack.SDK.Models;
using Slack.Webhooks;

namespace Slack.SDK
{
    public static class SlackIoc
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SlackOptions>(options =>
                configuration.GetSection("SlackConfig").Bind(options));

            services.AddScoped<ISlackClient, SlackClient>();
            //todo add urlChannelSentry
            services.AddScoped<ISlackNotify, SlackNotify>();
        }
    }
}
