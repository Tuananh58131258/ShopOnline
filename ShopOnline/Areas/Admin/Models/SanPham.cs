//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnline.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.CTHoaDons = new HashSet<CTHoaDon>();
            this.MauSacs = new HashSet<MauSac>();
        }
    
        public string MaSP { get; set; }
        public string MaNSX { get; set; }
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        public string ManHinh { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public string HDH { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string Ram { get; set; }
        public string Pin { get; set; }
        public string Camera { get; set; }
        public string BoNhoTrong { get; set; }
        public string MoTa { get; set; }
        public Nullable<double> KhuyenMai { get; set; }
        public Nullable<int> SoLuong { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTHoaDon> CTHoaDons { get; set; }
        public virtual NhaSanXuat NhaSanXuat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MauSac> MauSacs { get; set; }
    }
}
