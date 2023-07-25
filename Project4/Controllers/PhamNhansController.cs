﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PagedList;
using Project4.Models;

namespace Project4.Controllers
{
    public class PhamNhansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: PhamNhans oh yes
        public ActionResult Index(string txtTenHoacMa, string khuID, string phongID, int? i)
        {
            var tmp = User.Identity.GetQuanNgucId();
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
            if (User.IsInRole("QuanNgucTruong"))
            {
                var khu = db.Khu.ToList();
                if (khu != null)
                {
                    //view bag 2 dropdown
                    ViewBag.Khu = khu;
                    ViewBag.SelectedKhu = khuID;
                    int idKhu = 5;
                    if (string.IsNullOrEmpty(khuID))
                    {
                    }
                    else
                    {
                        idKhu = int.Parse(khuID);
                    }
                    ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == idKhu).Distinct().ToList();
                }
                else
                {
                }
            }
            else
            {
                var quanNgucViaUser = User.Identity.GetQuanNgucId();
                var khuquannguc = db.QuanNguc.Find(Guid.Parse(quanNgucViaUser));
                if (khuquannguc == null)
                { 
                    return RedirectToAction("Index", "TrangChu");
                }
                else
                { 
                    ViewBag.Khu = db.Khu.Find(khuquannguc.KhuID);
                    ViewBag.SelectedKhu = khuquannguc.KhuID;
                    var allPhong = db.PhongGiam.Where(w => w.KhuID == khuquannguc.KhuID).ToList();
                    var idQuanNgucToGuid = Guid.Parse(quanNgucViaUser);
                    var nhungPhongDangDuocBanGiaoTamThoi = db.BanGiaoPhamNhan
                        .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid
                        && DbFunctions.DiffDays(w.NgayNhan, DateTime.Now) <= w.SoNgayBanGiao);
                    var npddbgtt = nhungPhongDangDuocBanGiaoTamThoi.Select(s => s.PhongGiam).ToList();
                    if (nhungPhongDangDuocBanGiaoTamThoi != null)
                    {
                        allPhong.AddRange(npddbgtt);
                    }
<<<<<<< HEAD

                    var nhungPhongDuocBanGiao = db.BanGiaoCongViecCuaQuanNgucNghi
                        .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid);
                    var npdbg = nhungPhongDuocBanGiao.Select(p => p.PhongGiam).ToList();

                    if (npdbg != null)
                    {
                        allPhong.AddRange(npdbg);
                    }

                    ViewBag.Phong = allPhong.Distinct();
=======
                    var nhungPhongBanGiaoDoQuanNgucNghi = db.BanGiaoCongViecCuaQuanNgucNghi.Where(w => w.QuanNgucNhanID == idQuanNgucToGuid);
                    var npbgdqnn = nhungPhongBanGiaoDoQuanNgucNghi.Select(s => s.PhongGiam).ToList();
                    if (nhungPhongBanGiaoDoQuanNgucNghi != null)
                    {
                        allPhong.AddRange(npbgdqnn);
                    }
                    ViewBag.Phong = allPhong;
>>>>>>> 8735bbc2b274040065dd3811d0d181406537f57c
                    if (string.IsNullOrEmpty(khuID)) khuID = khuquannguc.KhuID.ToString();
                    if (string.IsNullOrEmpty(phongID)) phongID = db.PhongGiam.FirstOrDefault(f => f.KhuID.ToString() == khuID).ID.ToString();
                }
            }
            //search  
            int khuid = 0, phongid = 0;
            if (string.IsNullOrEmpty(khuID)) khuid = 1;
            else khuid = Convert.ToInt32(khuID);

            if (string.IsNullOrEmpty(phongID)) phongid = 1;
            else phongid = Convert.ToInt32(phongID);

            if (string.IsNullOrEmpty(txtTenHoacMa)) txtTenHoacMa = "";
            var phamNhans = db.PhamNhan.Include(p => p.Khu).Include(p => p.PhongGiam)
                .Where(p => p.TenPhamNhan.Contains(txtTenHoacMa) && p.PhongGiamID == phongid
                && DbFunctions.DiffDays(p.NgayVaoTrai, DateTime.Now) < p.SoNgayGiamGiu);

<<<<<<< HEAD
=======
            //List<PhamNhan> listPhamNhan = db.PhamNhan
            //    .Where(w => (w.TenPhamNhan.Contains(txtTenHoacMa) || txtTenHoacMa == null)
            //     && (w.IDKhu == khuid)
            //     && (w.PhongGiamID == phongid))
            //    .ToList();  
            var listPhamNhan = from p in db.PhamNhan
                               join k in db.Khu
                                    on p.IDKhu equals k.ID
                               join ph in db.PhongGiam
                                    on p.PhongGiamID equals ph.ID
                               where p.TenPhamNhan.Contains(txtTenHoacMa)
                                    && p.PhongGiamID == phongid && DbFunctions.DiffDays(DateTime.Now, p.NgayVaoTrai) <= p.SoNgayGiamGiu //bỏ tìm theo khu đi
                               select new PhamNhanParams
                               {
                                   ID = p.ID,
                                   TenPhamNhan = p.TenPhamNhan,
                                   BiDanh = p.BiDanh,
                                   AnhNhanDien = p.AnhNhanDien,
                                   QueQuan = p.QueQuan,
                                   NgaySinh = p.NgaySinh,
                                   GioiTinh = p.GioiTinh,
                                   IDKhu = k.TenKhu,
                                   ToiDanh = p.ToiDanh,
                                   MucDoNguyHiem = p.MucDoNguyHiem,
                                   SoNgayGiamGiu = p.SoNgayGiamGiu,
                                   CMND = p.CMND,
                                   QuaTrinhGayAn = p.QuaTrinhGayAn,
                                   DiaDiemGayAn = p.DiaDiemGayAn,
                                   PhongGiamID = ph.TenPhong,
                               };
>>>>>>> 8735bbc2b274040065dd3811d0d181406537f57c
            int pageSize = 5;
            int pageNumber = (i ?? 1);

            ViewBag.GioiTinh = Common.CommonConstant.gioiTinh;
            ViewBag.ToiDanh = Common.CommonConstant.toiDanh;
            ViewBag.MucDoNguyHiem = Common.CommonConstant.mucDoNguyHiem;
            return View(phamNhans.OrderBy(p => p.TenPhamNhan).ToPagedList(pageNumber, pageSize));
        }


        public ActionResult ThanhTimKiem(string khuID)
        {
            ViewBag.Khu = db.Khu.ToList();
            ViewBag.SelectedKhu = khuID;
            int idKhu = int.Parse(khuID);
            ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == idKhu).ToList();

            return PartialView("_PhamNhansSearchBar");
        }

        //public ActionResult TimKiem(string txtTenHoacMa, string khuID, string phongID, int? i)
        //{
        //    IQueryable<PhamNhan> listPhamNhan = db.PhamNhan;
        //    if (txtTenHoacMa.Length == 5)
        //    {
        //        //listPhamNhan = db.PhamNhan.Where(w => w. == txtTenOrMa);
        //    }
        //    else
        //    {
        //    }


        //    var listPhamNhan = db.PhamNhan.Where(w => w.TenPhamNhan.Contains(txtTenHoacMa));
        //    if (string.IsNullOrEmpty(khuID))
        //    {
        //        khuID = "1";
        //    }
        //    var IDkhu = int.Parse(khuID);
        //    listPhamNhan = listPhamNhan.Where(w => w.IDKhu == IDkhu);
        //    if (phongID != string.Empty || phongID != "null")
        //    {
        //        var IDphong = int.Parse(phongID);
        //        listPhamNhan = listPhamNhan.Where(w => w.PhongGiamID == IDphong);
        //    }
        //    return View("", listPhamNhan.OrderBy(o => o.TenPhamNhan).ToPagedList(i ?? 1, 7));
        //    return PartialView("_PhamNhansDataTable", listPhamNhan.OrderBy(p => p.TenPhamNhan).ToPagedList(i ?? 1, 7));
        //}


        // GET: PhamNhans/Details/5
        public ActionResult Details(Guid? id)
        {
            ViewBag.GioiTinh = Common.CommonConstant.gioiTinh;
            ViewBag.ToiDanh = Common.CommonConstant.toiDanh;
            ViewBag.MucDoNguyHiem = Common.CommonConstant.mucDoNguyHiem;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhamNhan phamNhan = db.PhamNhan.Find(id);
            if (phamNhan == null)
            {
                return HttpNotFound();
            }
            return View(phamNhan);
        }

        // GET: PhamNhans/Create 2222D
        public ActionResult Create()
        {
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", null);
            ViewBag.ToiDanh = new SelectList(Common.CommonConstant.toiDanh, "Key", "Value", null);
            ViewBag.MucDoNguyHiem = new SelectList(Common.CommonConstant.mucDoNguyHiem, "Key", "Value", null);
            ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", null);
            ViewBag.PhongGiamID = new SelectList(db.PhongGiam, "ID", "TenPhong", null);
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", quanNgucHienTai.KhuID);
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    phongGiamConCho.Remove(item);
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", null);
            }
            else
            {
                var khuAid = db.Khu.FirstOrDefault(f => f.TenKhu == "Khu A");
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == khuAid.ID).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    phongGiamConCho.Remove(item);
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", null);
            }
            return View();
        }

        public ActionResult LoadPhong(string khuID, string phongDangChuaPhamNhanID)
        {
            ViewBag.IDKhu = db.Khu.ToList();
            ViewBag.SelectedKhu = khuID;
            int idKhu = int.Parse(khuID);
            ViewBag.PhongGiamID = new SelectList(db.PhongGiam.Where(w => w.KhuID == idKhu), "ID", "TenPhong", null);

            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", quanNgucHienTai.KhuID);
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    int i = 0;
                    if (!int.TryParse(phongDangChuaPhamNhanID, out i) && item.ID != int.Parse(phongDangChuaPhamNhanID))
                    {
                        phongGiamConCho.Remove(item);
                    }
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong");
            }
            else
            {
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == idKhu).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    int i = 0;
                    if (!int.TryParse(phongDangChuaPhamNhanID, out i) && item.ID != int.Parse(phongDangChuaPhamNhanID))
                    {
                        phongGiamConCho.Remove(item);
                    }
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong");
            }
            return PartialView("_PhongPartial");
        }

        // POST: PhamNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,TenPhamNhan,BiDanh,AnhNhanDien,QueQuan,NgaySinh,GioiTinh,IDKhu,ToiDanh,MucDoNguyHiem,SoNgayGiamGiu,CMND,QuaTrinhGayAn,DiaDiemGayAn,PhongGiamID")] PhamNhan phamNhan, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string url = string.Empty;
                phamNhan.ID = Guid.NewGuid();
                phamNhan.NgayVaoTrai = DateTime.Now;
                FileUpload(file, phamNhan.ID, out url);
                if (url == string.Empty)
                {
                    //error = "You must include a Featured Image for event.";
                    //ViewBag.Error = error;
                    ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", null);
                    ViewBag.ToiDanh = new SelectList(Common.CommonConstant.toiDanh, "Key", "Value", null);
                    ViewBag.MucDoNguyHiem = new SelectList(Common.CommonConstant.mucDoNguyHiem, "Key", "Value", null);
                    ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", null);
                    ViewBag.PhongGiamID = new SelectList(db.PhongGiam, "ID", "TenPhong", null);
                    if (!User.IsInRole("QuanNgucTruong"))
                    {
                        var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                        ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", quanNgucHienTai.KhuID);
                        var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).ToList();
                        List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                        foreach (var item in phongGiamConCho)
                        {
                            if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                            {
                                phongGiamRemove.Add(item);
                            }
                        }
                        foreach (var item in phongGiamRemove)
                        {
                            phongGiamConCho.Remove(item);
                        }
                        ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", null);
                    }
                    else
                    {
                        var phongGiamConCho = db.PhongGiam.ToList();
                        List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                        foreach (var item in phongGiamConCho)
                        {
                            if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                            {
                                phongGiamRemove.Add(item);
                            }
                        }
                        foreach (var item in phongGiamRemove)
                        {
                            phongGiamConCho.Remove(item);
                        }
                        ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", null);
                    }
                    return View(phamNhan);
                }
                phamNhan.AnhNhanDien = url;
                if (!User.IsInRole("QuanNgucTruong"))
                {
                    var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                    phamNhan.IDKhu = quanNgucHienTai.KhuID;
                }
                db.PhamNhan.Add(phamNhan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NgaySinh = phamNhan.NgaySinh.HasValue ? phamNhan.NgaySinh.Value.ToString("MM/dd/yyyy") : null;
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", null);
            ViewBag.ToiDanh = new SelectList(Common.CommonConstant.toiDanh, "Key", "Value", null);
            ViewBag.MucDoNguyHiem = new SelectList(Common.CommonConstant.mucDoNguyHiem, "Key", "Value", null);
            ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", null);
            ViewBag.PhongGiamID = new SelectList(db.PhongGiam, "ID", "TenPhong", null);
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", quanNgucHienTai.KhuID);
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    phongGiamConCho.Remove(item);
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", null);
            }
            else
            {
                var phongGiamConCho = db.PhongGiam.ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    phongGiamConCho.Remove(item);
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", null);
            }
            return View(phamNhan);
        }

        // GET: PhamNhans/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhamNhan phamNhan = db.PhamNhan.Find(id);
            if (phamNhan == null)
            {
                return HttpNotFound();
            }
            ViewBag.NgaySinh = phamNhan.NgaySinh.HasValue ? phamNhan.NgaySinh.Value.ToString("MM/dd/yyyy") : null;
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", phamNhan.GioiTinh);
            ViewBag.ToiDanh = new SelectList(Common.CommonConstant.toiDanh, "Key", "Value", phamNhan.ToiDanh);
            ViewBag.MucDoNguyHiem = new SelectList(Common.CommonConstant.mucDoNguyHiem, "Key", "Value", phamNhan.MucDoNguyHiem);
            ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", phamNhan.IDKhu);
            ViewBag.PhongGiamID = new SelectList(db.PhongGiam, "ID", "TenPhong", phamNhan.PhongGiamID);
            ViewBag.PhongDangChuaPhamNhan = phamNhan.PhongGiamID;
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", quanNgucHienTai.KhuID);
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    if (item.ID != phamNhan.PhongGiamID)
                    {
                        phongGiamConCho.Remove(item);
                    }
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", phamNhan.PhongGiamID);
            }
            else
            {
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == phamNhan.IDKhu).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    if (item.ID != phamNhan.PhongGiamID)
                    {
                        phongGiamConCho.Remove(item);
                    }
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", phamNhan.PhongGiamID);
            }
            return View(phamNhan);
        }

        // POST: PhamNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TenPhamNhan,BiDanh,AnhNhanDien,QueQuan,NgaySinh,GioiTinh,IDKhu,ToiDanh,MucDoNguyHiem,SoNgayGiamGiu,CMND,QuaTrinhGayAn,DiaDiemGayAn,PhongGiamID,NgayVaoTrai")] PhamNhan phamNhan, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                string url = string.Empty;
                FileUpload(file, phamNhan.ID, out url);
                if (url != string.Empty)
                {
                    phamNhan.AnhNhanDien = url;
                }
                if (!User.IsInRole("QuanNgucTruong"))
                {
                    var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                    //phamNhan.IDKhu = quanNgucHienTai.KhuID;
                }
                db.Entry(phamNhan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NgaySinh = phamNhan.NgaySinh.HasValue ? phamNhan.NgaySinh.Value.ToString("MM/dd/yyyy") : null;
            ViewBag.GioiTinh = new SelectList(Common.CommonConstant.gioiTinh, "Key", "Value", phamNhan.GioiTinh);
            ViewBag.ToiDanh = new SelectList(Common.CommonConstant.toiDanh, "Key", "Value", phamNhan.ToiDanh);
            ViewBag.MucDoNguyHiem = new SelectList(Common.CommonConstant.mucDoNguyHiem, "Key", "Value", phamNhan.MucDoNguyHiem);
            ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", phamNhan.IDKhu);
            ViewBag.PhongGiamID = new SelectList(db.PhongGiam, "ID", "TenPhong", phamNhan.PhongGiamID);
            ViewBag.PhongDangChuaPhamNhan = phamNhan.PhongGiamID;
            if (!User.IsInRole("QuanNgucTruong"))
            {
                var quanNgucHienTai = db.QuanNguc.Find(Guid.Parse(User.Identity.GetQuanNgucId()));
                ViewBag.IDKhu = new SelectList(db.Khu, "ID", "TenKhu", quanNgucHienTai.KhuID);
                var phongGiamConCho = db.PhongGiam.Where(w => w.KhuID == quanNgucHienTai.KhuID).ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    if (item.ID != phamNhan.PhongGiamID)
                    {
                        phongGiamConCho.Remove(item);
                    }
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", phamNhan.PhongGiamID);
            }
            else
            {
                var phongGiamConCho = db.PhongGiam.ToList();
                List<PhongGiam> phongGiamRemove = new List<PhongGiam>();
                foreach (var item in phongGiamConCho)
                {
                    if (db.PhamNhan.Where(w => w.PhongGiamID == item.ID).Count() == item.SoLuongPhamNhanMax)
                    {
                        phongGiamRemove.Add(item);
                    }
                }
                foreach (var item in phongGiamRemove)
                {
                    if (item.ID != phamNhan.PhongGiamID)
                    {
                        phongGiamConCho.Remove(item);
                    }
                }
                ViewBag.PhongGiamID = new SelectList(phongGiamConCho, "ID", "TenPhong", phamNhan.PhongGiamID);
            }
            return View(phamNhan);
        }

        // GET: PhamNhans/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhamNhan phamNhan = db.PhamNhan.Find(id);
            if (phamNhan == null)
            {
                return HttpNotFound();
            }
            return View(phamNhan);
        }

        // POST: PhamNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            PhamNhan phamNhan = db.PhamNhan.Find(id);
            db.PhamNhan.Remove(phamNhan);
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

        public void FileUpload(HttpPostedFileBase file, Guid phamNhanID, out string url)
        {
            url = string.Empty;
            try
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    DirectoryInfo di = Directory.CreateDirectory(Server.MapPath($"~\\Common\\Image\\PhamNhans\\{phamNhanID}"));
                    string path = System.IO.Path.Combine(
                                           Server.MapPath($"~/Common/Image/PhamNhans/{phamNhanID}"), pic);
                    url = $"~/Common/Image/PhamNhans/{phamNhanID}/" + pic;
                    // file is uploaded
                    file.SaveAs(path);

                    // save the image path path to the database or you can send image 
                    // directly to database
                    // in-case if you want to store byte[] ie. for DB
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                }
            }
            catch (Exception ex)
            {

            }

            // after successfully uploading redirect the user
            //return RedirectToAction("actionname", "controller name");
        }


        public ActionResult Dashboard(string month, string year)
        {
            var y = db.PhamNhan.Select(o => o.NgayVaoTrai.Value.Year).Distinct();
            if (y != null)
            {
                //view bag 2 dropdown
                ViewBag.Year = y;
                ViewBag.SelectedYear = year;
                int cur_year = 1;
                if (string.IsNullOrEmpty(year))
                {
                    var firstY = y.FirstOrDefault(); 
                    ViewBag.Month = db.PhamNhan.Where(w => w.NgayVaoTrai.Value.Year == firstY).Select(o => o.NgayVaoTrai.Value.Month).Distinct();
                }
                else
                {
                    cur_year = int.Parse(year);
                    ViewBag.Month = db.PhamNhan.Where(w => w.NgayVaoTrai.Value.Year == cur_year).Select(o => o.NgayVaoTrai.Value.Month).Distinct();
                }
                //ViewBag.Phong = db.PhongGiam.Where(w => w.KhuID == idKhu).ToList();


            }
            else
            {
            }
            //ViewBag.Month = db.PhamNhan.Select(o => o.NgayVaoTrai.Value.Month).Distinct();
            //ViewBag.Year = db.PhamNhan.Select(o => o.NgayVaoTrai.Value.Year).Distinct();
            if (String.IsNullOrEmpty(month) || String.IsNullOrEmpty(year))
            {
                var list = db.PhamNhan.OrderBy(o => o.NgayVaoTrai).ToList();
                List<int> repartitions = new List<int>();
                var vaoTrai = list.Select(x => x.NgayVaoTrai.Value.ToString("yyyy")).Distinct();
                //var vaoTrai = list.Select(x => x.SoNgayGiamGiu).Distinct();

                foreach (var item in vaoTrai)
                {
                    repartitions.Add(list.Count(x => x.NgayVaoTrai.Value.ToString("yyyy") == item));
                    //repartitions.Add(list.Count(x => x.SoNgayGiamGiu == item));
                    var rep = repartitions;
                    ViewBag.VaoTrai = vaoTrai;
                    ViewBag.REP = repartitions.ToList();
                }
            }
            else
            {
                var list = db.PhamNhan.OrderBy(o => o.NgayVaoTrai).ToList();
                List<int> repartitions = new List<int>();
                var vaoTrai = list.Where(x => x.NgayVaoTrai.Value.Month == int.Parse(month) && x.NgayVaoTrai.Value.Year == int.Parse(year)).Select(x => x.NgayVaoTrai.Value.ToString("MM/dd/yyyy")).Distinct();
                foreach (var item in vaoTrai)
                {
                    repartitions.Add(list.Count(x => x.NgayVaoTrai.Value.ToString("MM/dd/yyyy") == item));
                    //repartitions.Add(list.Count(x => x.SoNgayGiamGiu == item));
                    var rep = repartitions;
                }
                ViewBag.VaoTrai = vaoTrai;
                ViewBag.REP = repartitions.ToList();
                return View();
            }

            return View();
        }
        //public ActionResult Chart(string time)
        //{

        //}

        public ActionResult Filter4Chart(string year)
        {
            ViewBag.Year = db.PhamNhan.Select(o => o.NgayVaoTrai.Value.Year).Distinct();
            ViewBag.SelectedYear = year;
            int cur_year = int.Parse(year);
            ViewBag.Month = db.PhamNhan.Where(w => w.NgayVaoTrai.Value.Year == cur_year).Select(o => o.NgayVaoTrai.Value.Month).Distinct();

            return PartialView("_Filter4Chart");
        }
    }
}
