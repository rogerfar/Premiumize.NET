using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public class ItemApi
{
    private readonly Requests _requests;

    internal ItemApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <summary>
    ///     List all files.
    /// </summary>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<List<File>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await _requests.GetRequestAsync<FileGetAllResponse>("item/listall", true, null, cancellationToken);

        return response.Files;
    }

    /// <summary>
    ///     Delete an item.
    /// </summary>
    /// <param name="id">ID of the item</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task DeleteAsync(String id, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("id", id)
        };

        await _requests.PostRequestAsync<Response>("item/delete", data, true, cancellationToken);
    }
        
    /// <summary>
    ///     Rename an item.
    /// </summary>
    /// <param name="id">ID of the item</param>
    /// <param name="name">New name of the item</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task RenameAsync(String id, String name, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("id", id),
            new KeyValuePair<String, String>("name", name)
        };

        await _requests.PostRequestAsync<Response>("item/rename", data, true, cancellationToken);
    }

    /// <summary>
    ///     Shows details of the item.
    /// </summary>
    /// <param name="id">ID of the item</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    public async Task<ItemContent> DetailsAsync(String id, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            { "id", id }
        };

        return await _requests.GetRequestAsync<ItemContent>("item/details", true, parameters, cancellationToken);
    }
}