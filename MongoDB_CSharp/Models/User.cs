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
        public bool active { get; set; } = true;
        public Address address { get; set; }

        public string GetAllInfos()
        {
            return $"User: {name} - email: {email} - password: {password} - active: {active} {this.GetAddress()}";
        }

        private string GetAddress()
        {
            if (address == null)
                return "";

            return $"{Environment.NewLine}Address: {address.number}, {address.streetAddress} - {address.city} {address.province} {address.country} { Environment.NewLine} ";
        }
    }
}
