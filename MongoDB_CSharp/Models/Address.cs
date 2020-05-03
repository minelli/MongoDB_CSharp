using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CSharp.Models
{
    public class Address
    {
        public string streetAddress { get; set; }
        public string number { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set;  }
    }
}
