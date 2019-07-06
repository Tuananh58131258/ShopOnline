using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Areas.Admin.Models;

namespace ShopOnline.Areas.Admin.Controllers
{
    [Authorize]
    public class SanPhamsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/SanPhams
        [HttpGet]
        public ActionResult Index(string LoaiSP,string TenSP,string MaNSX,string GiaMin,string GiaMax)
        {
            string min = GiaMin, max = GiaMax;
            if (GiaMin == "")
            {
                ViewBag.GiaMin = "";
                min = "0";
            }
            else
            {
                ViewBag.GiaMin = GiaMin;
                min = GiaMin;
            }
            if (max == "")
            {
                max = Int32.MaxValue.ToString();
                ViewBag.GiaMax = "";// Int32.MaxValue.ToString(); 
            }
            else
            {
                ViewBag.GiaMax = GiaMax;
                max = GiaMax;
            }
            ViewBag.SP = LoaiSP;
            ViewBag.TenSP = TenSP;
            ViewBag.GiaMin = GiaMin;
            ViewBag.GiaMax = GiaMax;
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX");
            //var sanPhams = db.SanPhams.SqlQuery("Select * from SanPham where MaSP like '" + LoaiSP + "%'");

            //var sanPhams = db.SanPhams.Include(s => s.NhaSanXuat);
            var sanPhams = db.SanPhams.SqlQuery("Execute TimKiemSP '"+LoaiSP+"',N'"+TenSP+"','"+MaNSX+"','"+GiaMin+"','"+GiaMax+"'");
            return View(sanPhams.ToList());
        }

        // GET: Admin/SanPhams/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX");
            return View();
        }

        // POST: Admin/SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaNSX,TenSP,HinhAnh,ManHinh,DonGia,HDH,CPU,GPU,Ram,Pin,Camera,BoNhoTrong,MoTa,KhuyenMai,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaNSX,TenSP,HinhAnh,ManHinh,DonGia,HDH,CPU,GPU,Ram,Pin,Camera,BoNhoTrong,MoTa,KhuyenMai,SoLuong")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats, "MaNSX", "TenNSX", sanPham.MaNSX);
            return View(sanPham);
        }

        // GET: Admin/SanPhams/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
