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
    
    public partial class Nhan_Vien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nhan_Vien()
        {
            this.Chuyen_Xe = new HashSet<Chuyen_Xe>();
            this.Ve_Xe = new HashSet<Ve_Xe>();
        }
    
        public int MaNV { get; set; }
        public string TenNV { get; set; }
        public Nullable<int> MaLoaiNV { get; set; }
        public System.DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string CMND { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chuyen_Xe> Chuyen_Xe { get; set; }
        public virtual Loai_NV Loai_NV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve_Xe> Ve_Xe { get; set; }
    }
}