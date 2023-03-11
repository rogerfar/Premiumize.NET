using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class ServicesTest
{
    [Fact]
    public async Task List()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Services.List();

        Assert.Equal(2, result.Queue.Count);
    }
}
