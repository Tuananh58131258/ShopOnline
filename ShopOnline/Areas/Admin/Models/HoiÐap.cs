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
    
    public partial class HoiÐap
    {
        public string IdHoiDap { get; set; }
        public string MaTaiKhoan { get; set; }
        public string NoiDung { get; set; }
    
        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
