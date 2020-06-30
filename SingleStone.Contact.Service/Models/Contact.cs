using LiteDB;
using Microsoft.Extensions.Options;
using SingleStone.Contact.Service.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SingleStone.Contact.Service.Models
{

    public class Contact
    {
       
        [JsonPropertyName("id")]
        public int Id { get; set; }
       
        [JsonPropertyName("name")]
        public ContactName Name { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("phone")]
        public IList<Phone> Phones { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Return a contact based on the passed Id
        /// </summary>
        public static Contact GetById(int id, IOptions<ContactOptions> options) {
            using (var db = new LiteDatabase(options.Value.GetConnectionString()))
            {
                var collection = db.GetCollection<Contact>("contacts");
                var contact = collection.FindById(id);
                return contact;
            }
            //return null;
        }

        /// <summary>
        /// Returns all contacts in the system
        /// </summary>
        public static IEnumerable<Contact> GetAll(IOptions<ContactOptions> options) {
            var contacts = new List<Contact>();
            using (var db = new LiteDatabase(options.Value.GetConnectionString()))
            {
                var collection = db.GetCollection<Contact>("contacts");
                // return a lit of found "all"
                return collection.FindAll().ToList();
            }
        }

        /// <summary>
        /// Returns all contacts with a matching email
        /// </summary>
        public static IEnumerable<Contact> GetByEmail(string email, IOptions<ContactOptions> options)
        {
            using (var db = new LiteDatabase(options.Value.GetConnectionString()))
            {
                var collection = db.GetCollection<Contact>("contacts");
                var contacts = collection.Find(Query.Contains("Email", email)).ToList();
                return contacts;
            }
        }

        /// <summary>
        /// A bit of a short cut here.  Normally, I would build a repository
        /// Here I'm just shortcutting and combining the Model with the data layer.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public void Save(IOptions<ContactOptions> options) {
            if (Id > 0)
            {
                // make a new value
                using (var db = new LiteDatabase(options.Value.GetConnectionString()))
                {
                    var collection = db.GetCollection<Contact>("contacts");
                    //var contact = collection.FindById(Id);
                    // make updates to found item
                    
                    collection.Update(this);
                }
            }
            else
            {
                // make a new value
                using (var db = new LiteDatabase(options.Value.GetConnectionString()))
                {
                    var collection = db.GetCollection<Contact>("contacts");

                    collection.Insert(this);
                }
            }
        }

        public static void Delete(int id, IOptions<ContactOptions> options) {
            using (var db = new LiteDatabase(options.Value.GetConnectionString()))
            {
                var collection = db.GetCollection<Contact>("contacts");

                collection.Delete(id);
            }
        }
    }
}
