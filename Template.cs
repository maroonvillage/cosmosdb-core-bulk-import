using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace cosmosdb_core_bulk_import
{
     [JsonObject]
    public partial class Template
    {
        [JsonProperty]
         //[JsonPropertyName("id")]
        public string TemplateId { get; set; }
         //[JsonPropertyName("template_name")]
          [JsonProperty]
        public string TemplateName { get; set; }
        // [JsonPropertyName("template_machine_name")]
         [JsonProperty]
        public string TemplateMachineName { get; set; }
         //[JsonPropertyName("is_mobile")]
          [JsonProperty]
        public bool IsMobile { get; set; }

   


    }
}
