using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShopOnline.Controllers
{
    public class CartController : Controller
    {
        private DoAnWebEntities2 db = new DoAnWebEntities2();
        // GET: Cart
        private const string CartSession = "CartSession";
        public ActionResult GioHang()
        {

            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new { status = true });
        }
        public JsonResult Delete(string id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.sanpham.MaSP == id);
            Session[CartSession] = sessionCart;
            return Json(new { status = true });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.sanpham.MaSP == item.sanpham.MaSP);
                if(jsonItem != null)
                {
                    item.soluong = jsonItem.soluong;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new { status = true });

        }
        public ActionResult payment()
        {

            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(int quantity, string MaSP)
        {
            var sanpham = db.SanPham.Find(MaSP);
            var cart = Session[CartSession];
            if(cart != null )
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.sanpham.MaSP == MaSP))
                {
                    foreach (var item in list)
                    {
                        if (item.sanpham.MaSP == MaSP)
                        {
                            item.soluong += quantity;
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.sanpham = sanpham;
                    item.soluong = quantity;
                    list.Add(item);
                }
            }
            else
            {
                var item = new CartItem();
                item.sanpham = sanpham;
                item.soluong = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CartSession] = list;
            }
            return RedirectToAction("GioHang");
        }
    }
}