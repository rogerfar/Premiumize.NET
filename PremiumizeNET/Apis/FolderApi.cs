using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PremiumizeNET;

public interface IFolderApi
{
    /// <summary>
    ///     List a folder.
    /// </summary>
    /// <param name="id">Id of the folder to be listed.</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<Item> ListAsync(String id = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Create a new folder.
    /// </summary>
    /// <param name="name">Name of the folder to be created</param>
    /// <param name="parentId">Id of the parent folder</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<String> CreateAsync(String name, String parentId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Rename a new folder.
    /// </summary>
    /// <param name="id">Id of the folder</param>
    /// <param name="name">Name of the folder to be created</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RenameAsync(String id, String name, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Paste multiple files or folders into a folder.
    /// </summary>
    /// <param name="files">List of file IDs to be moved</param>
    /// <param name="folders">List of folder IDs to be moved</param>
    /// <param name="id">ID of the folder to paste into</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PasteAsync(IEnumerable<String> files, IEnumerable<String> folders, String id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Delete a new folder.
    /// </summary>
    /// <param name="id">Id of the folder</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(String id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Upload a file to a folder.
    /// </summary>
    /// <param name="id">Id of the folder to be listed.</param>
    /// <param name="file">Byte array of the file.</param>
    /// <param name="fileName">The name of the file to upload.</param>
    /// <param name="mimeType">The content type of the file.</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task UploadAsync(String id, Byte[] file, String fileName, String mimeType, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Search a folder or files.
    /// </summary>
    /// <param name="query">Search your files</param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used by other objects or threads to receive notice of
    ///     cancellation.
    /// </param>
    Task<Item> SearchAsync(String query, CancellationToken cancellationToken = default);
}

public class FolderApi : IFolderApi
{
    private readonly Requests _requests;

    internal FolderApi(HttpClient httpClient, Store store)
    {
        _requests = new Requests(httpClient, store);
    }

    /// <inheritdoc />
    public async Task<Item> ListAsync(String id = null, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "includebreadcrumbs", "true"
            }
        };

        if (!String.IsNullOrWhiteSpace(id))
        {
            parameters.Add("id", id);
        }

        return await _requests.GetRequestAsync<Item>("folder/list", true, parameters, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<String> CreateAsync(String name, String parentId, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("name", name),
            new KeyValuePair<String, String>("parentId", parentId)
        };

        var response = await _requests.PostRequestAsync<Response>("folder/create", data, true, cancellationToken);

        return response.Id;
    }

    /// <inheritdoc />
    public async Task RenameAsync(String id, String name, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("id", id),
            new KeyValuePair<String, String>("name", name)
        };

        await _requests.PostRequestAsync<Response>("folder/rename", data, true, cancellationToken);
    }
        
    /// <inheritdoc />
    public async Task PasteAsync(IEnumerable<String> files, IEnumerable<String> folders, String id, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("id", id)
        };

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

        await _requests.PostRequestAsync<Response>("folder/paste", data, true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(String id, CancellationToken cancellationToken = default)
    {
        var data = new List<KeyValuePair<String, String>>
        {
            new KeyValuePair<String, String>("id", id)
        };

        await _requests.PostRequestAsync<Response>("folder/delete", data, true, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UploadAsync(String id, Byte[] file, String fileName, String mimeType, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            { "id", id }
        };

        var response = await _requests.GetRequestAsync<FolderUploadInfoResponse>("folder/uploadinfo", true, parameters, cancellationToken);

        await _requests.UploadFileAsync<Response>(response.Url, response.Token, file, fileName, mimeType, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Item> SearchAsync(String query, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<String, String>
        {
            {
                "q", query
            }
        };
            
        return await _requests.GetRequestAsync<Item>("folder/list", true, parameters, cancellationToken);
    }
}