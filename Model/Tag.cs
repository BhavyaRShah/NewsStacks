using System;
using System.Collections.Generic;

#nullable disable

namespace NewsStacks.Model
{
    public partial class Tag
    {
        public int Tagid { get; set; }
        public int Articleid { get; set; }
        public string Tagname { get; set; }
        public DateTime Lastmodifieddate { get; set; }
        public bool Isdeleted { get; set; }

    }
}
