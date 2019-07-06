using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnline.Models
{
    [Serializable] 
    public class CartItem
    {
        
        public SanPhamKH sanpham { get; set; }
        public int soluong { get; set; }
    }
}