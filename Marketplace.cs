using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
     [JsonObject]
    public partial class Marketplace
    {
        //[JsonPropertyName("id")]
        [JsonProperty]
        public string MarketplaceId { get; set; }
        //[JsonPropertyName("site_name")]
        [JsonProperty]
        public string Name { get; set; }
        //[JsonPropertyName("header_logo")]
        [JsonProperty]
        public string HeaderLogo { get; set; }
        //[JsonPropertyName("description")]
        [JsonProperty]
        public string Description { get; set; }
        //[JsonPropertyName("url")]
        [JsonProperty]
        public string Url { get; set; }
        //public MarketplaceSetting Settings { get; set; }
    }
}
