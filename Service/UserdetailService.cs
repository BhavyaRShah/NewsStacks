using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsStacks.Common;
using NewsStacks.IService;
using NewsStacks.Model;
using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.Service
{
    public class UserdetailService : IUserDetailService
    {
        readonly NewsStacksContext dbContext;


        public UserdetailService(
            NewsStacksContext _db)
        {
            dbContext = _db;
        }

        public async Task<Userdetail> GetUserById(int userId)
        {
            var user = dbContext.Userdetails.Where(x => x.Userdetailid == userId).Include(x => x.Timezone).FirstOrDefault();
            return await System.Threading.Tasks.Task.FromResult(user);
        }

        public async Task<List<Userdetail>> GetUsers(string search = "")
        {
            var users = dbContext.Userdetails.Include(x => x.Timezone).ToList();

            users.ForEach(x =>
            {
                x.Lastmodifieddate = Common.Common.ConvertToUserTimezone(x.Lastmodifieddate, x.Timezone.Name);
            });

            return await System.Threading.Tasks.Task.FromResult(users);
        }

        public async Task<dynamic> Create(Userdetail user)
        {
            if (user != null)
            {
                //validate for duplicate username
                if (dbContext.Userdetails.Where(x => x.Username == user.Username).Any())
                    throw new Exception(string.Format(ValidationMessages.DUPLICATE, PropertyNames.USER));

                //add lastmodified date
                user.Lastmodifieddate = DateTime.UtcNow;

                //add the user to the db and save changes
                dbContext.Add(user);
                dbContext.SaveChanges();
                return await System.Threading.Tasks.Task.FromResult(user);
            }
            return null;

        }

        public async Task<dynamic> Update(Userdetail user)
        {
            if (dbContext.Userdetails.Where(x => x.Username == user.Username && x.Userdetailid != user.Userdetailid).Any())
                throw new Exception(string.Format(ValidationMessages.DUPLICATE, PropertyNames.USER));

            //var objUser = dbContext.Users.Where(x => x.Userid == user.Userid).FirstOrDefault();
            if (dbContext.Userdetails.Where(x => x.Userdetailid == user.Userdetailid && x.Lastmodifieddate == user.Lastmodifieddate).Any())
            {
                user.Lastmodifieddate = DateTime.UtcNow;
                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.SaveChanges();
                return await System.Threading.Tasks.Task.FromResult(user);
            }
            else
                throw new Exception(string.Format(ValidationMessages.DELETEDORUPDATED, PropertyNames.USER));
        }

        public async Task<int> Delete(DeleteRequest objUser)
        {
            var user = dbContext.Userdetails.Where(x => x.Userdetailid == objUser.id).FirstOrDefault();
            if (user.Lastmodifieddate == objUser.lastmodifieddate)
            {
                user.Lastmodifieddate = DateTime.UtcNow;
                user.Isdeleted = true;
                dbContext.Entry(user).State = EntityState.Modified;
                dbContext.SaveChanges();
                return await System.Threading.Tasks.Task.FromResult(objUser.id);
            }
            else
                throw new Exception(string.Format(ValidationMessages.DELETEDORUPDATED, PropertyNames.USER));
            
        }

        ///<summary>
        ///To get all the publishers
        ///</summary>
        public async Task<List<Userdetail>> GetPublishers(string search = "")
        {
            List<Userdetail> publishers = dbContext.Userdetails.Where(x => x.Ispublisher == true && x.Isdeleted == false &&
            (!string.IsNullOrEmpty(search) ? x.Firstname.Contains(search) || x.Lastname.Contains(search) || x.Username.Contains(search) : 1 == 1)).Include(x => x.Timezone).ToList();

            publishers.ForEach(x =>
            {
                x.Lastmodifieddate = Common.Common.ConvertToUserTimezone(x.Lastmodifieddate, x.Timezone.Name);
            });
           
            return await System.Threading.Tasks.Task.FromResult(publishers);
        }

        ///<summary>
        ///To get all the writers
        ///</summary>
        public async Task<List<Userdetail>> GetWriters(string search = "")
        {
            List<Userdetail> writers = dbContext.Userdetails.Where(x => x.Iswriter == true && x.Isdeleted == false &&
            (!string.IsNullOrEmpty(search) ? x.Firstname.Contains(search) || x.Lastname.Contains(search) || x.Username.Contains(search) : 1 == 1)).Include(x => x.Timezone).ToList();

            writers.ForEach(x =>
            {
                x.Lastmodifieddate = Common.Common.ConvertToUserTimezone(x.Lastmodifieddate, x.Timezone.Name);
            });
            
            return await System.Threading.Tasks.Task.FromResult(writers);
        }

        //public IEnumerable<UserResponse> CreateUserResponse(long? userid,string search = "")
        //{
        //    var response = (from U in dbContext.Userdetails
        //                    join T in dbContext.Timezones on U.Timezoneid equals T.Timezoneid

        //                    where
        //                    U.Isdeleted == false &&
        //                    !string.IsNullOrEmpty(search) ? (U.Username.Contains(search) || U.Firstname.Contains(search) || U.Lastname.Contains(search)) : 1 == 1 &&
        //                    (userid != null && userid != 0) ? U.Userdetailid == userid : 1 == 1

        //                    select new UserResponse
        //                    {
        //                        userdetailid = U.Userdetailid,
        //                        firstname = U.Firstname,
        //                        lastname = U.Lastname,
        //                        timezoneid = U.Timezoneid,
        //                        username = U.Username,
        //                        ispublisher = U.Ispublisher,
        //                        iswriter = U.Iswriter,
        //                        lastmodifieddate = U.Lastmodifieddate,

        //                        timezone = new TimezoneResponse
        //                        {
        //                            timezoneid = T.Timezoneid,
        //                            name = T.Name,
        //                            offset = T.Offset
        //                        }
        //                    }).OrderBy(x => x.userdetailid).ToList().Select(x => (UserResponse)x).ToList();

        //    return response;
        //}

    }
}
