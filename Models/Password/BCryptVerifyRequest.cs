using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptionAPIServicesSDK.Password
{
    internal class BCryptVerifyRequest
    {
        public string Password { get; set; }
        public string HashedPassword { get; set; }
    }
}
