using System;
using System.Collections.Generic;
using System.Text;
using Firebase.SDK.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Slack.SDK;
using Slack.SDK.Models;
using Slack.Webhooks;

namespace Firebase.SDK
{
    public static class FirebaseIoc
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<FirebaseConfig>(options => 
                configuration.GetSection("FirebaseConfig").Bind(options));
            services.Configure<SlackOptions>(options =>
                configuration.GetSection("SlackConfig").Bind(options));

            services.AddScoped<ISlackClient, SlackClient>();
            //todo add urlChannelSentry
            services.AddScoped<ISlackNotify, SlackNotify>();
            services.AddScoped<IFirebase, Firebase>();
        }
    }
}
