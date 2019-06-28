using Microsoft.Bot.Connector.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarlCoreBot2
{
    public class TenantedCredentialProvider : ICredentialProvider
    {
        public Task<string> GetAppPasswordAsync(string appId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAuthenticationDisabledAsync()
        {
            return Task.FromResult(false);
        }

        public Task<bool> IsValidAppIdAsync(string appId)
        {
            throw new NotImplementedException();
        }
    }
}
