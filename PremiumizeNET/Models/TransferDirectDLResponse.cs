using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PremiumizeNET;

public class TransferDirectDLResponse
{
    [JsonProperty("content")]
    public List<TransferDirectDLResultContent> Content { get; set; }

    [JsonProperty("location")]
    public String Location { get; set; }

    [JsonProperty("filename")]
    public String Filename { get; set; }

    [JsonProperty("filesize")]
    public Int64 Filesize { get; set; }
}

public class TransferDirectDLResultContent
{
    [JsonProperty("path")]
    public String Path { get; set; }

    [JsonProperty("size")]
    public Int64 Size { get; set; }

    [JsonProperty("link")]
    public String Link { get; set; }

    [JsonProperty("stream_link")]
    public String StreamLink { get; set; }

    [JsonProperty("transcode_status")]
    public String TranscodeStatus { get; set; }
}
