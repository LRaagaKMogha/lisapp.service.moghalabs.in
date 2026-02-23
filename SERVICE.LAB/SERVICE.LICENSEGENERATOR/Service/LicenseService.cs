using SERVICE.LICENSEGENERATOR.Model;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace SERVICE.LICENSEGENERATOR.Service
{
    public class LicenseService
    {
        private readonly string _privateKey;

        public LicenseService(string privateKey)
        {
            _privateKey = privateKey;
        }

        public string GenerateLicense(string hardwareId, DateTime expiryDate)
        {
            var license = new LicenseModel
            {
                HardwareId = hardwareId,
                ExpiryDate = expiryDate
            };

            string json = JsonSerializer.Serialize(license);
            byte[] dataBytes = Encoding.UTF8.GetBytes(json);

            using var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(Convert.FromBase64String(_privateKey), out _);

            byte[] signature = rsa.SignData(
                dataBytes,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            var wrapper = new LicenseWrapper
            {
                Data = Convert.ToBase64String(dataBytes),
                Signature = Convert.ToBase64String(signature)
            };

            string finalJson = JsonSerializer.Serialize(wrapper);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(finalJson));
        }
    }
}
