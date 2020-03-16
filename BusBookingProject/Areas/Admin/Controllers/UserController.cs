using BusBookingProject.Areas.Admin.Models;
using BusBookingProject.DAO;
using BusBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusBookingProject.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        BusTicketManagementEntities db = new BusTicketManagementEntities();
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var userDAO = new UserDAO();
            var model = userDAO.ListAllPaging(page, pageSize);
            //List<User> listUser = db.Users.ToList();
            //ViewBag.ListUser = listUser;
            return View(model);
        }
        
        public JsonResult DeleteUser(int Id)
        {
            bool result = false;
            var userDAO = new UserDAO();
            if(userDAO.Delete(Id))
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUser(UserRegistor model)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();

                if (userDAO.CheckUsername(model.Username))
                {
                    ModelState.AddModelError("", "Tên người dùng đã tồn tại!");
                }
                else
                {
                    var user = new User();
                    user.Username = model.Username;
                    user.Password = Common.MD5Hash.GetMd5Hash(model.Password);
                    user.CreatedDate = DateTime.Now;
                    user.ActiveCode = Guid.NewGuid();
                    user.Role = model.Role;
                    user.Status = model.Status;

                    var result = userDAO.Insert(user);
                    if(result > 1)
                    {
                        ViewBag.Success = "Đăng ký người dùng thành công!";
                        model = new UserRegistor();
                    }
                    else
                    {
                        ViewBag.Error = "Đăng ký tài khoản thất bại!";
                    }
                }
            }
            return View();

        }
    }
}