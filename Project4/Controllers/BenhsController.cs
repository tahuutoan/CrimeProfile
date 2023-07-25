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
    public class BenhsController : Controller
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
        // GET: Benhs  
        public ActionResult Index(string txtTenPhamNhan, int? i)
        {
            if (string.IsNullOrEmpty(txtTenPhamNhan)) txtTenPhamNhan = "";
            var allBenh = db.Benh.Include(a => a.PhamNhan)
                .Where(a => a.PhamNhan.TenPhamNhan.Contains(txtTenPhamNhan) &&
                 DbFunctions.DiffDays(a.PhamNhan.NgayVaoTrai, DateTime.Now) < a.PhamNhan.SoNgayGiamGiu)
                .ToList();
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
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucViaUser = User.Identity.GetQuanNgucId();
                var idQuanNgucToGuid = Guid.Parse(quanNgucViaUser);
                var nhungPhongDangDuocBanGiaoTamThoi = db.BanGiaoPhamNhan
                    .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid
                  && DbFunctions.DiffDays(DateTime.Now, w.NgayNhan) <= w.SoNgayBanGiao).Select(p => p.PhongID);

                if (nhungPhongDangDuocBanGiaoTamThoi != null)
                {
                    // nghĩa là nếu phòng ban giao != null tức là có đang bàn giao cho thằng quản ngục này
                    // thì nó select ra những thẳng phạm nhân id ở bên trong phòng mà thằng này được bàn giao cho
                    // cộng với phòng nó đang trông
                    var phamNhan = db.PhamNhan
                        .Where(w => nhungPhongDangDuocBanGiaoTamThoi.Contains(w.PhongGiamID)).Select(p => p.ID);
                    if (phamNhan != null)
                    {
                        var benh = db.Benh.Where(w => phamNhan.Contains(w.PhamNhanID)).ToList();
                        if (benh != null)
                        {
                            allBenh.AddRange(benh);
                        }
                    }
                }

                var nhungPhongBanGiaoSauKhiXoa = db.BanGiaoCongViecCuaQuanNgucNghi
                    .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid)
                    .Select(p => p.PhongID);

                if (nhungPhongBanGiaoSauKhiXoa != null)
                {
                    var phamNhan = db.PhamNhan
                        .Where(w => nhungPhongBanGiaoSauKhiXoa.Contains(w.PhongGiamID)).Select(p => p.ID);
                    if (phamNhan != null)
                    {
                        var benh = db.Benh
                            .Where(w => phamNhan.Contains(w.PhamNhanID)).ToList();
                        if (benh != null)
                        {
                            allBenh.AddRange(benh);
                        } 
                    }
                }
            }
            else
            { 
                var phongGiam = db.PhongGiam.ToList();
                ViewBag.Phong = phongGiam; 

                var bieuHienKem = db.LaoDongCongIch.Where(l => l.BieuHien == 5).Select(l => l.PhamNhanID);
                var allPhamNhan = db.PhamNhan.Where(p => !bieuHienKem.Contains(p.ID) &&
                 DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu).ToList();

                ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);
            }
            int pageSize = 5;
            int pageNumber = (i ?? 1);
            return View(allBenh.Distinct().OrderBy(t => t.PhamNhanID).ToPagedList(pageNumber, pageSize));
        }

        // GET: Benhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benh benh = db.Benh.Find(id);
            if (benh == null)
            {
                return HttpNotFound();
            }
            return View(benh);
        }

        // GET: Benhs/Create
        public ActionResult Create()
        {
            var allPhamNhan = db.PhamNhan;
            //var allPhamNhan = db.PhamNhan.Where(p => p.IDKhu == 5).ToList();
            var allBenh = db.Benh;
            if (User.IsInRole("QuanNgucTruong"))
            {
                foreach (var item in allPhamNhan)
                {
                    var biBenh = db.Benh.Where(w => w.PhamNhanID == item.ID);
                    if (biBenh != null && biBenh.Where(w => DbFunctions.DiffDays(w.NgayBatDauChuaTri, DateTime.Now) < w.NgayChuaTri).Any())
                    { 
                        allPhamNhan.Remove(item);
                    }
                }
                var phongGiamDauTien = db.PhongGiam.FirstOrDefault().ID; 

                ViewBag.Phong = db.PhongGiam.Distinct().ToList(); 
                ViewBag.PhamNhanID = new SelectList(allPhamNhan
                    .Where(a => DbFunctions.DiffDays(a.NgayVaoTrai, DateTime.Now) < a.SoNgayGiamGiu && 
                           a.PhongGiamID == phongGiamDauTien), "ID", "TenPhamNhan", null); 
            }
            else
            {
                var allPhamNhanBenh = db.Benh.Select(b => b.PhamNhanID);
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                var phongPhamNhan = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).Select(s => s.ID);
                var phamNhan = db.PhamNhan.Where(w => phongPhamNhan.Contains(w.PhongGiamID)).ToList();

                var phongBanGiaoTamThoi = phongTamthoi
                    .BanGiaoTamThoi(Guid.Parse(User.Identity.GetQuanNgucId()))
                    .Select(p => p.ID);
                if (phongBanGiaoTamThoi != null)
                {
                    var phamNhanTamThoi = db.PhamNhan
                        .Where(p => phongBanGiaoTamThoi.Contains(p.PhongGiamID))
                        .ToList();
                    if (phamNhanTamThoi != null)
                    {
                        phamNhan.AddRange(phamNhanTamThoi);
                    }
                }
                var phongBanGiaoVoThoiHan = phongTamthoi.BanGiaoVoThoiHan(Guid.Parse(User.Identity.GetQuanNgucId()))
                    .Select(p => p.ID);
                if (phongBanGiaoVoThoiHan != null)
                {
                    var phamNhanBanGiaoVinhVien = db.PhamNhan
                        .Where(p => phongBanGiaoVoThoiHan.Contains(p.PhongGiamID))
                        .ToList();
                    if (phamNhanBanGiaoVinhVien != null)
                    {
                        phamNhan.AddRange(phamNhanBanGiaoVinhVien);
                    }
                }

                List<PhamNhan> phamNhanRemove = new List<PhamNhan>();
                foreach (var item in phamNhan)
                {
                    var biBenh = db.Benh.Where(w => w.PhamNhanID == item.ID);
                    if (biBenh != null && biBenh.Where(w => DbFunctions.DiffDays(w.NgayBatDauChuaTri, DateTime.Now) < w.NgayChuaTri).Any())
                    { 
                        phamNhanRemove.Add(item);
                    }
                }
                foreach (var item in phamNhanRemove)
                {
                    phamNhan.Remove(item);
                }
                var phongGiam = db.PhongGiam.Where(p => p.KhuID == quanNgucHienTai.KhuID).ToList();
                Guid idQuannguc = Guid.Parse(User.Identity.GetQuanNgucId());
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

                ViewBag.Phong = phongGiam.Distinct();  
                ViewBag.PhamNhanID = new SelectList(
                    phamNhan.Where(p => (DateTime.Now - p.NgayVaoTrai).Value.TotalDays < p.SoNgayGiamGiu &&
                    p.PhongGiamID == phongGiamDauTien), "ID", "TenPhamNhan", null);  
            }
            if (ViewBag.PhamNhanID == null)
            {
                return RedirectToAction("Index");
            }
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

        // POST: Benhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhamNhanID,NgayChuaTri")] Benh benh)
        {
            if (ModelState.IsValid)
            {
                benh.NgayBatDauChuaTri = DateTime.Now;
                db.Benh.Add(benh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(benh);
        }

        // GET: Benhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benh benh = db.Benh.Find(id);
            if (benh == null) 
            {
                return HttpNotFound();
            }
            ViewBag.PhamNhanID = new SelectList(db.PhamNhan, "ID", "TenPhamNhan", benh.PhamNhanID);
            return View(benh);
        }

        // POST: Benhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhamNhanID,NgayChuaTri")] Benh benh)
        {
            if (ModelState.IsValid)
            {
                benh.NgayBatDauChuaTri = db.Benh.Where(b => b.ID == benh.ID).Select(b => b.NgayBatDauChuaTri).FirstOrDefault();
                db.Entry(benh).State = EntityState.Modified; 
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(benh);
        }

        // GET: Benhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Benh benh = db.Benh.Find(id);
            if (benh == null)
            {
                return HttpNotFound();
            }
            return View(benh);
        }

        // POST: Benhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Benh benh = db.Benh.Find(id);
            db.Benh.Remove(benh);
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
