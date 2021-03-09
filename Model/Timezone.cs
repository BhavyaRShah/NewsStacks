using System;
using System.Collections.Generic;

#nullable disable

namespace NewsStacks.Model
{
    public partial class Timezone
    {
        public Timezone()
        {
            //Userdetails = new HashSet<Userdetail>();
        }

        public int Timezoneid { get; set; }
        public string Name { get; set; }
        public string Offset { get; set; }

    }
}
