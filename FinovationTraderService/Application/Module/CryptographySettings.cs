namespace FinovationTrader.Application.Module
{
    public class CryptographySettings
    {
        public const string Key = "CryptographySettings";

        public string EncryptionAESKeyBytes { get; set; }

        public string EncryptionAESIVBytes { get; set; }

        public string Pbkdf2Salt { get; set; }
    }
}
