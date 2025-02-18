using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public interface ITransferApi
{
    /// <summary>
    ///     Create a transfer from an URL.
    /// </summary>
    /// <param name="src">Http(s) links to supported container files, links to any supported website and magnet links</param>
    /// <param name="folderId">Id of the target folder</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<TransferCreateResponse> CreateAsync(String src, String folderId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Create a transfer from a file.
    /// </summary>
    /// <param name="file">File (supported containerfiles, nzb, dlc)</param>
    /// <param name="folderId">Id of the target folder</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<TransferCreateResponse> CreateAsync(Byte[] file, String folderId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Create a direct download link.
    /// </summary>
    /// <param name="src">Http(s) links to supported container files, links to any supported website and magnet links</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<TransferDirectDLResponse> DirectDLAsync(String src, CancellationToken cancellationToken = default);

    /// <summary>
    ///     List transfers.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<IList<Transfer>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Clear finished transfers.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task ClearFinishedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete a transfer.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task DeleteAsync(String id, CancellationToken cancellationToken = default);
}

public class TransferApi : ITransferApi
{
    private readonly Requests _requests;

    internal TransferApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<TransferCreateResponse> CreateAsync(String src, String folderId, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("src", src),
            new KeyValuePair<String, String>("folder_id", folderId)
        };

        return await _requests.PostRequestAsync<TransferCreateResponse>("transfer/create", data, true, cancellationToken);
    }
        
    /// <inheritdoc />
    public async Task<TransferCreateResponse> CreateAsync(Byte[] file, String folderId, CancellationToken cancellationToken = default)
    {
        return await _requests.PostFileRequestAsync<TransferCreateResponse>("transfer/create", file, folderId, true, cancellationToken);
    }
        
    /// <inheritdoc />
    public async Task<TransferDirectDLResponse> DirectDLAsync(String src, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("src", src)
        };

        return await _requests.PostRequestAsync<TransferDirectDLResponse>("transfer/directdl", data, true, cancellationToken);
    }
        
    /// <inheritdoc />
    public async Task<IList<Transfer>> ListAsync(CancellationToken cancellationToken = default)
    {
        var result = await _requests.PostRequestAsync<TransferListResponse>("transfer/list", null, true, cancellationToken);

        return result.Transfers;
    }

    /// <inheritdoc />
    public async Task ClearFinishedAsync(CancellationToken cancellationToken = default)
    {
        await _requests.PostRequestAsync<TransferListResponse>("transfer/clearfinished", null, true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(String id, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("id", id)
        };

        await _requests.PostRequestAsync<Response>("transfer/delete", data, true, cancellationToken);
    }
}