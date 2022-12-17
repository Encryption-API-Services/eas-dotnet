using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionAPIServicesSDK
{
    public static class EASConfiguration
    {
        /// <summary>
        /// The consumer of the library should be able to set in their API Token as such
        /// EASConfiguration.ApiKey = "MyApiKey";
        /// </summary>
        private static string _ApiKey;
        public static string ApiKey
        {
            get { return _ApiKey; }
            set { _ApiKey = value; }
        }

        /// <summary>
        /// The Base URL should automatically point to the production server api route. However,
        /// the user should be able to override it for local testing purposes.
        /// </summary>
        private static string _BaseUrl = "https://encryptionapiservices.com/api/";
        public static string BaseUrl
        {
            get { return _BaseUrl; }
            set { _BaseUrl = value; }
        }
    }
}
