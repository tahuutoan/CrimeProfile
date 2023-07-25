using Project4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
 
namespace Project4.Services
{
    public class NhungPhongDuocBanGiaoTamThoi 
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public List<PhongGiam> BanGiaoTamThoi(Guid idQuanNgucToGuid)
        {
            var nhungPhongDangDuocBanGiaoTamThoi = db.BanGiaoPhamNhan
                .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid
                && DbFunctions.DiffDays(DateTime.Now, w.NgayNhan) <= w.SoNgayBanGiao);
            return nhungPhongDangDuocBanGiaoTamThoi.Select(p => p.PhongGiam).ToList();
        } 
        public List<PhongGiam> BanGiaoVoThoiHan(Guid idQuanNgucToGuid)
        {
            var nhungPhongDuocBanGiao = db.BanGiaoCongViecCuaQuanNgucNghi
                        .Where(w => w.QuanNgucNhanID == idQuanNgucToGuid);
            return nhungPhongDuocBanGiao.Select(p => p.PhongGiam).ToList();
        }
    }
}