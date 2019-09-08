using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BadgerRank.Heart
{
    public sealed class CfbApiClientFactory : ICfbApiClientFactory
    {
        public HttpClient CreateCFBApiClient()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.collegefootballdata.com/");
            return client;
        }
    }
}
