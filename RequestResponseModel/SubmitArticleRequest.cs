using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.RequestResponseModel
{
    public class SubmitArticleRequest
    {
        public int articleId { get; set; }
        public int submittedToId { get; set; }
    }

    public class PublishArticle
    {
        public int articleId { get; set; }
    }
}
