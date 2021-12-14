using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
     [JsonObject]
    public partial class Block
    {

        // [JsonProperty]
        // public string id { get; set; }
         //[JsonPropertyName("block_id")]
        [JsonProperty]
        public string BlockId { get; set; }
         //[JsonPropertyName("block_machine_name")]
           [JsonProperty]
        public string BlockMachineName { get; set; }
         //[JsonPropertyName("block_title")]
           [JsonProperty]
        public string BlockTitle { get; set; }
         //[JsonPropertyName("block_type_content")]
           [JsonProperty]
        public int BlockTypeOfContent { get; set; }
         //[JsonPropertyName("is_data_feed")]
           [JsonProperty]
        public bool? IsDataFeed { get; set; }
    }
}
