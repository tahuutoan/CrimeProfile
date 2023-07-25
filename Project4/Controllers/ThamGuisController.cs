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
    public class ThamGuisController : Controller
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
        // GET: ThamGuis 
        public ActionResult Index(string tenPhamNhan, int? i)
        {
            if (string.IsNullOrEmpty(tenPhamNhan)) tenPhamNhan = "";
            var thamGuis = db.ThamGui
                .Include(t => t.PhamNhan).Include(t => t.QuanNguc)
                .Where(t => t.PhamNhan.TenPhamNhan.Contains(tenPhamNhan) &&
                 DbFunctions.DiffDays(t.PhamNhan.NgayVaoTrai, DateTime.Now) < t.PhamNhan.SoNgayGiamGiu)
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

            /////
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucViaUser = User.Identity.GetQuanNgucId();
                var idQuanNgucToGuid = Guid.Parse(quanNgucViaUser);
                var nhungPhongDangDuocBanGiaoTamThoi = db.BanGiaoPhamNhan
                    .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid &&
                     DbFunctions.DiffDays(w.NgayNhan, DateTime.Now) <= w.SoNgayBanGiao)
                    .Select(p => p.PhongID);

                if (nhungPhongDangDuocBanGiaoTamThoi != null)
                {
                    var phamNhan = db.PhamNhan
                        .Where(w => nhungPhongDangDuocBanGiaoTamThoi.Contains(w.PhongGiamID) &&
                         DbFunctions.DiffDays(w.NgayVaoTrai, DateTime.Now) < w.SoNgayGiamGiu)
                        .Select(p => p.ID);
                    if (phamNhan != null)
                    {
                        var thamGui = db.ThamGui
                            .Where(w => phamNhan.Contains(w.PhamNhanID)).ToList();
                        if (thamGui != null)
                        {
                            thamGuis.AddRange(thamGui);
                        }
                    }
                }

                var nhungPhongBanGiaoSauKhiXoa = db.BanGiaoCongViecCuaQuanNgucNghi
                    .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid)
                    .Select(p => p.PhongID);

                if (nhungPhongBanGiaoSauKhiXoa != null)
                {
                    var phamNhan = db.PhamNhan
                        .Where(w => nhungPhongBanGiaoSauKhiXoa.Contains(w.PhongGiamID)
                        && DbFunctions.DiffDays(w.NgayVaoTrai, DateTime.Now) < w.SoNgayGiamGiu).Select(p => p.ID);
                    if (phamNhan != null)
                    {
                        var thamGui = db.ThamGui
                            .Where(w => phamNhan.Contains(w.PhamNhanID)).ToList();
                        if (thamGui != null)
                        {
                            thamGuis.AddRange(thamGui);
                        }
                    }
                }
            }

            /////

            ViewBag.Khu = db.Khu.ToList();
            ViewBag.Phong = db.PhongGiam.ToList();
            int pageSize = 5;
            int pageNumber = (i ?? 1);
            return View(thamGuis.Distinct().OrderBy(t => t.PhamNhanID).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ThanhTimKiem(string khuID)
        {
            ViewBag.Khu = db.Khu.ToList();
            ViewBag.SelectedKhu = khuID;
            int idKhu = int.Parse(khuID);
            ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == idKhu).ToList();

            return PartialView("_ThamGuisSearchBar");
        }

        public ActionResult TimKiem(string txtTenHoacMa, string khuID, string phongID)
        {
            IQueryable<PhamNhan> listPhamNhan = db.PhamNhan;
            List<ThamGui> listThamGui = db.ThamGui.ToList();
            if (txtTenHoacMa.Length == 5)
            {
                //listPhamNhan = db.PhamNhan.Where(w => w. == txtTenOrMa);
            }
            else
            {
                listPhamNhan = listPhamNhan.Where(w => w.TenPhamNhan.Contains(txtTenHoacMa));
            }
            var IDkhu = int.Parse(khuID);
            listPhamNhan = listPhamNhan.Where(w => w.IDKhu == IDkhu);
            if (phongID != string.Empty || phongID != "null")
            {
                var IDphong = int.Parse(phongID);
                listPhamNhan = listPhamNhan.Where(w => w.PhongGiamID == IDphong);
            }
            foreach (var item in listPhamNhan)
            {
                listThamGui.Add(listThamGui.FirstOrDefault(f => f.PhamNhanID == item.ID));
            }
            return PartialView("_ThamGuisDataTable", listThamGui);
        }
        // GET: ThamGuis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThamGui thamGui = db.ThamGui.Find(id);
            if (thamGui == null)
            {
                return HttpNotFound();
            }
            return View(thamGui);
        }

        // GET: ThamGuis/Create
        public ActionResult Create()
        {
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
                var allPhamNhan = db.PhamNhan
                    .Where(p => p.IDKhu == _khu && DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu
                    && p.PhongGiamID == phongGiamDauTien);
                ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);
                ViewBag.Phong = phongGiam.Distinct().ToList();
            }
            else
            {
                var phongGiamDauTien = db.PhongGiam.FirstOrDefault().ID;
                var allPhamNhan = db.PhamNhan
                    .Where(p => DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu &&
                           p.PhongGiamID == phongGiamDauTien).ToList();

                ViewBag.Phong = db.PhongGiam.ToList();
                ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);
            }
            return View();
        }

        public ActionResult LoadPhamNhan(string phongID)
        {
            int _phong = int.Parse(phongID);
            var allPhamNhan = db.PhamNhan
                .Where(p => p.PhongGiamID == _phong && DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu)
                .ToList();
            ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);

            return PartialView("_PhamNhanPartial");
        }

        // POST: ThamGuis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhamNhanID,QuanNgucID,KeHoachThamGui,NgayThamGui,ThongTinNguoiThamHoi,SoLanThamHoi")] ThamGui thamGui)
        {
            if (ModelState.IsValid)
            {
                ViewBag.NgayThamGui = thamGui.NgayThamGui.HasValue ? thamGui.NgayThamGui.Value.ToString("MM/dd/yyyy") : null;
                thamGui.QuanNgucID = Guid.Parse(User.Identity.GetQuanNgucId());
                thamGui.KeHoachThamGui = 1;
                db.ThamGui.Add(thamGui); 
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thamGui);
        }

        // GET: ThamGuis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThamGui thamGui = db.ThamGui.Find(id);
            if (thamGui == null)
            {
                return HttpNotFound();
            }
            ViewBag.NgayThamGui = thamGui.NgayThamGui.HasValue ? thamGui.NgayThamGui.Value.ToString("MM/dd/yyyy") : null;
            return View(thamGui);
        }


        // POST: ThamGuis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PhamNhanID,QuanNgucID,KeHoachThamGui,NgayThamGui,ThongTinNguoiThamHoi,SoLanThamHoi")] ThamGui thamGui)
        {
            if (ModelState.IsValid)  
            {   //1
                thamGui.KeHoachThamGui = 1;  
                db.Entry(thamGui).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thamGui);
        }

        // GET: ThamGuis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThamGui thamGui = db.ThamGui.Find(id);
            if (thamGui == null)
            {
                return HttpNotFound();
            }
            return View(thamGui);
        }

        // POST: ThamGuis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThamGui thamGui = db.ThamGui.Find(id);
            db.ThamGui.Remove(thamGui);
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
