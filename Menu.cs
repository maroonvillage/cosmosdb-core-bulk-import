using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
     [Serializable]
    public partial class Menu
    {
        [JsonProperty]
         public string id { get; set; }
         [Newtonsoft.Json.JsonIgnore]
        public string MenuId { get; set; }
        [JsonProperty]
        public Marketplace MarketPlace { get; set; }
        [JsonProperty]
        public string MenuName { get; set; }
        [JsonProperty]
        public string MenuParentId { get; set; }
        [JsonProperty]
        public int? SiteId { get; set; }
        public IList<MenuItem> MenuItems { get; set; }

    }
}
