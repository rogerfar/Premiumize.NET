using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public interface IZipApi
{
    /// <summary>
    ///     Check a zip.
    /// </summary>
    /// <param name="files"></param>
    /// <param name="folders"></param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<String> Generate(IList<String> files, IList<String> folders, CancellationToken cancellationToken = default);
}

public class ZipApi : IZipApi
{
    private readonly Requests _requests;

    internal ZipApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<String> Generate(IList<String> files, IList<String> folders, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>();

        if (files != null)
        {
            foreach (var file in files)
            {
                data.Add(new KeyValuePair<String, String>("files[]", file));
            }
        }

        if (folders != null)
        {
            foreach (var folder in folders)
            {
                data.Add(new KeyValuePair<String, String>("folders[]", folder));
            }
        }

        var result = await _requests.PostRequestAsync<Response>("zip/generate", data, true, cancellationToken);

        return result.Location;
    }
}