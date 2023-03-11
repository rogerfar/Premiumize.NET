using System;
using Newtonsoft.Json;

namespace PremiumizeNET;

internal class Response
{
    [JsonProperty("status")]
    public String Status { get; set; }

    [JsonProperty("message")]
    public String Message { get; set; }
    
    [JsonProperty("id")]
    public String Id { get; set; }

    [JsonProperty("location")]
    public String Location { get; set; }
}
