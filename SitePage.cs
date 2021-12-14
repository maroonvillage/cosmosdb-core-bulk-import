using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
    [JsonObject]
    public partial class SitePage
    {

        [JsonProperty]
        public string id { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string PageId { get; set; }
         //[JsonPropertyName("page_machine_name")]
        [JsonProperty]
        public string PageMachineName { get; set; }
         //[JsonPropertyName("page_title")]
        [JsonProperty]
        public string PageTitle { get; set; }
         //[JsonPropertyName("market_place")]
        [JsonProperty]
        public Marketplace MarketPlace { get; set; }
         //[JsonPropertyName("template")]
        [JsonProperty]
        public Template Template { get; set; }
         //[JsonPropertyName("blocks")]
        [JsonProperty]
        public List<Block> Blocks { get; set; }

    }
}
