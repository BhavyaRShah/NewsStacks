using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.RequestResponseModel
{
    public class UserResponse
    {
        public int userdetailid { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public int timezoneid { get; set; }
        public bool iswriter { get; set; }
        public bool ispublisher { get; set; }
        public string role { get; set; }
        public DateTime lastmodifieddate { get; set; }
        public TimezoneResponse timezone { get; set; }
    }
}
