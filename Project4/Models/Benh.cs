using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class Benh
    {
        public int ID { get; set; }

        [DisplayName("Mã Phạm nhân")] 
        //[Required(ErrorMessage = "Phạm nhân không được để trống")]
        public Guid PhamNhanID { get; set; } 

        [DisplayName("Số ngày chữa trị")]
        [Required(ErrorMessage = "Số ngày chữa trị không được để trống")]
        public int NgayChuaTri { get; set; } 

        [DisplayName("Ngày bắt đầu chữa trị")] 
        //[Required(ErrorMessage = "Ngày bắt đầu chữa trị không được để trống")]
        public DateTime? NgayBatDauChuaTri { get; set; }

        public virtual PhamNhan PhamNhan { get; set; }
    } 
}