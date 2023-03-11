using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class TransfersTest
{
    [Fact]
    public async Task CreateMagnet()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Transfers.CreateAsync("magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent", "");

        Assert.Equal("Big Buck Bunny", result.Name);
    }

    [Fact]
    public async Task CreateTorrentFile()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var file = await System.IO.File.ReadAllBytesAsync("big-buck-bunny.torrent");

        var result = await client.Transfers.CreateAsync(file, "");

        Assert.Equal("1.torrent", result.Name);
    }

    [Fact]
    public async Task CreateDirectDL()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Transfers.DirectDLAsync("magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent");

        Assert.Equal(3, result.Content.Count);
    }
    
    [Fact]
    public async Task List()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Transfers.ListAsync();

        Assert.Equal(3, result.Count);
    }
    
    [Fact]
    public async Task ClearFinished()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Transfers.ClearFinishedAsync();
    }
    
    [Fact]
    public async Task Delete()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Transfers.DeleteAsync("0BM3a7i39yiWHrpQ1MQzxw");
    }
}
