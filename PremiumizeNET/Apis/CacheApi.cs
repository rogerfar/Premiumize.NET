using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public interface ICacheApi
{
    /// <summary>
    ///     Check supported links availability.
    /// </summary>
    /// <param name="items">Items to check</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<CacheResult> Check(IList<String> items, CancellationToken cancellationToken = default);
}

public class CacheApi : ICacheApi
{
    private readonly Requests _requests;

    internal CacheApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<CacheResult> Check(IList<String> items, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>();

        if (items != null)
        {
            foreach (var item in items)
            {
                data.Add(new KeyValuePair<String, String>("items[]", item));
            }
        }

        return await _requests.PostRequestAsync<CacheResult>("cache/check", data, true, cancellationToken);
    }
}