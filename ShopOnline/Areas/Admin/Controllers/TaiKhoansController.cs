using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopOnline.Areas.Admin.Models;
using System.Web.Security;

namespace ShopOnline.Areas.Admin.Controllers
{
    public class TaiKhoansController : Controller
    {
        private DoAnWebEntities db = new DoAnWebEntities();

        // GET: Admin/TaiKhoans
        public ActionResult Index()
        {
            var taiKhoans = db.TaiKhoans.Include(t => t.LoaiTaiKhoan);
            return View(taiKhoans.ToList());
        }

        // GET: Admin/TaiKhoans/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiTK = new SelectList(db.LoaiTaiKhoans, "MaLoaiTK", "TenLoaiTk");
            return View();
        }

        // POST: Admin/TaiKhoans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTaiKhoan,MaLoaiTK,TenTaiKhoan,MatKhau,HoTen,DiaChi,SDT,GioiTinh")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiTK = new SelectList(db.LoaiTaiKhoans, "MaLoaiTK", "TenLoaiTk", taiKhoan.MaLoaiTK);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiTK = new SelectList(db.LoaiTaiKhoans, "MaLoaiTK", "TenLoaiTk", taiKhoan.MaLoaiTK);
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTaiKhoan,MaLoaiTK,TenTaiKhoan,MatKhau,HoTen,DiaChi,SDT,GioiTinh")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiTK = new SelectList(db.LoaiTaiKhoans, "MaLoaiTK", "TenLoaiTk", taiKhoan.MaLoaiTK);
            return View(taiKhoan);
        }

        // GET: Admin/TaiKhoans/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: Admin/TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TaiKhoan taiKhoan = db.TaiKhoans.Find(id);
            db.TaiKhoans.Remove(taiKhoan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        public bool Validate(string username, string password)
        {
            var user = db.TaiKhoans.SqlQuery("execute USP_Login '"+username+"','"+password+"'").ToList();
            if (user.Count() != 0)
                return true;
            return false;
        }
        [HttpPost]
        public ActionResult LogOff()
        {
            //WebSecurity.Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("ThongKe", "HoaDons", new { Area = "Admin" });
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("ThongKe", "HoaDons",new { Area ="Admin"});
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(TaiKhoan user, string returnUrl)
        {
            if (Validate(user.TenTaiKhoan, user.MatKhau))
            {
                FormsAuthentication.SetAuthCookie(user.TenTaiKhoan,false);
                return RedirectToLocal(returnUrl);
            }
            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(user);
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
