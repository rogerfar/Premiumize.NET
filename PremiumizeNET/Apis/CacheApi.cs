using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET.Apis;

public class CacheApi
{
    private readonly Requests _requests;

    internal CacheApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     Check supported links availability.
    /// </summary>
    /// <param name="items">Items to check</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
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