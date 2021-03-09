//using Microsoft.EntityFrameworkCore;
//using NewsStacks.Common;
//using NewsStacks.IService;
//using NewsStacks.Model;
//using NewsStacks.RequestResponseModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NewsStacks.Service
//{
//    public class TagService : ITagService
//    {
//        private readonly NewsStacksContext _dbContext;

//        public TagService(NewsStacksContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public IEnumerable<Tag> GetTags(int? articleId, string search = "")
//        {
//            IEnumerable<Tag> tags = _dbContext.Tags.Where(x => x.Isdeleted == false && ((articleId != null || articleId != 0) ? x.Articleid == articleId : true) && (!string.IsNullOrEmpty(search) ? x.Tagname.Contains(search) : true)).ToList();
//            return tags;
//        }

//        public Tag Create(int articleId, Tag tag)
//        {
//            //if (tag != null)
//            //{
//            //    tag.Articleid = articleId;
//            //    tag.Lastmodifieddate = DateTime.UtcNow;
//            //    _dbContext.Add(tag);
//            //    _dbContext.SaveChanges();
//            //    return tag;
//            //}
//            return null;
//        }

//        public Tag Update(Tag tag)
//        {
//            if (_dbContext.Tags.Where(x => x.Tagid == tag.Tagid && x.Lastmodifieddate == tag.Lastmodifieddate).Any())
//            {
//                tag.Lastmodifieddate = DateTime.UtcNow;
//                _dbContext.Entry(tag).State = EntityState.Modified;
//                _dbContext.SaveChanges();
//                return tag;
//            }
//            else
//                throw new Exception(string.Format(ValidationMessages.DELETEDORUPDATED, PropertyNames.TAG));
//        }

//        public int Delete(DeleteRequest request)
//        {
//            var tag = _dbContext.Tags.Where(x => x.Tagid == request.id).FirstOrDefault();
//            if (tag.Lastmodifieddate == request.lastmodifieddate)
//            {
//                tag.Lastmodifieddate = DateTime.UtcNow;
//                tag.Isdeleted = true;
//                _dbContext.Entry(tag).State = EntityState.Modified;
//                _dbContext.SaveChanges();
//                return tag.Tagid;
//            }
//            else
//                throw new Exception(string.Format(ValidationMessages.DELETEDORUPDATED, PropertyNames.TAG));
//        }
//    }
//}
