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
    
    public partial class Tuyen_Xe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tuyen_Xe()
        {
            this.Chuyen_Xe = new HashSet<Chuyen_Xe>();
        }
    
        public int MaTuyen { get; set; }
        public string TenTuyen { get; set; }
        public string DiemDi { get; set; }
        public string DiemDen { get; set; }
        public int BangGia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chuyen_Xe> Chuyen_Xe { get; set; }
    }
}