using System;

namespace PremiumizeNET;

public class PremiumizeException : Exception
{
    public PremiumizeException(String error)
        : base(error)
    {

    }
}