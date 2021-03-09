using NewsStacks.Model;
using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.IService
{
    public interface IArticleService
    {
        Task<Article> GetById(int articleId);

        Task<List<Article>> GetList(string search = "");

        Task<dynamic> Create(Article article);

        Task<Article> Update(Article article);

        Task<int> Delete(DeleteRequest objArticle);

        Task<int> SubmitArticle(SubmitArticleRequest request);

        Task<int> PublishArticle(PublishArticle request);

        Task<List<Article>> GetSubmittedArticles();
    }
}
