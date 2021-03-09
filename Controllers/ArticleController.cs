using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsStacks.IService;
using NewsStacks.Model;
using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.Controllers
{
    [Route("api/Article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
     
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        [Route("{articleId}")]
        public Task<Article> GetById(int articleId)
        {
            var result =  _articleService.GetById(articleId: articleId).Result;
            return Task.FromResult(result);
        }

        [HttpGet]
        [Route("")]
        public Task<List<Article>> GetArticles(string search = "")
        {
            var result = _articleService.GetList(search: search).Result;
            return Task.FromResult(result);
        }

        [Authorize(Roles = "WRITER")]
        [HttpPost]
        [Route("")]
        public Task<dynamic> Create(Article article)
        {
            var result = _articleService.Create(article: article).Result;
            return Task.FromResult(result);
        }

        [Authorize(Roles = "WRITER")]
        [HttpPut]
        [Route("{articleId}")]
        public Task<Article> Update(Article article)
        {
            var result = _articleService.Update(article: article).Result;
            return Task.FromResult(result);
        }

        [Authorize(Roles = "WRITER")]
        [HttpDelete]
        [Route("{articleId}")]
        public Task<int> Delete(DeleteRequest objArticle)
        {
            var result = _articleService.Delete(objArticle: objArticle).Result;
            return Task.FromResult(result);
        }

        [Authorize(Roles = "WRITER")]
        [HttpPost]
        [Route("SubmitArticle/{articleId}")]
        public Task<int> SubmitArticle([FromBody] SubmitArticleRequest request)
        {
            var result = _articleService.SubmitArticle(request: request).Result;
            return Task.FromResult(result);
        }

        [Authorize(Roles = "PUBLISHER,WRITER")]
        [Route("GetSubmitted")]
        public Task<List<Article>> GetSubmittedArticles()
        {
            var result = _articleService.GetSubmittedArticles().Result;
            return Task.FromResult(result);
        }

        [Authorize(Roles = "PUBLISHER")]
        [HttpPost]
        [Route("PublishArticle/{articleId}")]
        public Task<int> PublishArticle([FromBody] PublishArticle request)
        {
            var result = _articleService.PublishArticle(request: request).Result;
            return Task.FromResult(result);
        }


    }
}
