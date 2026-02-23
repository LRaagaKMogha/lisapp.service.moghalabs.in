using System.Security.Cryptography;

using var rsa = RSA.Create(2048);

string publicKey = Convert.ToBase64String(rsa.ExportSubjectPublicKeyInfo());
string privateKey = Convert.ToBase64String(rsa.ExportPkcs8PrivateKey());

Console.WriteLine("PUBLIC KEY:");
Console.WriteLine(publicKey);
Console.WriteLine();
Console.WriteLine("PRIVATE KEY:");
Console.WriteLine(privateKey);