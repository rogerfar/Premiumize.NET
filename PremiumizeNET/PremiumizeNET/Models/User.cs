using System;
using Newtonsoft.Json;

namespace PremiumizeNET
{
    public class User
    {
        /// <summary>
        ///     Status.
        /// </summary>
        [JsonProperty("status")]
        public String Status { get; set; }

        /// <summary>
        ///     Customer ID.
        /// </summary>
        [JsonProperty("customer_id")]
        public Int64 CustomerId { get; set; }

        /// <summary>
        ///     Premium until.
        /// </summary>
        [JsonProperty("premium_until")]
        public Int64? PremiumUntil { get; set; }

        /// <summary>
        ///     Limit used.
        /// </summary>
        [JsonProperty("limit_used")]
        public Int64 LimitUsed { get; set; }

        /// <summary>
        ///     Space used.
        /// </summary>
        [JsonProperty("space_used")]
        public Int64 SpaceUsed { get; set; }
    }
}
