using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SingleStone.Contact.Service.Models;
using SingleStone.Contact.Service.Utilities;

namespace SingleStone.Contact.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public ContactsController(IOptions<ContactOptions> optionsAccessor, IWebHostEnvironment env)
        {
            options = optionsAccessor;
            environment = env;
        }
        private readonly IWebHostEnvironment environment;

        private readonly IOptions<ContactOptions> options; //set in the startup from the appsettings

        [HttpGet]
        public IEnumerable<Models.Contact> Get()
        {
            return Models.Contact.GetAll(options);

            
            //return contacts;
        }

        // GET /<EmailController>/5
        [HttpGet("{id}")]
        public Models.Contact Get(int id)
        {
            // should throw 401 when not found rather than empty 
            return Models.Contact.GetById(id, options);
        }


        // POST /<EmailController>
        [HttpPost]
        public void Post( Models.Contact value)
        {
             value.Save(options);
        }

        // PUT /<EmailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Models.Contact value)
        {
            if (id > 0) {
                value.Id = id;
                value.Save(options);
            }

        }

        // DELETE /<EmailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // should throw error when not found?
            Models.Contact.Delete(id, options);
        }

    }
}
