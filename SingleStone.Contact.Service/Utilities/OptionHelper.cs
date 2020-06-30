using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SingleStone.Contact.Service.Utilities
{
    public class OptionHelper
    {
        public OptionHelper(IOptions<ContactOptions> optionsAccessor, IWebHostEnvironment env)
        {
            options = optionsAccessor;
            environment = env;
        }
        private readonly IWebHostEnvironment environment;

        private readonly IOptions<ContactOptions> options; //set in the startup from the appsettings
    }
}
