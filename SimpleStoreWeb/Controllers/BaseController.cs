using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleStoreWeb.Controllers
{
    public class BaseController : Controller
    {
        //public string CurrentUserLoginName
        //{
        //    get
        //    {
        //        string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
        //        HttpCookie authCookie = HttpContext.Request.Cookies[cookieName]; //Get the cookie by it's name
        //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
        //        return ticket.Name; //You have the UserName!
        //    }
        //}
    }
}