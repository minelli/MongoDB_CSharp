using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CSharp.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public bool active { get; set; }

        public string GetAllInfos() { return $"User: {name} - email: {email} - password: {password} - active: {active}";  }
    }
}
