using System;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class User
{
    [JsonProperty("customer_id")]
    public Int64 CustomerId { get; set; }

    [JsonProperty("premium_until")]
    public Int64? PremiumUntil { get; set; }

    [JsonProperty("limit_used")]
    public Double LimitUsed { get; set; }

    [JsonProperty("space_used")]
    public Int64 SpaceUsed { get; set; }
}