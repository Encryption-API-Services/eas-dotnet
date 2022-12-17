using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionAPIServicesSDK
{
    public static class EASConfiguration
    {
        private static string _ApiToken;
        public static string ApiToken
        {
            get { return _ApiToken; }
            set { _ApiToken = value; }
        }
    }
}
