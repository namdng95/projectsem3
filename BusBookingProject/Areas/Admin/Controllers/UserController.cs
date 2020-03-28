using BusBookingProject.Areas.Admin.Models;
using BusBookingProject.Common;
using BusBookingProject.DAO;
using BusBookingProject.Models;
using System;
using PagedList;
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
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            var userDAO = new UserDAO();
            var model = userDAO.ListAllPaging(page, pageSize);
            return View(model);
        }
        public JsonResult SearchingData(string SearchBy, string SearchValue, int page = 1, int pageSize = 3)
        {
            IEnumerable<User> listUser = new List<User>();
            if (SearchBy == "Id")
            {
                try
                {
                    if(!String.IsNullOrEmpty(SearchValue))
                    {
                        int Id = Convert.ToInt32(SearchValue);
                        listUser = db.Users.Where(x => x.Id == Id).OrderBy(x=>x.Id).ToPagedList(page, pageSize);
                    }
                    else
                    {
                        listUser = db.Users.ToList();
                    }                 
                }
                catch(FormatException)
                {
                    Console.WriteLine("{0} is not a ID", SearchValue);
                }
                return Json(listUser, JsonRequestBehavior.AllowGet);
            }
            else
            {
                listUser = db.Users.Where(x => x.Username.Contains(SearchValue) || SearchValue == null).OrderBy(x=>x.Id).ToPagedList(page, pageSize);

                return Json(listUser, JsonRequestBehavior.AllowGet);
            }
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
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(UserRegistor model)
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
                    user.Password = MD5Hash.GetMd5Hash(model.Password);
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

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = new UserRegistor();
            var user = new UserDAO().GetUserById(id);
            model.ID = user.Id;
            model.Username = user.Username;
            model.Password = user.Password;
            model.Role = user.Role;
            model.Status = user.Status;
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(UserRegistor model)
        {
            if (ModelState.IsValid)
            {
                var userDAO = new UserDAO();
                var user = new User();
                user.Id = model.ID;
                user.Username = model.Username;
                user.Password = model.Password;
                user.Role = model.Role;
                user.Status = model.Status;

                if (!String.IsNullOrEmpty(user.Password) && !String.IsNullOrEmpty(user.Username))
                {
                    user.Password = MD5Hash.GetMd5Hash(user.Password);
                }
                var result = userDAO.Update(user);
                if (result)
                {
                    ViewBag.Success = "Thay đổi thông tin người dùng thành công!";
                    model = new UserRegistor();
                }
                else
                {
                    ViewBag.Error = "Thay đổi thông tin người dùng thất bại!";
                }
            }
            return View();
        }
    }
}