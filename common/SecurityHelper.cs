using System.Security.Cryptography;
using System.Text;

namespace cts_twister_api.common
{
    public static class SecurityHelper
    {
        private const int Keysize = 256;
        private const int DerivationIterations = 1000;
        private static readonly string DefaultMasterKey = "LOAL_API_Apps_Secret_Key_2026_Secure!";

        private static string GetMasterKey()
        {
            var envKey = Environment.GetEnvironmentVariable("APP_SECRET_KEY");
            return string.IsNullOrEmpty(envKey) ? DefaultMasterKey : envKey;
        }


        public static string Decrypt(string cipherText, string salt)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentException("El texto cifrado no puede estar vacío.");
            if (string.IsNullOrEmpty(salt))
                throw new ArgumentException("La sal (salt) no puede estar vacía.");

            try
            {
                byte[] ivAndEncryptedBytes = Convert.FromBase64String(cipherText);
                byte[] saltBytes = Convert.FromBase64String(salt);

                // Extraer el IV (primeros 16 bytes)
                byte[] ivBytes = new byte[16];
                byte[] encryptedBytes = new byte[ivAndEncryptedBytes.Length - 16];
                Buffer.BlockCopy(ivAndEncryptedBytes, 0, ivBytes, 0, ivBytes.Length);
                Buffer.BlockCopy(ivAndEncryptedBytes, 16, encryptedBytes, 0, encryptedBytes.Length);

                string masterKey = GetMasterKey();

                using (var passwordDerivation = new Rfc2898DeriveBytes(masterKey, saltBytes, DerivationIterations, HashAlgorithmName.SHA256))
                {
                    byte[] keyBytes = passwordDerivation.GetBytes(Keysize / 8);

                    using (var symmetricKey = Aes.Create())
                    {
                        symmetricKey.BlockSize = 128;
                        symmetricKey.Mode = CipherMode.CBC;
                        symmetricKey.Padding = PaddingMode.PKCS7;

                        using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivBytes))
                        using (var memoryStream = new MemoryStream(encryptedBytes))
                        using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                        using (var streamReader = new StreamReader(cryptoStream, Encoding.UTF8))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Error al desencriptar el texto. La clave o sal son inválidas.", ex);
            }
        }
    }
}
