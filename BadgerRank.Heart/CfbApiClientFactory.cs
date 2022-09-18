using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BadgerRank.Heart
{
    public sealed class CfbApiClientFactory : ICfbApiClientFactory
    {
        private HttpClient client;
        private object clientLock = new object();

        public HttpClient CreateCFBApiClient()
        {
            if (client is null)
            {
                lock (clientLock)
                {
                    if (client is null)
                    {
                        client = new HttpClient();
                        client.BaseAddress = new Uri("https://api.collegefootballdata.com/");
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", "hzxhw76Wn5WXT/BfGFU1TmMDA4Fwo6D5i9DEHdDlWMNAI1mitkQT7w6BHoadOgOW");

                    }
                }
            }

            return client;
        }
    }
}
