using System.Net.Http;

namespace EncryptionAPIServicesSDK
{
    internal sealed class HttpClientWrapper : HttpClient
    {
        private static HttpClientWrapper instance = null;
        private static readonly object padlock = new object();

        HttpClientWrapper()
        {
        }

        public static HttpClientWrapper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new HttpClientWrapper();
                    }
                    return instance;
                }
            }
        }
    }
}