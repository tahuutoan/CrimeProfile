using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PagedList;
using Project4.Models;
using Project4.Services;

namespace Project4.Controllers
{
    public class LaoDongCongIchesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private NhungPhongDuocBanGiaoTamThoi phongTamthoi = new NhungPhongDuocBanGiaoTamThoi();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: LaoDongCongIches 
        public ActionResult Index(string tenPhamNhan, int? i)
        {
            if (User.Identity.GetQuanNgucId() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            var qn = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
            if (qn == null)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                return RedirectToAction("DangNhap", "TaiKhoan");
            }


            if (string.IsNullOrEmpty(tenPhamNhan)) tenPhamNhan = "";
            var laodongCaitaoList = db.LaoDongCongIch.Include(p => p.PhamNhan).Include(p => p.QuanNguc)
                .Where(p => p.PhamNhan.TenPhamNhan.Contains(tenPhamNhan) &&
                 DbFunctions.DiffDays(p.PhamNhan.NgayVaoTrai, DateTime.Now) < p.PhamNhan.SoNgayGiamGiu)
                .ToList();
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                laodongCaitaoList = laodongCaitaoList.Where(q => q.QuanNguc.KhuID == quanNgucHienTai.KhuID).ToList();
            }

            var model = laodongCaitaoList.ToList();
            foreach (var item in model)
            {
                var benhNhanBiBenh = db.Benh.Where(w => w.PhamNhanID == item.PhamNhanID && DbFunctions.DiffDays(DateTime.Now, w.NgayBatDauChuaTri) <= w.NgayChuaTri);
                if (benhNhanBiBenh.Any())
                {
                    item.DangBiBenh = true;
                }
                else
                {
                    item.DangBiBenh = false;
                }
            }
            int pageSize = 5;
            int pageNumber = (i ?? 1);
            ViewBag.khuVucLamViec = Common.CommonConstant.khuVucLamViec;
            ViewBag.bieuHien = Common.CommonConstant.bieuHien;
            return View(model.Distinct().OrderBy(l => l.PhamNhanID).ToPagedList(pageNumber, pageSize));

        }

        // GET: LaoDongCongIches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaoDongCongIch laoDongCongIch = db.LaoDongCongIch.Find(id);
            if (laoDongCongIch == null)
            {
                return HttpNotFound();
            }
            return View(laoDongCongIch);
        }

        // GET: LaoDongCongIches/Create
        public ActionResult Create()
        {
            ViewBag.KhuVucLamViec = new SelectList(Common.CommonConstant.khuVucLamViec, "Key", "Value", null);
            ViewBag.BieuHien = new SelectList(Common.CommonConstant.bieuHien, "Key", "Value", null);
            var allPhamNhan = db.PhamNhan
                .Where(a => DbFunctions.DiffDays(a.NgayVaoTrai, DateTime.Now) < a.SoNgayGiamGiu).ToList();

            if (!User.IsInRole("QuanNgucTruong"))
            { 
                Guid idQuannguc = Guid.Parse(User.Identity.GetQuanNgucId());
                var khuQuanNgucCoi = db.QuanNguc.Where(q => q.ID == idQuannguc)
                    .Select(q => q.KhuID)
                    .FirstOrDefault();

                int _khu = Convert.ToInt32(khuQuanNgucCoi);
                var phongGiam = db.PhongGiam.Where(p => p.KhuID == _khu).ToList();
                var phongTamThoi = phongTamthoi.BanGiaoTamThoi(idQuannguc);
                if (phongTamThoi != null)
                {
                    phongGiam.AddRange(phongTamThoi);
                }
                var phongGiamBanGiaoVinhVien = phongTamthoi.BanGiaoVoThoiHan(idQuannguc);
                if (phongGiamBanGiaoVinhVien != null)
                {
                    phongGiam.AddRange(phongGiamBanGiaoVinhVien);
                }
                var phongGiamDauTien = phongGiam.FirstOrDefault().ID;
                var bieuHienKem = db.LaoDongCongIch.Where(l => l.BieuHien == 5).Select(l => l.PhamNhanID);

                ViewBag.Phong = phongGiam.Distinct().ToList();
                ViewBag.PhamNhanID = new SelectList(
                     allPhamNhan.Where(p => (DateTime.Now - p.NgayVaoTrai).Value.TotalDays < p.SoNgayGiamGiu &&
                                       p.PhongGiamID == phongGiamDauTien), "ID", "TenPhamNhan", null);  
            }
            else
            {
                var phongGiamDauTien = db.PhongGiam.FirstOrDefault().ID;
                ViewBag.Phong = db.PhongGiam.ToList();
                ViewBag.PhamNhanID = new SelectList(
                    allPhamNhan.Where(p => p.PhongGiamID == phongGiamDauTien), "ID", "TenPhamNhan", null);
            }
            //if (User.IsInRole("QuanNgucTruong"))
            //{
            //} 
            return View();
        }

        public ActionResult LoadPhamNhan(string phongID)
        {
            int _phong = int.Parse(phongID);
            var allPhamNhan = db.PhamNhan
                .Where(p => p.PhongGiamID == _phong && DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu)
                .ToList();

            var allIdPhamNhan = allPhamNhan.Select(p => p.ID);
            var phamNhanBiBenh = db.Benh.Where(b => allIdPhamNhan.Contains(b.PhamNhanID) && DbFunctions.DiffDays(b.NgayBatDauChuaTri, DateTime.Now) < b.NgayChuaTri).Select(b => b.PhamNhanID).ToList();
            allPhamNhan = allPhamNhan.Where(b => !phamNhanBiBenh.Contains(b.ID)).ToList();
            ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);

            return PartialView("_PhamNhanPartial");
        }


        // POST: LaoDongCongIches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhamNhanID,KhuVucLamViec,BieuHien")] LaoDongCongIch laoDongCongIch)
        {
            if (ModelState.IsValid)
            {
                var khucuaphamNhan = db.PhamNhan.Find(laoDongCongIch.PhamNhanID).IDKhu;
                var khuID = db.Khu.FirstOrDefault(f => f.ID == khucuaphamNhan).ID;
                // get id quan nguc tai khoan hien tai
                laoDongCongIch.QuanNgucID = Guid.Parse(User.Identity.GetQuanNgucId());
                //laoDongCongIch.QuanNgucID = db.QuanNguc.FirstOrDefault(f => f.KhuID == khuID).ID;
                db.LaoDongCongIch.Add(laoDongCongIch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(laoDongCongIch);
        }

        // GET: LaoDongCongIches/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.KhuVucLamViec = new SelectList(Common.CommonConstant.khuVucLamViec, "Key", "Value", null);
            ViewBag.BieuHien = new SelectList(Common.CommonConstant.bieuHien, "Key", "Value", null);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaoDongCongIch laoDongCongIch = db.LaoDongCongIch.Find(id);
            if (laoDongCongIch == null)
            {
                return HttpNotFound();
            }
            if (db.Benh.Where(w => w.PhamNhanID == laoDongCongIch.PhamNhanID && DbFunctions.DiffDays(DateTime.Now, w.NgayBatDauChuaTri) <= w.NgayChuaTri).Any())
            {
                return RedirectToAction("Index", "LaoDongCongIches");
            }
            return View(laoDongCongIch);
        }

        // POST: LaoDongCongIches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhamNhanID,KhuVucLamViec,BieuHien")] LaoDongCongIch laoDongCongIch)
        {
            if (ModelState.IsValid)
            {
                var khucuaphamNhan = db.PhamNhan.Find(laoDongCongIch.PhamNhanID).IDKhu;
                var khuID = db.Khu.FirstOrDefault(f => f.ID == khucuaphamNhan).ID;
                laoDongCongIch.QuanNgucID = db.QuanNguc.FirstOrDefault(f => f.KhuID == khuID).ID;
                db.Entry(laoDongCongIch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(laoDongCongIch);
        }

        // GET: LaoDongCongIches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LaoDongCongIch laoDongCongIch = db.LaoDongCongIch.Find(id);
            if (laoDongCongIch == null)
            {
                return HttpNotFound();
            }
            return View(laoDongCongIch);
        }

        // POST: LaoDongCongIches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LaoDongCongIch laoDongCongIch = db.LaoDongCongIch.Find(id);
            db.LaoDongCongIch.Remove(laoDongCongIch);
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
