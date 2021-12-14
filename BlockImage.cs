using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
      [JsonObject]
    public partial class BlockImage
    {
        [JsonProperty]
        public string id { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string BlockImageId { get; set; }
        [JsonProperty]
        public int? BlockId { get; set; }
        [JsonProperty]
        public int? CarouselImageId { get; set; }
        [JsonProperty]
        public int? ImageId { get; set; }
        [JsonProperty]
        public bool IsLogo { get; set; }

    }
}
