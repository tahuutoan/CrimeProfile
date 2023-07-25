using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class BanGiaoCongViecCuaQuanNgucNghi
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Quản ngục nhận ID không được để trống")]
        public Guid QuanNgucNhanID { get; set; } 

        [Required(ErrorMessage = "Ngày nhận không được để trống")] 
        public DateTime NgayNhan { get; set; }

        [ForeignKey("PhongGiam")] 
        [Required(ErrorMessage = "Phòng ID không được để trống")] 
        public virtual int? PhongID { get; set; }

        public virtual PhongGiam PhongGiam { get; set; } 
    }
}