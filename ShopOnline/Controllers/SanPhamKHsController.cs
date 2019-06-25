using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class SanPhamKHsController : Controller
    {
        private DoAnWebEntities2 db = new DoAnWebEntities2();

        // GET: SanPhamKHs
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.NhaSanXuat);
            return View(sanPham.ToList());
        }

        // GET: SanPhamKHs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKH sanPhamKH = db.SanPham.Find(id);
            if (sanPhamKH == null)
            {
                return HttpNotFound();
            }
            return View(sanPhamKH);
        }

        // GET: SanPhamKHs/Create
        public ActionResult Create()
        {
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX");
            return View();
        }

        // POST: SanPhamKHs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaNSX,TenSP,HinhAnh,ManHinh,DonGia,HDH,CPU,GPU,Ram,Pin,Camera,BoNhoTrong,MoTa,KhuyenMai,SoLuong")] SanPhamKH sanPhamKH)
        {
            if (ModelState.IsValid)
            {
                db.SanPham.Add(sanPhamKH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX", sanPhamKH.MaNSX);
            return View(sanPhamKH);
        }

        // GET: SanPhamKHs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKH sanPhamKH = db.SanPham.Find(id);
            if (sanPhamKH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX", sanPhamKH.MaNSX);
            return View(sanPhamKH);
        }

        // POST: SanPhamKHs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaNSX,TenSP,HinhAnh,ManHinh,DonGia,HDH,CPU,GPU,Ram,Pin,Camera,BoNhoTrong,MoTa,KhuyenMai,SoLuong")] SanPhamKH sanPhamKH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPhamKH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNSX = new SelectList(db.NhaSanXuat, "MaNSX", "TenNSX", sanPhamKH.MaNSX);
            return View(sanPhamKH);
        }

        // GET: SanPhamKHs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPhamKH sanPhamKH = db.SanPham.Find(id);
            if (sanPhamKH == null)
            {
                return HttpNotFound();
            }
            return View(sanPhamKH);
        }

        // POST: SanPhamKHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SanPhamKH sanPhamKH = db.SanPham.Find(id);
            db.SanPham.Remove(sanPhamKH);
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
