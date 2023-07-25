using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class BanGiaoPhamNhan
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Quản ngục nghỉ được để trống")]
        public Guid QuanNgucNghiID { get; set; }
        [Required(ErrorMessage = "Quản ngục nhận được để trống")]
        public Guid QuanNgucNhanID { get; set; }

        [Required(ErrorMessage = "Ngày nhận được để trống")]
        public DateTime NgayNhan { get; set; }
        public int SoNgayBanGiao { get; set; } 
        [ForeignKey("PhongGiam")] 
        public virtual int? PhongID { get; set; }
        public virtual PhongGiam PhongGiam { get; set; }
    }
}