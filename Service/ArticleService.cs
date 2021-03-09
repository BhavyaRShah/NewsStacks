using NewsStacks.IService;
using NewsStacks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NewsStacks.RequestResponseModel;
using System.Security.Claims;
using NewsStacks.Common;

namespace NewsStacks.Service
{
    public class ArticleService : IArticleService
    {
        readonly NewsStacksContext _dbContext;


        private readonly IHttpContextAccessor _httpContextAccessor;

        public ArticleService(NewsStacksContext dbContext
            , IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Article> GetById(int articleId)
        {
            if (articleId != 0)
            {
                Article article = _dbContext.Articles.Where(x => x.Articleid == articleId).FirstOrDefault();
                return await System.Threading.Tasks.Task.FromResult(article);
            }
            else
                return null;
            
        }

       //the search parameter can be used to search the articles by tag
        public async Task<List<Article>> GetList(string search = "")
        {
            //get the user from the session and assign that user in createdbyid
            string userRole = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            //if userrole is publisher, then get all the articles that are published and those which are submitted to that publisher
            //if userrole is writer, then get all the articles that are published and those which are created by that writer
            //if userrole is user, then get only the published articles
            var articles = _dbContext.Articles.Where(x => x.Isdeleted == false && 
                                (userRole == USERROLES.PUBLISHER.ToString() ? x.Submittedtoid == userId || (x.Publishedbyid != null && x.Publisheddate != null) : (userRole == USERROLES.WRITER.ToString() ? x.Createdbyid == userId || (x.Publishedbyid != null && x.Publisheddate != null) : x.Publishedbyid != null && x.Publisheddate != null)) 
                                && (!string.IsNullOrEmpty(search) ? x.Tags.Where(y => y.Tagname.Contains(search)).Any() : true ))
                                .Include(x => x.Tags).Include(x => x.Publishedby).Include(x => x.Submittedto).Include(x => x.Createdby).ToList();
            return await System.Threading.Tasks.Task.FromResult(articles);
        }

            public async Task<dynamic> Create(Article article)
            {
                try
                {
                    if (article != null)
                    {
                        article.Createddate = DateTime.UtcNow;

                        article.Lastmodifieddate = DateTime.UtcNow;

                        //get the user from the session and assign that user in createdbyid
                        int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                        article.Createdbyid = userId;

                        _dbContext.Articles.Add(article);

                        article.Tags.ToList().ForEach(x => { x.Lastmodifieddate = DateTime.UtcNow; });
                        _dbContext.SaveChanges();

                        return await System.Threading.Tasks.Task.FromResult(article);
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    ex = ex.InnerException != null ? ex.InnerException : ex;
                    var response = new ErrorResponse { errorMessage = ex.Message};
                    return response;
                }
            
            }

            public async Task<Article> Update(Article article)
            {
                //var objUser = dbContext.Users.Where(x => x.Userid == user.Userid).FirstOrDefault();
                if (_dbContext.Articles.Where(x => x.Articleid == article.Articleid && x.Isdeleted == false).Any())
                {
                    if (article.Submittedtoid != null || article.Submitteddate != null)
                    {
                        throw new Exception(string.Format(ValidationMessages.ARTICLEALREADYSUBMITTED));
                    }    
                     article.Lastmodifieddate = DateTime.UtcNow;

                    _dbContext.Entry(article).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                    
                    
                article.Tags.ToList().ForEach(x =>
                {
                    x.Lastmodifieddate = DateTime.UtcNow;
                    if (x.Tagid != 0)
                    {
                        _dbContext.Entry(x).State = EntityState.Modified;
                    }
                    else
                    {
                        x.Articleid = article.Articleid;
                        _dbContext.Tags.Add(x);
                    }
                });

                _dbContext.SaveChanges();

                    return await System.Threading.Tasks.Task.FromResult(article);
                }
                else
                    throw new Exception(string.Format(ValidationMessages.DELETEDORUPDATED, PropertyNames.ARTICLE));
            }

            public async Task<int> Delete(DeleteRequest objArticle)
            {
                var article = _dbContext.Articles.Where(x => x.Articleid == objArticle.id).FirstOrDefault();
                if (article.Lastmodifieddate == objArticle.lastmodifieddate)
                {
                    article.Lastmodifieddate = DateTime.UtcNow;
                    article.Isdeleted = true;
                    _dbContext.Entry(article).State = EntityState.Modified;
                    _dbContext.SaveChanges();

                var tags = _dbContext.Tags.Where(x => x.Articleid == objArticle.id).ToList();

                tags.ForEach(x =>
                {
                    _dbContext.Entry(x).State = EntityState.Deleted;
                });

                _dbContext.SaveChanges();

                    return await System.Threading.Tasks.Task.FromResult(objArticle.id);
                }
                else
                    throw new Exception(string.Format(ValidationMessages.DELETEDORUPDATED, PropertyNames.ARTICLE));

            }

            public async Task<List<Article>> GetSubmittedArticles()
            {
                //get the publisher from the  session and then find the articles for that publisher
                int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
                string userRole = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);

                //get the data
                //if the user is a publisher, then get the articles which are submitted to that publisher
                //if the user is a writer, then get the articles that are submitted by that writer
                List<Article> articles = _dbContext.Articles.Where(x => x.Isdeleted == false && x.Publisheddate == null && x.Publishedbyid == null &&
                                        (userRole == USERROLES.PUBLISHER.ToString() ? x.Submittedtoid == userId 
                                        : (userRole == USERROLES.WRITER.ToString() ? x.Createdbyid == userId : true) )).Include(x => x.Tags).ToList();

                return await System.Threading.Tasks.Task.FromResult(articles);
            }

            public async Task<int> SubmitArticle(SubmitArticleRequest request)
            {
                Article article = _dbContext.Articles.Where(x => x.Articleid == request.articleId).FirstOrDefault();

           
                article.Lastmodifieddate = DateTime.UtcNow;
                article.Submitteddate = DateTime.UtcNow;
                article.Submittedtoid = request.submittedToId;
                _dbContext.Entry(article).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return await System.Threading.Tasks.Task.FromResult(article.Articleid);
            
            }

            public async Task<int> PublishArticle(PublishArticle request)
            {
                //get the publisher from the  session and then check if that publisher is able to publish that content or not
                int publisherId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

                Article article = _dbContext.Articles.Where(x => x.Articleid == request.articleId && x.Submittedtoid == publisherId).FirstOrDefault();

                article.Lastmodifieddate = DateTime.UtcNow;
                article.Publisheddate = DateTime.UtcNow;
                article.Publishedbyid = publisherId;
                _dbContext.Entry(article).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return await System.Threading.Tasks.Task.FromResult(article.Articleid);
            }

        //public IEnumerable<Article> GetPublishedArticles()


        //get the response for article along with tag details


        ///change the req/res model for every data model (especially article model)
        ///test the update of article with tags
        ///add the tag service 
        ///add async await 
        ///add user timezone specific data
        ///talk about deadlock of article and tag


        //public IEnumerable<ArticleResponse> CreateArticleResponse(long? articleId, string search = "")
        //{
            //var response = (from A in _dbContext.Articles
            //                join T in _dbContext.Tags on A.Articleid equals T.Articleid 
            //                join UC in _dbContext.Userdetails on A.Createdbyid equals UC.Userdetailid
            //                join US in _dbContext.Userdetails on A.Submittedtoid equals US.Userdetailid
            //                join UP in _dbContext.Userdetails on A.Publishedbyid equals UP.Userdetailid

            //                where
            //                A.Isdeleted == false &&
            //                !string.IsNullOrEmpty(search) ? (U.Username.Contains(search) || U.Firstname.Contains(search) || U.Lastname.Contains(search)) : 1 == 1 &&
            //                (userid != null && userid != 0) ? U.Userdetailid == userid : 1 == 1

            //                select new UserResponse
            //                {
            //                    userdetailid = U.Userdetailid,
            //                    firstname = U.Firstname,
            //                    lastname = U.Lastname,
            //                    timezoneid = U.Timezoneid,
            //                    username = U.Username,
            //                    ispublisher = U.Ispublisher,
            //                    iswriter = U.Iswriter,
            //                    lastmodifieddate = U.Lastmodifieddate,

            //                    timezone = new TimezoneResponse
            //                    {
            //                        timezoneid = T.Timezoneid,
            //                        name = T.Name,
            //                        offset = T.Offset
            //                    }
            //                }).OrderBy(x => x.userdetailid).ToList().Select(x => (UserResponse)x).ToList();

            //return response;
        //}
    }
}
