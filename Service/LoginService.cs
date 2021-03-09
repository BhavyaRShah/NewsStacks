using NewsStacks.IService;
using NewsStacks.Model;
using NewsStacks.RequestResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Isopoh.Cryptography.Argon2;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace NewsStacks.Service
{
    public class LoginService : ILoginService
    {
        readonly NewsStacksContext _dbContext;

        private IConfiguration _configuration;
       // private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(NewsStacksContext dbContext
            , IConfiguration configuraiton)
            //, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _configuration = configuraiton;
           // _httpContextAccessor = httpContextAccessor;
        }

        public dynamic Login(LoginRequest request)
        {
            //ISession session = _httpContextAccessor.HttpContext.Session;

            if (string.IsNullOrEmpty(request.username) || string.IsNullOrEmpty(request.password))
                throw new Exception("Username or Password cannot be empty");

            //search from db if any user with the given username exist or not
            Userdetail user = _dbContext.Userdetails.Where(x => x.Username == request.username).Include(x => x.Timezone).FirstOrDefault();

            if (user != null)
            {
                if (!Argon2.Verify(user.Userpassword, request.password))
                    throw new Exception("The password is incorrect");
                else
                {
                    AuthenticationResponse response = new AuthenticationResponse();
                    response.data = CreateToken(user: user);
                    response.success = true;

                    //byte[] userId = Encoding.ASCII.GetBytes(user.Userdetailid.ToString());

                    //session.Set("UserdetailId",userId);
                    
                    return response;
                    
                }

            }

            else
                throw new Exception("This user does not exist");


        }

        private string CreateToken(Userdetail user)
        {
            string role = string.Empty;

            if (user.Ispublisher == true)
                role = Common.USERROLES.PUBLISHER.ToString();
            else if (user.Iswriter == true)
                role = Common.USERROLES.WRITER.ToString();
            else
                role = Common.USERROLES.USER.ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Userdetailid.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
                //new Claim(ClaimTypes.)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_configuration.GetSection("JwtSecretKey").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
