using BusBookingProject.Common;
using BusBookingProject.DAO;
using BusBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingProject.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        BusTicketManagementEntities db = new BusTicketManagementEntities();
        // GET: Admin/Account
        public ActionResult Login()
        {
            //Check Cookie is null or not null.
            LoginModel account = checkCookie();
            if (account == null)
            {
                return View();
            }
            return View("Login", account);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                var userDAO = new UserDAO();
                var result = userDAO.Login(model.Username, model.Password);
                if (result)
                {
                    var user = userDAO.GetUserByName(model.Username);
                    var userSession = new UserLogin();
                    userSession.UserName = user.Username;
                    userSession.UserID = user.Id;

                    Session.Add(CommonConstants.USER_SESSION, userSession); //Add Session =>([key],[value])


                    //Add Coockie User
                    if (model.RememberPassword)
                    {
                        HttpCookie ckUsername = new HttpCookie("username");
                        ckUsername.Expires = DateTime.Now.AddDays(1);
                        ckUsername.Value = model.Username;
                        Response.Cookies.Add(ckUsername);

                        HttpCookie ckPassword = new HttpCookie("password");
                        ckPassword.Expires = DateTime.Now.AddDays(1);
                        ckPassword.Value = model.Password;
                        Response.Cookies.Add(ckPassword);
                    }
                    return RedirectToAction("Index", "DashBoard");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản không đúng!");
                }
            }
            return View("Login");
        }

        public LoginModel checkCookie()
        {
            LoginModel account = null;
            string username = string.Empty, password = string.Empty;
            if (Request.Cookies["username"] != null && Request.Cookies["password"] != null)
            {
                username = Request.Cookies["username"].Value;
                password = Request.Cookies["password"].Value;
            }
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                account = new LoginModel { Username = username, Password = password, RememberPassword = true };
            }
            return account;
        }

        public ActionResult Logout()
        {
            //Remove Session 
            Session.Remove(CommonConstants.USER_SESSION);
            //Remove Coockie
            if (Response.Cookies["username"] != null && Response.Cookies["password"] != null)
            {
                HttpCookie ckUsername = new HttpCookie("username");
                ckUsername.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ckUsername);

                HttpCookie ckPassword = new HttpCookie("password");
                ckPassword.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(ckPassword);
            }
            return View("Login");
        }
    }
}