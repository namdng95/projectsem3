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
    
    public partial class Chuyen_Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chuyen_Xe()
        {
            this.Ve_Xe = new HashSet<Ve_Xe>();
        }
    
        public int MaChuyenXe { get; set; }
        public string TenChuyenXe { get; set; }
        public Nullable<int> MaTuyen { get; set; }
        public Nullable<System.DateTime> GioDi { get; set; }
        public Nullable<System.DateTime> GioDen { get; set; }
        public Nullable<int> MaNV { get; set; }
        public Nullable<int> ChoTrong { get; set; }
        public Nullable<int> MaTaiXe { get; set; }
        public string UrlImage { get; set; }
    
        public virtual Nhan_Vien Nhan_Vien { get; set; }
        public virtual Tai_Xe Tai_Xe { get; set; }
        public virtual Tuyen_Xe Tuyen_Xe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ve_Xe> Ve_Xe { get; set; }
    }
}
