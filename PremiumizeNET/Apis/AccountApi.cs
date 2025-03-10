﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public interface IAccountApi
{
    /// <summary>
    ///     Get account info.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<User> InfoAsync(CancellationToken cancellationToken = default);
}

public class AccountApi : IAccountApi
{
    private readonly Requests _requests;

    internal AccountApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<User> InfoAsync(CancellationToken cancellationToken = default)
    {
        return await _requests.GetRequestAsync<User>("account/info", true, null, cancellationToken);
    }
}