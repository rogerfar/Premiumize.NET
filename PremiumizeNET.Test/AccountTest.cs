using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class AccountTest
{
    [Fact]
    public async Task GetUser()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Account.InfoAsync();

        Assert.Equal(123, result.CustomerId);
    }
}