using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CSharp.Models
{
    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
    }
}
