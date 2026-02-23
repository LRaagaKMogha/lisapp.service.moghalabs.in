using SERVICE.LICENSEGENERATOR.Service;

Console.WriteLine("===== OFFLINE LICENSE GENERATOR =====");
Console.WriteLine();

Console.Write("Enter Hardware ID: ");
string hardwareId = Console.ReadLine();

Console.Write("Enter Expiry Date (yyyy-MM-dd): ");
string dateInput = Console.ReadLine();

if (!DateTime.TryParse(dateInput, out DateTime expiryDate))
{
    Console.WriteLine("Invalid Date Format!");
    return;
}

// 🔐 IMPORTANT: Replace with your real private key
string privateKey = "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwggSiAgEAAoIBAQCnX05Rk3uPc4tiREvDXEfHRwQPCeCO2j7t8fsvl1nXCfa4QgSifaMQaHS0Lp8CBHdhej27fAwCiNTvLnvyg7UPzc06rJOszr5vZl2gPhTKjWFZ2JWomCLcuVWFCxvpfhyufONLafs/aIFyiMmVzYWpjeuO5Qa6c0sbB7ajk9h2rDAnqiaXPDev9Va5ypnByfEDqhYy6TbVtDdwLrjux+DFc/5QA93jOxDZ/Q7twLQ86Ae8mtGSbqD1Wp8XqXpdJhpA3wm2E8ptLVUtCYsP1GkmNNSqItBQoHJSxoFCyls0E2qmikTBLtsvecvimZP+e0raZNx5VnjqYX76oPLsZDl5AgMBAAECggEAXXvpJb1HjWdPGfLlklevq8mppbCxoXibH5JB52IVBvwEtwQAzLV558dAMAe+PoU6HNMHOfneR2gf7vw6tMkFz672i2grD6FmUpiNgNxMBpqTHnjPlpxrFHfcIXSEAZWz9W3CErQ+Zjs72jo2xhpGJt8jC+w2JtwQvic/xvvkMtuwxCn96u1p9piWL+jj9BZ6/FhI4Todfoadro0wzIrd2jQhoQiS92HS0k14MtB/uu4MDkec03oqHNik9It43DuJY0Zn5kOF/PwiXdEfNLYlToMoeff2mdL8zm/6r8AYBZ5tmVQv/HSQcIVkzs649/wEDaoZ/foMWF98fzx22JAQ1QKBgQDMIOWqaewElhdxMVuTfVH9Rtq1qfjjk/NXByMYNgbjBXRXnpND0LKmdqHS5By9ITO7/ZJzIohhuHyslaYb9V3Ci7f46saesVnzec1PrMoaslN3xQ9ceIUoCFukYvbm6J7LpBca1HmVhqapOtYtORSXbYWCGMjnH18aiWj/D2wCCwKBgQDR51GvVCAVpN4Jum4k7W4wRKiRccasZH1hTs5wQ9n9kVLXSIquT+crkFuoWix2T7soHJTFceZOS3w6NjMjmv5poixoUGoPPssrMUi+SztNqGRRVy4fPotg/vG4GNNAyDQx2wAlKFMTc7PiwOpdP3XSG70mW727ZuBely/eEGFJCwKBgGcVleXhZ9dJFL2M93ocJ3OIfJqRI6eJ57FjYU2wuvman44//o6Yrh9yeXZOzFSWl9Gv1G1gWw8+Y3ekeyZTWu4MMPP7XCJ33b0fHZfG4qlotM4fLgq8skHtNpplf9pMTyT30NMzydLBFRkRJWfhE40FOg7hBVPye8yi5+smlpzRAoGAb83iuJz6qgyzKENhP9IoCxXHJBGSXWj5T8eGNk4t4t5xXbKKC+cLnyy5ZacCX9KAQhFXPQQ6RCH4/Zi5DJIWSXXUaYvLsmskFNGfiZzQ7cBwDtN9Aa9y1it13TV78Nmy04tvPFuKRKYwfut66khPHacgzTm4igV2JWwqVwIqj5kCgYAtUMtjOUmwDZf+m7y+S0Wy/JWinYDIlwO2Om4vl1XSMwOkiy7mk5jjO2gpixdwD5K5IANgqwNljDVPL8NEk3WOdAj0brrdfrrrag2S6blBoNqTAvmaqqfcMFSR7cn2pjmdTJdIunhyFHHmo+yFDvAclMiX3PpkFyjPzFHyl5ziRA==";

var service = new LicenseService(privateKey);

string licenseKey = service.GenerateLicense(hardwareId, expiryDate);

Console.WriteLine();
Console.WriteLine("===== GENERATED LICENSE KEY =====");
Console.WriteLine();
Console.WriteLine(licenseKey);
Console.WriteLine();
Console.WriteLine("Copy this key into appsettings.json under License:Key");