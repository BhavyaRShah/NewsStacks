using NewsStacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.RequestResponseModel
{
    public class ArticleResponse
    {
        public int articleid { get; set; }
        public string headline { get; set; }
        public string content { get; set; }
        public DateTime createddate { get; set; }
        public DateTime lastmodifieddate { get; set; }
        public int createdbyid { get; set; }
        public DateTime? submitteddate { get; set; }
        public int? submittedtoid { get; set; }
        public DateTime? publisheddate { get; set; }
        public int publishedbyid { get; set; }
        public Tag tag { get; set; }
        public UserResponse createdby { get; set; }
        public UserResponse submittedto { get; set; }
        public UserResponse publishedby { get; set; }

    }
}
