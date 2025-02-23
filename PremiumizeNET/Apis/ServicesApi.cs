using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public interface IServicesApi
{
    /// <summary>
    ///     Get a list of services.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<Service> List(CancellationToken cancellationToken = default);
}

public class ServicesApi : IServicesApi
{
    private readonly Requests _requests;

    internal ServicesApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<Service> List(CancellationToken cancellationToken = default)
    {
        return await _requests.PostRequestAsync<Service>("services/list", null, true, cancellationToken);
    }
}