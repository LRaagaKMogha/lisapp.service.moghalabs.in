using Service.API.Controllers;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

public static class LicenseValidator
{
    private static string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAp19OUZN7j3OLYkRLw1xHx0cEDwngjto+7fH7L5dZ1wn2uEIEon2jEGh0tC6fAgR3YXo9u3wMAojU7y578oO1D83NOqyTrM6+b2ZdoD4Uyo1hWdiVqJgi3LlVhQsb6X4crnzjS2n7P2iBcojJlc2FqY3rjuUGunNLGwe2o5PYdqwwJ6omlzw3r/VWucqZwcnxA6oWMuk21bQ3cC647sfgxXP+UAPd4zsQ2f0O7cC0POgHvJrRkm6g9VqfF6l6XSYaQN8JthPKbS1VLQmLD9RpJjTUqiLQUKByUsaBQspbNBNqpopEwS7bL3nL4pmT/ntK2mTceVZ46mF++qDy7GQ5eQIDAQAB";

    public static bool Validate(string licenseKey)
    {
        try
        {
            var wrapperJson = Encoding.UTF8.GetString(Convert.FromBase64String(licenseKey));
            var wrapper = JsonSerializer.Deserialize<LicenseWrapper>(wrapperJson);

            byte[] data = Convert.FromBase64String(wrapper.Data);
            byte[] signature = Convert.FromBase64String(wrapper.Signature);

            using var rsa = RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(PublicKey), out _);

            bool validSignature = rsa.VerifyData(
                data,
                signature,
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            if (!validSignature)
                return false;

            var license = JsonSerializer.Deserialize<LicenseModel>(Encoding.UTF8.GetString(data));

            HardwareHelper hardwareHelper = new HardwareHelper();

            if (license.HardwareId != hardwareHelper.GenerateHardwareId())
                return false;

            if (DateTime.UtcNow > license.ExpiryDate)
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }
}
public class LicenseWrapper
{
    public string Data { get; set; }
    public string Signature { get; set; }
}
public class LicenseModel
{
    public string HardwareId { get; set; }
    public DateTime ExpiryDate { get; set; }
}