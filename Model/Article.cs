using System;
using System.Collections.Generic;

#nullable disable

namespace NewsStacks.Model
{
    public partial class Article
    {
        public Article()
        {
            Tags = new HashSet<Tag>();
        }

        public int Articleid { get; set; }
        public string Headline { get; set; }
        public string Content { get; set; }
        public DateTime Createddate { get; set; }
        public int Createdbyid { get; set; }
        public DateTime? Submitteddate { get; set; }
        public int? Submittedtoid { get; set; }
        public DateTime? Publisheddate { get; set; }
        public int? Publishedbyid { get; set; }
        public DateTime Lastmodifieddate { get; set; }
        public bool Isdeleted { get; set; }

        public virtual Userdetail Createdby { get; set; }
        public virtual Userdetail Publishedby { get; set; }
        public virtual Userdetail Submittedto { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
