using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.RequestResponseModel
{
    public class DeleteRequest
    {
        public int id { get; set; }
        public DateTime lastmodifieddate { get; set; }
    }
}
