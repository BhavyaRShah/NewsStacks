using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.RequestResponseModel
{
    public class TimezoneResponse
    {
        public int timezoneid { get; set; }
        public string name { get; set; }
        public string offset { get; set; }
    }
}
