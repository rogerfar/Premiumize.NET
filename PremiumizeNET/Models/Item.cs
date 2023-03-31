using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class Item
{
    [JsonProperty("status")]
    public String Status { get; set; }

    [JsonProperty("content")]
    public IList<ItemContent> Content { get; set; }

    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("parent_id")]
    public String ParentId { get; set; }

    [JsonProperty("folder_id")]
    public String FolderId { get; set; }

    [JsonProperty("breadcrumbs")]
    public IList<Breadcrumb> Breadcrumbs { get; set; }
}

public class Breadcrumb
{
    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("name")]
    public String Name { get; set; }
}

public class ItemContent
{
    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("user_id")]
    public Int64 UserId { get; set; }

    [JsonProperty("customer_id")]
    public Int64 CustomerId { get; set; }

    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("size")]
    public Int64 Size { get; set; }

    [JsonProperty("created_at")]
    public Int64 CreatedAt { get; set; }

    [JsonProperty("transcode_status")]
    public String TranscodeStatus { get; set; }

    [JsonProperty("folder_id")]
    public String FolderId { get; set; }

    [JsonProperty("server_name")]
    public String ServerName { get; set; }

    [JsonProperty("acodec")]
    public String Acodec { get; set; }

    [JsonProperty("vcodec")]
    public String Vcodec { get; set; }

    [JsonProperty("mime_type")]
    public String MimeType { get; set; }

    [JsonProperty("opensubtitles_hash")]
    public String OpensubtitlesHash { get; set; }

    [JsonProperty("resx")]
    public Int64 Resx { get; set; }

    [JsonProperty("resy")]
    public Int64 Resy { get; set; }

    [JsonProperty("duration")]
    public Double Duration { get; set; }

    [JsonProperty("virus_scan")]
    public String VirusScan { get; set; }

    [JsonProperty("audio_track_names")]
    public List<String> AudioTrackNames { get; set; }

    [JsonProperty("type")]
    public String Type { get; set; }

    [JsonProperty("link")]
    public String Link { get; set; }

    [JsonProperty("stream_link")]
    public String StreamLink { get; set; }
}
