using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class CacheResult
{
    [JsonProperty("response")]
    public List<Boolean> Response { get; set; }

    [JsonProperty("transcoded")]
    public List<Boolean?> Transcoded { get; set; }

    [JsonProperty("filename")]
    public List<String> Filename { get; set; }

    [JsonProperty("filesize")]
    public List<Int64> Filesize { get; set; }
}
