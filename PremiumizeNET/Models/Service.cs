using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class Service
{
    [JsonProperty("cache")]
    public List<String> Cache { get; set; }

    [JsonProperty("directdl")]
    public List<String> Directdl { get; set; }

    [JsonProperty("queue")]
    public List<String> Queue { get; set; }

    [JsonProperty("fairusefactor")]
    public Dictionary<String, Int64> Fairusefactor { get; set; }

    [JsonProperty("aliases")]
    public Dictionary<String, List<String>> Aliases { get; set; }

    [JsonProperty("regexpatterns")]
    public Dictionary<String, List<String>> Regexpatterns { get; set; }
}
