using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using SchoolManagementAPI.Models;

namespace SchoolManagementAPI.Controllers
{
    public class LoginController : ApiController
    {
        //check Authenticated user for login
        public JObject CheckLogin(User user)
        {
            var jo = new JObject();
            if(user != null)
            {
                var userName = user.UserName;
                var password = user.Password;
            }
            else
            {

            }
            return jo;
        }
    }
}