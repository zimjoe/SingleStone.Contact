using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Utilities
{
    public class ContactOptions
    {
        public ContactOptions() { }

        /// <summary>
        /// Name of the section in the config file
        /// </summary>
        public const string Contact = "Contact";

        /// <summary>
        /// This is the configurable database name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The application will set this at startup.
        /// I'm not 100% sure I love this method, but
        /// I don't want an application that has the 
        /// path hardcoded in the config file
        /// </summary>
        public static string WebRoot{ get; set; }

        /// <summary>
        /// This returns the connection string using the Database name,
        /// application root and the App_Data file
        /// </summary>
        public string GetConnectionString() {
            
            return Path.Combine(WebRoot, "App_Data", DatabaseName);

        }
    }
}
