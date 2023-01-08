namespace EncryptionAPIServicesSDK.Models.Password
{
    internal class Argon2VerifyRequest
    {
        public string HashedPassword { get; set; }
        public string Password { get; set; }
    }
}
