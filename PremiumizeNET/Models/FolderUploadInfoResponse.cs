using System;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class FolderUploadInfoResponse
{
    [JsonProperty("status")]
    public String Status { get; set; }

    [JsonProperty("token")]
    public String Token { get; set; }

    [JsonProperty("url")]
    public String Url { get; set; }
}
