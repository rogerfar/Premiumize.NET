using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PremiumizeNET.Apis;

internal class Requests
{
    private readonly HttpClient _httpClient;
    private readonly Store _store;

    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };

    public Requests(HttpClient httpClient, Store store)
    {
        _httpClient = httpClient;
        _store = store;
    }

    private async Task<String> Request(String url,
                                       Boolean requireAuthentication, 
                                       RequestType requestType,
                                       HttpContent data,
                                       IDictionary<String, String> parameters,
                                       CancellationToken cancellationToken)
    {
        parameters ??= new Dictionary<String, String>();

        if (requireAuthentication)
        {
            parameters.Add("apikey", _store.ApiKey);
        }

        var parametersString = String.Join("&", parameters.Select(m => $"{m.Key}={HttpUtility.UrlEncode(m.Value)}"));

        if (!url.StartsWith("http"))
        {
            url = $"{_store.ApiUrl}{url}?{parametersString}";
        }

        var response = requestType switch
        {
            RequestType.Get => await _httpClient.GetAsync(url, cancellationToken),
            RequestType.Post => await _httpClient.PostAsync(url, data, cancellationToken),
            RequestType.Put => await _httpClient.PutAsync(url, data, cancellationToken),
            RequestType.Delete => await _httpClient.DeleteAsync(url, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
        };

        var buffer = await response.Content.ReadAsByteArrayAsync();
        var text = Encoding.UTF8.GetString(buffer, 0, buffer.Length);

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            text = null;
        }
            
        return text;
    }
        
    private async Task<T> Request<T>(String url,
                                     Boolean requireAuthentication,
                                     RequestType requestType,
                                     HttpContent data,
                                     IDictionary<String, String> parameters,
                                     CancellationToken cancellationToken)
        where T : class, new()
    {
        var requestResult = await Request(url, requireAuthentication, requestType, data, parameters, cancellationToken);

        if (requestResult == null)
        {
            throw new Exception("API returned no result");
        }

        try
        {
            var responseResult = JsonConvert.DeserializeObject<Response>(requestResult, JsonSerializerSettings);

            if (responseResult != null && responseResult.Status != "success" && responseResult.Message != null)
            {
                throw new PremiumizeException(responseResult.Message);
            }

            var result = JsonConvert.DeserializeObject<T>(requestResult, JsonSerializerSettings);

            if (result == null)
            {
                throw new Exception("Response was null");
            }

            return result;
        }
        catch (JsonSerializationException ex)
        {
            throw new Exception($"Unable to deserialize Premiumize API response to {typeof(T).Name}. Response was: {requestResult}. {ex.Message}", ex);
        }
    }
        
    public async Task<T> GetRequestAsync<T>(String url, Boolean requireAuthentication, IDictionary<String, String> parameters, CancellationToken cancellationToken)
        where T : class, new()
    {
        return await Request<T>(url, requireAuthentication, RequestType.Get, null, parameters, cancellationToken);
    }
        
    public async Task<T> PostRequestAsync<T>(String url, IEnumerable<KeyValuePair<String, String>> data, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        var content = data != null ? new FormUrlEncodedContent(data) : null;
        return await Request<T>(url, requireAuthentication, RequestType.Post, content, null, cancellationToken);
    }

    public async Task<T> PostFileRequestAsync<T>(String url, Byte[] file, String folderId, Boolean requireAuthentication, CancellationToken cancellationToken)
        where T : class, new()
    {
        using var multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Headers.ContentType.MediaType = "multipart/form-data";

        var fileContent = new StreamContent(new MemoryStream(file));
        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data") 
        { 
            Name = "file",
            FileName = "1.torrent"
        };
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-bittorrent");

        var folderIdContent = new StringContent(folderId);
        folderIdContent.Headers.ContentDisposition = null;
        folderIdContent.Headers.ContentType = null;

        multipartFormDataContent.Add(fileContent);
        multipartFormDataContent.Add(folderIdContent, "folder_id");
            
        return await Request<T>(url, requireAuthentication, RequestType.Post, multipartFormDataContent, null, cancellationToken);
    }

    public async Task<T> UploadFileAsync<T>(String url, String token, Byte[] file, String fileName, String mimeType, CancellationToken cancellationToken)
        where T : class, new()
    {
        var fileContent = new StreamContent(new MemoryStream(file));
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
            
        var tokenContent = new StringContent(token);
        tokenContent.Headers.ContentDisposition = null;
        tokenContent.Headers.ContentType = null;

        using var multipartFormDataContent = new MultipartFormDataContent();
        multipartFormDataContent.Headers.ContentType.MediaType = "multipart/form-data";
        multipartFormDataContent.Add(fileContent, "file", fileName);
        multipartFormDataContent.Add(tokenContent, @"token");
            
        return await Request<T>(url, false, RequestType.Post, multipartFormDataContent, null, cancellationToken);
    }
        
    private enum RequestType
    {
        Get,
        Post,
        Put,
        Delete
    }
}