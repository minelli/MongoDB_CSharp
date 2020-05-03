using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB_CSharp.Models
{
    public class Filter
    {
        public string FilterName { get; private set; }
        public object FilterValue { get; private set; }
        public EFilterOperation Operation { get; private set; }

        public Filter(string filterName, object filterValue, EFilterOperation operation = EFilterOperation.AND)
        {
            this.FilterName = filterName;
            this.FilterValue = filterValue;
            this.Operation = operation;
        }
    }

    public enum EFilterOperation
    {
        AND,
        OR
    }
}
