using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BadgerRank.Heart
{
    public interface ICfbApiClientFactory
    {
        HttpClient CreateCFBApiClient();
    }
}
