using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PremiumizeNET.Test;

public class HostsTest
{
    [Fact]
    public async Task List()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Folder.ListAsync();

        Assert.Equal("root", result.Name);
    }

    [Fact]
    public async Task ListFiles()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Folder.ListAsync("W3mnGcM3IyzGEq0Jr-TMfg");

        Assert.Equal("root", result.Name);
    }

    [Fact]
    public async Task ListErr()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        String error = null;

        try
        {
            var result = await client.Folder.ListAsync("FAKE!!");
        }
        catch (PremiumizeException ex)
        {
            error = ex.Message;
        }

        Assert.Equal("Could not decode folder id", error);
    }

    [Fact]
    public async Task Create()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Folder.CreateAsync("test", "");

        Assert.Equal("ez1CYSnOF6wN2bso5Y-qpA", result);
    }

    [Fact]
    public async Task Rename()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Folder.RenameAsync("ez1CYSnOF6wN2bso5Y-qpA", "test2");

        Assert.True(true);
    }

    [Fact]
    public async Task Paste()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Folder.PasteAsync(new List<String>(),
                                       new List<String>
                                       {
                                           "ia0ICHuHRPaAeH_XV3Wxsw",
                                           "H30L5ZLNotz5Uy4DsqDD8w"
                                       },
                                       "l5exiKTIEWLDeOfULOaZNA");

        Assert.True(true);
    }

    [Fact]
    public async Task Delete()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        await client.Folder.DeleteAsync("ez1CYSnOF6wN2bso5Y-qpA");

        Assert.True(true);
    }

    [Fact]
    public async Task UploadFile()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var file = await System.IO.File.ReadAllBytesAsync($"Test.txt");
        await client.Folder.UploadAsync("jniMy7YoccX-Mb1ea-LhRQ", file, "Test.txt", "text/plain");

        Assert.True(true);
    }

    [Fact]
    public async Task Search()
    {
        var client = new PremiumizeNETClient(Setup.ApiKey);

        var result = await client.Folder.SearchAsync("Test");

        Assert.Equal("root", result.Name);
    }
}
