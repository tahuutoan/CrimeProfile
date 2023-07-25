using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class QuanLyBenhTat
    {
        public int ID { get; set; }

        [DisplayName("Phạm nhân")]
        [Required(ErrorMessage = "Phạm nhân không được để trống")]
        public Guid PhamNhanID { get; set; }

        [DisplayName("Bệnh")] 
        [Required(ErrorMessage = "Bệnh không được để trống")]
        public int BenhID { get; set; }

        [DisplayName("Thời gian điều trị")]
        [Required(ErrorMessage = "Thời gian điều trị không được để trống")]
        public int ThoiGianDieuTri { get; set; }

        [DisplayName("Nơi điều trị")] 
        //[Required(ErrorMessage = "Nơi điều trị không được để trống")]
        public string NoiDieuTri { get; set; }

    }
}