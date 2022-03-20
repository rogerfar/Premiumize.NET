using System;
using Newtonsoft.Json;

namespace PremiumizeNET.Models
{
    internal class ErrorResponse
    {
        /// <summary>
        ///     Status.
        /// </summary>
        [JsonProperty("status")]
        public String Status { get; set; }

        /// <summary>
        ///     Message.
        /// </summary>
        [JsonProperty("message")]
        public String Message { get; set; }
    }
}
