using NewsStacks.Model;
using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.IService
{
    public interface IUserDetailService
    {
        ///<summary>
        ///To get the User detail by userId
        ///</summary>
        Task<Userdetail> GetUserById(int userId);

        ///<summary>
        ///To get multiple Users according to the criteria
        ///</summary>
        Task<List<Userdetail>> GetUsers(string search = "");

        ///<summary>
        ///To create a user
        ///</summary>
        Task<dynamic> Create(Userdetail user);

        ///<summary>
        ///To update the User detail of a user
        ///</summary>
        Task<dynamic> Update(Userdetail user);

        ///<summary>
        ///To delete the User detail by userId
        ///</summary>
        Task<int> Delete(DeleteRequest objUser);

        ///<summary>
        ///To get all the publishers
        ///</summary>
        Task<List<Userdetail>> GetPublishers(string search = "");

        ///<summary>
        ///To get all the writers
        ///</summary>
        Task<List<Userdetail>> GetWriters(string search = "");
    }
}
