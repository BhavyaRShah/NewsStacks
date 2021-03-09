using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsStacks.IService
{
    public interface ILoginService
    {
        ///<summary>
        ///To log in a user
        ///</summary>
        dynamic Login(LoginRequest request);
    }
}
