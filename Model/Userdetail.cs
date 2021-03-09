using System;
using System.Collections.Generic;

#nullable disable

namespace NewsStacks.Model
{
    public partial class Userdetail
    {
        public Userdetail()
        {
        }

        public int Userdetailid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Userpassword { get; set; }
        public int Timezoneid { get; set; }
        public bool Iswriter { get; set; }
        public bool Ispublisher { get; set; }
        public DateTime Lastmodifieddate { get; set; }
        public bool Isdeleted { get; set; }

        public virtual Timezone Timezone { get; set; }
        
    }
}
