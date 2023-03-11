using System;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class TransferCreateResponse
{
    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("name")]
    public String Name { get; set; }

    [JsonProperty("type")]
    public String Type { get; set; }
}
