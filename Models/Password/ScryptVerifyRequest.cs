namespace EncryptionAPIServicesSDK.Models.Password
{
    internal class ScryptVerifyRequest
    {
        public string HashedPassword { get; set; }
        public string Password { get; set; }
    }
}