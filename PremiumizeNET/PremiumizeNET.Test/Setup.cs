using System;

namespace PremiumizeNET.Test
{
    public static class Setup
    {
        public static String ApiKey => System.IO.File.ReadAllText(@"C:\Projects\Premiumize.NET\PremiumizeNET\PremiumizeNET.Test\secret.txt");
    }
}
