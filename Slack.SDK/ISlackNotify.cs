using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Slack.SDK
{
    public interface ISlackNotify
    {
        Task<bool> SendErrorException(Exception ex, string title);
        Task<bool> SendRequestErrorException(Exception ex, string formatRequest);
        Task<bool> SendErrorException(Exception ex);
    }
}
