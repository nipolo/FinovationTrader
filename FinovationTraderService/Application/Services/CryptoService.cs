using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

using FinovationTrader.Application.Module;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace FinovationTrader.Application.Services
{
    public class CryptoService : IDisposable, ICryptoService
    {
        private Aes _aes;
        private string _pbkdf2Salt;

        public CryptoService(IOptions<CryptographySettings> cryptographySettings)
        {
            _aes = Aes.Create();
            _aes.Key = Convert.FromBase64String(cryptographySettings
                        .Value
                        .EncryptionAESKeyBytes);
            _aes.IV = Convert.FromBase64String(cryptographySettings
                        .Value
                        .EncryptionAESIVBytes);

            _pbkdf2Salt = cryptographySettings.Value.Pbkdf2Salt;
        }

        public void Dispose()
        {
            _aes.Dispose();
        }

        public string EncryptWithAes(string textToEncrypt)
        {
            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);

            byte[] encryptedText;

            using (var encryptMemoryStream = new MemoryStream())
            {
                using var encryptCryptoStream = new CryptoStream(encryptMemoryStream, encryptor, CryptoStreamMode.Write);

                using (var encryptStreamWriter = new StreamWriter(encryptCryptoStream))
                {
                    encryptStreamWriter.Write(textToEncrypt);
                }

                encryptedText = encryptMemoryStream.ToArray();
            }

            return Convert.ToBase64String(encryptedText);
        }

        public string DecryptWithAes(string textToDecrypt)
        {
            ICryptoTransform decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);

            string decryptedText;

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(textToDecrypt)))
            {
                using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using var srDecrypt = new StreamReader(csDecrypt);

                decryptedText = srDecrypt.ReadToEnd();
            }

            return decryptedText;
        }

        public string MD5Hashing(string textToHash)
        {
            var textToHashBytes = Encoding.ASCII.GetBytes(textToHash);

            var hashedValue = new MD5CryptoServiceProvider().ComputeHash(textToHashBytes);

            return Convert.ToBase64String(hashedValue);
        }

        public string Pbkdf2Hashing(string textToHash)
        {
            var hashedValue = KeyDerivation.Pbkdf2(
                password: textToHash,
                salt: Convert.FromBase64String(_pbkdf2Salt),
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 32);

            return Convert.ToBase64String(hashedValue);
        }
    }
}
