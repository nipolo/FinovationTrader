namespace FinovationTrader.Application.Services
{
    public interface ICryptoService
    {
        string DecryptWithAes(string textToDecrypt);
        string EncryptWithAes(string textToEncrypt);
        string MD5Hashing(string textToHash);
        string Pbkdf2Hashing(string textToHash);
    }
}