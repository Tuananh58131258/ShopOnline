//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnline.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTHoaDonKH
    {
        public string MaHoaDon { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
    
        public virtual HoaDonKH HoaDon { get; set; }
        public virtual SanPhamKH SanPham { get; set; }
    }
}
