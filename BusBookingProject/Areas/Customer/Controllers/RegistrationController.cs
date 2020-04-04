using BotDetect.Web.Mvc;
using BusBookingProject.Common;
using BusBookingProject.DAO;
using BusBookingProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace BusBookingProject.Areas.Customer.Controllers
{
    public class RegistrationController : Controller
    {
        BusTicketManagementEntities db = new BusTicketManagementEntities();
        // GET: Customer/Registration
        [HttpGet]
        public ActionResult Register()
        {
            List<Loai_KhachHang> list = db.Loai_KhachHang.ToList();
            ViewBag.CategoryID = list;
            ViewBag.CustomerId = new SelectList(list, "MaLoaiKH", "TenLoaiKH");
            return View();
        }
        //public void SetViewBag(long? id = null)
        //{
        //    ViewBag.CategoryID = new SelectList(db.Loai_KhachHang.ToList(), "MaLoaiKH", "TenLoaiKH", id);
        //}
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                List<Loai_KhachHang> list = db.Loai_KhachHang.ToList();
                ViewBag.CategoryID = list;
                ViewBag.CustomerId = new SelectList(list, "MaLoaiKH", "TenLoaiKH");
                var userDAO = new UserDAO();
                var customerDAO = new CustomerDAO();

                string userInput = HttpContext.Request.Form["CaptchaCode"];

                if (userDAO.CheckUsername(model.Username))
                {
                    ModelState.AddModelError("", "Tên người dùng đã tồn tại!");
                }
                else if (customerDAO.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!");
                }
                else if (customerDAO.CheckPhoneNumber(model.Phone))
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại!");
                }
                else if (CheckCaptcha(userInput))
                {

                    var user = new User();
                    user.Username = model.Username;
                    user.Password = MD5Hash.GetMd5Hash(model.Password);
                    user.Role = model.Role;
                    user.CreatedDate = DateTime.Now;
                    user.ActiveCode = Guid.NewGuid();
                    user.Status = false;
                    var res1 = userDAO.Insert(user);

                    var customer = new Khach_Hang();
                    customer.TenKH = model.Name;
                    customer.NgaySinh = model.DateOfBirth;
                    customer.GioiTinh = model.Gender;
                    customer.Email = model.Email;
                    customer.DienThoai = model.Phone;
                    customer.DiaChi = model.Address;
                    customer.CMND = model.IDCard;
                    customer.CreatedDate = DateTime.Now;
                    customer.MaLoaiKH = model.CategoryCustomerId;

                    var res2 = customerDAO.Insert(customer);
                    if (res1 > 0 && res2 > 0)
                    {
                        SendVerificationLinkEmail(customer.Email, user.ActiveCode.ToString());
                        ViewBag.Success = "Đăng ký tài khoản thành công! Mã xác nhận tài khoản đã được gửi đến " + customer.Email;
                        model = new RegisterModel();
                    }
                    else
                    {
                        ViewBag.Error = "Đăng ký tài khoản thất bại!";
                        //ModelState.AddModelError(null, "Đăng ký tài khoản thất bại!");
                    }
                }
            }
            else
            {
                MvcCaptcha.ResetCaptcha("CaptchaCode");
            }
            return View(model);
        }
        [NonAction]
        public bool CheckCaptcha(string userInput)
        {
            // init mvcCaptcha instance with captchaId
            MvcCaptcha mvcCaptcha = new MvcCaptcha("CaptchaCode");
            if (mvcCaptcha.Validate(userInput))
            {
                // TODO: captcha validation succeeded; execute the protected action  

                // Reset the captcha if your app's workflow continues with the same view
                MvcCaptcha.ResetCaptcha("CaptchaCode");
                return true;
            }
            else
            {
                ModelState.AddModelError("", "Mã Captcha không đúng!");
            }
            return false;
        }
        [NonAction]
        public void SendVerificationLinkEmail(string Email, string ActivitionCode)
        {
            var verifyUrl = "/Admin/Registration/VerifyAccount/" + ActivitionCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("namdng95@gmail.com", "EasyBooking Vietnam!");
            var fromEmailPassword = "Mathematical12";
            var toEmail = new MailAddress(Email);

            string subject = "Your account is successfully created!";
            string body = "</br></br>We are excited to tell that your BusTicketBooking Account is" +
                " successfully created. Please click on the below link to verify your account" +
                " </br></br><a href = '" + link + "'>" + link + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            db.Configuration.ValidateOnSaveEnabled = false;
            var user = db.Users.Where(x => x.ActiveCode == new Guid(id)).FirstOrDefault();
            if (user != null)
            {
                user.Status = true;
                db.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request!";
            }
            ViewBag.Status = Status;
            return View("VerifyAccount");
        }
    }
}