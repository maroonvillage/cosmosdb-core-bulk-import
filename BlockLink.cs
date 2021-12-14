using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
      [JsonObject]
    public partial class BlockLink
    {
        [JsonProperty]
         public string id { get; set; }
         [Newtonsoft.Json.JsonIgnore]
        public string BlockLinkId { get; set; }
        [JsonProperty]
        public int BlockId { get; set; }
        [JsonProperty]
        public int LinkId { get; set; }

    }
}
