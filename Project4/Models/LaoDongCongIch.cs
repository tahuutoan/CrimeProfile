using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class LaoDongCongIch
    {
        public int ID { get; set; }

        [DisplayName("Phạm nhân")]
        [Required(ErrorMessage = "Phạm nhân không được để trống")]
        public Guid PhamNhanID { get; set; }

        [DisplayName("Quản Ngục")]
        //[Required(ErrorMessage = "Quản Ngục không được để trống")]
        public Guid QuanNgucID { get; set; }

        [DisplayName("Khu làm việc")]
        [Required(ErrorMessage = "Khu làm việc không được để trống")]
        public int KhuVucLamViec { get; set; } //enum
         
        [DisplayName("Biểu hiện")]
        [Required(ErrorMessage = "Biểu hiện không được để trống")]
        public int BieuHien { get; set; } //enum

        public virtual PhamNhan PhamNhan { get; set; }
        public virtual QuanNguc QuanNguc { get; set; }
        public bool DangBiBenh { get; set; }
    }
}