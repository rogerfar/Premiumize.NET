using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PremiumizeNET;

internal class TransferListResponse
{
    [JsonProperty("status")]
    public String Status { get; set; }

    [JsonProperty("transfers")]
    public List<Transfer> Transfers { get; set; }
}

public class Transfer
{
    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("message")]
    public String Message { get; set; }

    /// <summary>
    ///     waiting, finished, running, deleted, banned, error, timeout, seeding, queued
    /// </summary>
    [JsonProperty("status")]
    public String Status { get; set; }

    [JsonProperty("progress")]
    public Double? Progress { get; set; }

    [JsonProperty("folder_id")]
    public String FolderId { get; set; }

    [JsonProperty("file_id")]
    public String FileId { get; set; }

    [JsonProperty("other_cloud_id")]
    public String OtherCloudId { get; set; }

    [JsonProperty("src")]
    public String Src { get; set; }
}
