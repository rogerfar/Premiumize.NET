using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class ItemsTest
{
    [Fact]
    public async Task ListAll()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Items.ListAllAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task Delete()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Items.DeleteAsync("qh67CXd4LDwvUglxaeaCzA");

        Assert.True(true);
    }
    
    [Fact]
    public async Task Rename()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Items.RenameAsync("KFPbCFGske0u0AspxhoEEQ", "Test2.txt");

        Assert.True(true);
    }
    
    [Fact]
    public async Task Details()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Items.DetailsAsync("aVkQ-7I5Zep9p2f0H2mjug");

        Assert.Equal("Big Buck Bunny.mp4", result.Name);
    }
}
