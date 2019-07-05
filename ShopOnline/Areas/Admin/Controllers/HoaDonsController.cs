using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Areas.Admin.Models;
using System.Collections;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class HoaDonsController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            var hoaDons = db.HoaDons.Include(h => h.TaiKhoan);
            return View(hoaDons.ToList());
        }

        // GET: Admin/HoaDons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaLoaiTK");
            return View();
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHoaDon,MaTaiKhoan,DiaChiGiaoHang,NgayLap,TinhTrang,TongTien")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaLoaiTK", hoaDon.MaTaiKhoan);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaLoaiTK", hoaDon.MaTaiKhoan);
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHoaDon,MaTaiKhoan,DiaChiGiaoHang,NgayLap,TinhTrang,TongTien")] HoaDon hoaDon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTaiKhoan = new SelectList(db.TaiKhoans, "MaTaiKhoan", "MaLoaiTK", hoaDon.MaTaiKhoan);
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HoaDon hoaDon = db.HoaDons.Find(id);
            db.HoaDons.Remove(hoaDon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ThongKe(string start,string end)
        {
            ViewBag.start = start;
            ViewBag.end = end;
            DateTime daystart = Convert.ToDateTime(start);
            DateTime dayend = Convert.ToDateTime(end);
            var dayl = db.HoaDons.Where(x => x.NgayLap >= daystart.Date && x.NgayLap <= dayend.Date)
                .Select(x => x.NgayLap).Distinct().ToList();
            ArrayList day = new ArrayList();
            List<string> hoaDon = new List<string>();
            List<int> dulieu = new List<int>();
            int tien = 0, tongtien = 0;
            foreach (DateTime item in dayl)
            {
                day.Add(item.ToString("dd/MM/yyyy"));
                foreach (var hd in db.HoaDons)
                {
                    if (hd.NgayLap.Value.Date == item.Date)
                    {
                        foreach (var ct in db.CTHoaDons)
                        {
                            if (ct.MaHoaDon == hd.MaHoaDon)
                            {
                                tien += ct.SoLuong.Value * ct.DonGia.Value;
                            }
                        }
                        tongtien += tien;
                        tien = 0;
                    }
                }
                dulieu.Add(tongtien);
                tongtien = 0;
            }
            ViewBag.Label = day;
            ViewBag.ChartData = dulieu;
            return View();
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
