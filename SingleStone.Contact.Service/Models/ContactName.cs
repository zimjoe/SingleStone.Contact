using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Models
{
    public class ContactName
    {

        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("middle")]
        public string Middle { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }
}
