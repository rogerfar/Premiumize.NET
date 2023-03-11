using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public class ServicesApi
{
    private readonly Requests _requests;

    internal ServicesApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     Get a list of services.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<Service> List(CancellationToken cancellationToken = default)
    {
        return await _requests.PostRequestAsync<Service>("services/list", null, true, cancellationToken);
    }
}