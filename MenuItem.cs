using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
     [Serializable]
    public  partial class MenuItem
    {
         [JsonProperty]
        public int MenuItemId { get; set; }
        // [JsonPropertyName("menu_id")]        
        // public int MenuId { get; set; }
         [JsonProperty]
        public int? SequenceNumber { get; set; }
         [JsonProperty]
        public string ItemName { get; set; }
          [JsonProperty]
        public string Controller { get; set; }
          [JsonProperty]
        public string Action { get; set; }
         [JsonProperty]
        public string Title { get; set; }
          [JsonProperty]
        public int? LinkId { get; set; }
          [JsonProperty]
        public bool? IsActive { get; set; }
    }
}
