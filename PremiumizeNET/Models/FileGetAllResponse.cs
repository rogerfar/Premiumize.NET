using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class FileGetAllResponse
{
    [JsonProperty("status")]
    public String Status { get; set; }

    [JsonProperty("files")]
    public List<File> Files { get; set; }
}

public class File
{
    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("created_at")]
    public Int64 CreatedAt { get; set; }

    [JsonProperty("size")]
    public Int64 Size { get; set; }

    [JsonProperty("mime_type")]
    public String MimeType { get; set; }

    [JsonProperty("virus_scan")]
    public String VirusScan { get; set; }

    [JsonProperty("path")]
    public String Path { get; set; }
}