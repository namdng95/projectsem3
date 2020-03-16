using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BusBookingProject.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { get; set; }
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
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Yêu cầu nhập họ và tên!")]
        public string Name { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Yêu cầu nhập ngày sinh!")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Yêu cầu chọn giới tính!")]
        public string Gender { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ!")]
        public string Address { get; set; }
        [Display(Name = "Số CMND")]
        [Required(ErrorMessage = "Yêu cầu nhập số chứng minh nhân dân!")]
        public string IDCard { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại!")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Yêu cầu nhập email!")]
        public string Email { get; set; }
        [Display(Name = "Loại khách hàng")]
        [Required(ErrorMessage = "Yêu cầu chọn loại khách hàng!")]
        public int CategoryCustomerId { get; set; }
        public bool Status { get; set; }
    }
}