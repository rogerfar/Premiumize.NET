using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class ZipTest
{
    [Fact]
    public async Task Generate()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Zip.Generate(new List<String>(), new List<String>
        {
            "eqPE3WRNgRudMaukdMBJHA"
        });

        Assert.Equal("", result);
    }
}
