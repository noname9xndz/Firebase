using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Slack.SDK.Helpers;
using Slack.SDK.Models;
using Slack.Webhooks;

namespace Slack.SDK
{
    public class SlackNotify : ISlackNotify
    {
        private SlackOptions _settings;
        private ISlackClient _slackClient;
        public SlackNotify(IOptions<SlackOptions> settings, ISlackClient slackClient)
        {
            _slackClient = slackClient;
            _settings = settings.Value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="content"></param>
        private async Task<bool> Send(string msg, string content)
        {
            //SlackClient slackClient = new SlackClient(_settings.urlChannelSentry);
            SlackMessage slackMessage = new SlackMessage();
            var slackAttachment = new SlackAttachment
            {
                Timestamp = (int)DateTime.Now.ToTimestamp(),
                Text = content,
                Color = "#D00000",
                Pretext = msg,
            };
            slackMessage.Attachments = new List<SlackAttachment> { slackAttachment };
            return await _slackClient.PostAsync(slackMessage);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ex"></param>
        public async Task<bool> SendErrorException(Exception ex)
        {
            if (ex != null)
            {
                var nameAssembly = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var msg = $"{SlackHelpers.GetEnvironmentName().ToUpper()} | {nameAssembly} | {Environment.UserName}\n{ex.Message}";
               return await  Send(msg, ex.ToString());
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="formatRequest"></param>
        public async Task<bool> SendRequestErrorException(Exception ex, string formatRequest)
        {
            if (ex != null)
            {
                if (formatRequest == null) formatRequest = "-";
                var nameAssembly = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var msg = $"{SlackHelpers.GetEnvironmentName().ToUpper()} | {nameAssembly} | {Environment.UserName}\n{ex.Message}";
                var content = $"{formatRequest}\n----\n{ex.ToString()}";
                return await Send(msg, content);
            }
            return false;
        }

        public async Task<bool> SendErrorException(Exception ex, string title)
        {
            if (ex != null)
            {
                if (title == null) title = "-";
                var nameAssembly = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var msg = $"{SlackHelpers.GetEnvironmentName().ToUpper()} | {nameAssembly} | {Environment.UserName}\n{ex.Message} |\n{title}";
                var content = ex.ToString();
                return await Send(msg, content);
            }
            return false;
        }
    }
}