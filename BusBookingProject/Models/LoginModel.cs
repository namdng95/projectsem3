using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBookingProject.Models
{
    public class LoginModel
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string Password { get; set; }
        [Display(Name = "Remember Password?")]
        public bool RememberPassword { get; set; }
    }
}