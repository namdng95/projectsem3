using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBookingProject.Models
{
    public class UserRegistor
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập!")]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu!")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "Mật khẩu ít nhất 6 ký tự!")]
        public string Password { get; set; }
        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu  không đúng!")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Quyền hạn")]
        [Required(ErrorMessage = "Yêu cầu chọn quyền hạn!")]
        public string Role { get; set; }
        [Display(Name = "Trạng thái")]
        [Required(ErrorMessage = "Yêu cầu chọn trạng thái!")]
        public Nullable<bool> Status { get; set; }
    }
}