using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Models
{
    public class Phone
    {
        [JsonPropertyName("number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("type")]
        public string PhoneType { get; set; }

       
    }

    /// <summary>
    /// This is a little hacky, but it provides nice intelisense
    /// </summary>
    public static class PhoneTypes {
        public const string Home = "home";
        public const string Work = "work";
        public const string Mobile = "mobile";

        public static IEnumerable<string> AllTypes {
            get {
                return new List<string> { Home, Work, Mobile };
            }
        }
    }
}
