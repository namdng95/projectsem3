//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusBookingProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Khach_Hang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Khach_Hang()
        {
            this.Chitietdatves = new HashSet<Chitietdatve>();
        }
    
        public int MaKH { get; set; }
        public string TenKH { get; set; }
        public Nullable<int> MaLoaiKH { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string CMND { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> IdLogin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chitietdatve> Chitietdatves { get; set; }
        public virtual Loai_KhachHang Loai_KhachHang { get; set; }
    }
}
