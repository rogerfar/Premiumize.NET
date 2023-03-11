using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class CacheTest
{
    [Fact]
    public async Task Check()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Cache.Check(new List<String>
        {
            "magnet:?xt=urn:btih:dd8255ecdc7ca55fb0bbf81323d87062db1f6d1c&dn=Big+Buck+Bunny&tr=udp%3A%2F%2Fexplodie.org%3A6969&tr=udp%3A%2F%2Ftracker.coppersurfer.tk%3A6969&tr=udp%3A%2F%2Ftracker.empire-js.us%3A1337&tr=udp%3A%2F%2Ftracker.leechers-paradise.org%3A6969&tr=udp%3A%2F%2Ftracker.opentrackr.org%3A1337&tr=wss%3A%2F%2Ftracker.btorrent.xyz&tr=wss%3A%2F%2Ftracker.fastcast.nz&tr=wss%3A%2F%2Ftracker.openwebtorrent.com&ws=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2F&xs=https%3A%2F%2Fwebtorrent.io%2Ftorrents%2Fbig-buck-bunny.torrent",
            "https://webtorrent.io/torrents/cosmos-laundromat.torrent"
        });

        Assert.Equal(2, result.Response.Count);
    }
}
