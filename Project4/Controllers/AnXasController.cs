using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project4.Models;
using Project4.Common;
using Project4.Services;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;

namespace Project4.Controllers
{ 
    public class AnXasController : Controller
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
        // GET: AnXas   03/31  
        public ActionResult Index(string tenPhamNhan, int? i)
        {
            if (string.IsNullOrEmpty(tenPhamNhan)) tenPhamNhan = "";
            var anXas = db.AnXa.Include(a => a.PhamNhan)
                .Where(a => a.PhamNhan.TenPhamNhan.Contains(tenPhamNhan) &&
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
                    var phamNhan = db.PhamNhan
                        .Where(w => nhungPhongDangDuocBanGiaoTamThoi.Contains(w.PhongGiamID)).Select(p => p.ID);
                    if (phamNhan != null)
                    {
                        var anXa = db.AnXa.Where(w => phamNhan.Contains(w.PhamNhanID)).ToList();
                        if (anXa != null)
                        {
                            anXas.AddRange(anXa);
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
                        var anXa = db.AnXa
                            .Where(w => phamNhan.Contains(w.PhamNhanID)).ToList();
                        if (anXa != null)
                        {
                            anXas.AddRange(anXa);
                        }
                    }
                }
            }

            int pageSize = 5;
            int pageNumber = (i ?? 1);
            ViewBag.Khu = db.Khu.ToList();
            ViewBag.Phong = db.PhongGiam.ToList();
            ViewBag.mucDoAnXa = Common.CommonConstant.mucDoAnXa;
            ViewBag.mucDoCaiTao = Common.CommonConstant.mucDoCaiTao;
            return View(anXas.Distinct().OrderBy(t => t.PhamNhanID).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public JsonResult getAllAnXa(string txtSearch, int? page)
        {
            var data = (from s in db.AnXa select s);
            if (!String.IsNullOrEmpty(txtSearch))
            {
                ViewBag.txtSearch = txtSearch;
                data = data.Where(s => s.PhamNhanID.ToString().Contains(txtSearch));
            }

            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int start = (int)(page - 1) * 3;

            ViewBag.pageCurrent = page;
            int totalPage = data.Count();
            float totalNumsize = (totalPage / (float)3);
            int numSize = (int)Math.Ceiling(totalNumsize);
            ViewBag.numSize = numSize;
            var dataPost = data.OrderByDescending(x => x.ID).Skip(start).Take(3);
            List<AnXa> listAnXa = new List<AnXa>();
            listAnXa = dataPost.ToList();
            // return Json(listPost);
            return Json(new { data = listAnXa, pageCurrent = page, numSize = numSize }, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult ThanhTimKiem(string khuID)
        //{
        //    ViewBag.Khu = db.Khu.ToList();
        //    ViewBag.SelectedKhu = khuID;
        //    int idKhu = int.Parse(khuID);
        //    ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == idKhu).ToList();

        //    return PartialView("_AnXaSearchBar");
        //}

        //public ActionResult TimKiem(string txtTenHoacMa, string khuID, string phongID)
        //{
        //    IQueryable<PhamNhan> listPhamNhan = db.PhamNhan;
        //    List<AnXa> listAnXa = db.AnXa.ToList();
        //    if (txtTenHoacMa.Length == 5)
        //    {
        //        //listPhamNhan = db.PhamNhan.Where(w => w. == txtTenOrMa);
        //    }
        //    else
        //    {
        //        listPhamNhan = listPhamNhan.Where(w => w.TenPhamNhan.Contains(txtTenHoacMa));
        //    }
        //    var IDkhu = int.Parse(khuID);
        //    listPhamNhan = listPhamNhan.Where(w => w.IDKhu == IDkhu);
        //    if (phongID != string.Empty || phongID != "null")
        //    {
        //        var IDphong = int.Parse(phongID);
        //        listPhamNhan = listPhamNhan.Where(w => w.PhongGiamID == IDphong);
        //    }
        //    foreach (var item in listPhamNhan)
        //    {
        //        listAnXa.Add(listAnXa.FirstOrDefault(f => f.PhamNhanID == item.ID));
        //    }
        //    return PartialView("_AnXaDataTable", listAnXa);
        //}
        // GET: AnXas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnXa anXa = db.AnXa.Find(id);
            if (anXa == null)
            {
                return HttpNotFound();
            }
            return View(anXa);
        }

        // GET: AnXas/Create
        public ActionResult Create()
        {
            ViewBag.mucDoAnXa = new SelectList(Common.CommonConstant.mucDoAnXa, "Key", "Value", null);
            ViewBag.mucDoCaiTao = new SelectList(Common.CommonConstant.mucDoCaiTao, "Key", "Value", null);
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
                var allPhamNhan = db.PhamNhan.Where(p => !bieuHienKem.Contains(p.ID) && p.IDKhu == _khu
                && p.PhongGiamID == phongGiamDauTien && 
                 DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu).ToList(); 
                ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);
                ViewBag.Phong = phongGiam.Distinct().ToList();  
            }
            else
            {   
                var phongGiam = db.PhongGiam.ToList();
                ViewBag.Phong = phongGiam;

                var phongGiamDauTien = phongGiam.FirstOrDefault().ID;
                var bieuHienKem = db.LaoDongCongIch.Where(l => l.BieuHien == 5).Select(l => l.PhamNhanID);

                var allPhamNhan = db.PhamNhan.Where(p => !bieuHienKem.Contains(p.ID) &&
                 DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu && 
                 p.PhongGiamID == phongGiamDauTien).ToList(); 

                ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);
            }
            return View();
        }

        public ActionResult LoadPhamNhan(string phongID)
        {
            int _phong = int.Parse(phongID);
            var bieuHienKem = db.LaoDongCongIch.Where(l => l.BieuHien == 5).Select(l => l.PhamNhanID);
            var allPhamNhan = db.PhamNhan
                .Where(p => !bieuHienKem.Contains(p.ID) && p.PhongGiamID == _phong &&
                 DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu)
                .ToList();
            ViewBag.PhamNhanID = new SelectList(allPhamNhan, "ID", "TenPhamNhan", null);
            return PartialView("_PhamNhanPartial");
        }

        // POST: AnXas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PhamNhanID,MucDoAnXa,MucDoCaiTao")] AnXa anXa)
        {
            if (ModelState.IsValid)
            {
                int soNgayGiamAn = 0;
                switch (anXa.MucDoAnXa)
                {
                    case 1:
                        soNgayGiamAn = 30;
                        break;
                    case 2:
                        soNgayGiamAn = 90;
                        break;
                    case 3:
                        soNgayGiamAn = 180;
                        break;
                    case 4:
                        soNgayGiamAn = 270;
                        break;
                    case 5:
                        soNgayGiamAn = 365;
                        break;
                    case 6:
                        soNgayGiamAn = 730;
                        break;
                    case 7:
                        soNgayGiamAn = 1095;
                        break;
                    case 8:
                        soNgayGiamAn = 1460;
                        break;
                    default:
                        break;
                }
                PhamNhanGiamAn(soNgayGiamAn, anXa.PhamNhanID);
                db.AnXa.Add(anXa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            return View(anXa);
        }

        private void PhamNhanGiamAn(int soNgayGiamAn, Guid phamNhanID)
        {
            var phamNhan = db.PhamNhan.Find(phamNhanID);
            phamNhan.SoNgayGiamGiu = phamNhan.SoNgayGiamGiu - soNgayGiamAn;
            if (phamNhan.SoNgayGiamGiu <= 0)
            {
                phamNhan.SoNgayGiamGiu = 0;
            }
        }


        //// GET: AnXas/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    AnXa anXa = db.AnXa.Find(id);
        //    if (anXa == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(anXa);
        //}

        //// POST: AnXas/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,PhamNhanID,MucDoAnXa,MucDoCaiTao")] AnXa anXa)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(anXa).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(anXa);
        //}

        // GET: AnXas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AnXa anXa = db.AnXa.Find(id);
            if (anXa == null)
            {
                return HttpNotFound();
            }
            return View(anXa);
        }

        // POST: AnXas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AnXa anXa = db.AnXa.Find(id);
            db.AnXa.Remove(anXa);
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
