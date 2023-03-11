using System;
using System.Net.Http;
using PremiumizeNET.Apis;

namespace PremiumizeNET;

/// <summary>
///     The PremiumizeNET consumed the premiumize.me API.
///     Documentation about the API can be found here: https://app.swaggerhub.com/apis-docs/premiumize.me/api/1.7.1
/// </summary>
public class PremiumizeNETClient
{
    private readonly Store _store = new();

    public AccountApi Account { get; }
    public CacheApi Cache { get; }
    public FolderApi Folder { get; }
    public ItemApi Items { get; set; }
    public ServicesApi Services { get; set; }
    public TransferApi Transfers { get; set; }
    public ZipApi Zip { get; set; }

    /// <summary>
    ///     Initialize the PremiumizeNET API.
    ///     To use authentication provide the key for your user.
    /// </summary>
    /// <param name="apiKey">
    ///     The Premiumize API uses API keys to authenticate requests. You can view and manage your API keys on your account page:
    ///     https://www.premiumize.me/account
    /// </param>
    /// <param name="httpClient">
    ///     Optional HttpClient if you want to use your own HttpClient.
    /// </param>
    public PremiumizeNETClient(String apiKey, HttpClient httpClient = null)
    {
        var client = httpClient ?? new HttpClient();
            
        _store.ApiKey = apiKey;

        Account = new AccountApi(client, _store);
        Cache = new CacheApi(client, _store);
        Folder = new FolderApi(client, _store);
        Items = new ItemApi(client, _store);
        Services = new ServicesApi(client, _store);
        Transfers = new TransferApi(client, _store);
        Zip = new ZipApi(client, _store);
    }
}