using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class ThamGui
    {
        public int ID { get; set; } 

        [DisplayName("Phạm nhân")]
        [Required(ErrorMessage = "Phạm nhân không được để trống")]
        public Guid PhamNhanID { get; set; }

        [DisplayName("Quản ngục coi")]
        //[Required(ErrorMessage = "Quản ngục không được để trống")] 
        public Guid QuanNgucID { get; set; }
         
        [DisplayName("Kế hoạch thăm gửi")]
        //[Required(ErrorMessage = "Kế hoạch thăm gửi không được để trống")]
        public int KeHoachThamGui { get; set; } //enum

        [DisplayName("Ngày thăm")]
        //[Required(ErrorMessage = "Ngày thăm không được để trống")]
        public DateTime? NgayThamGui { get; set; }

        [DisplayName("Thông tin người thăm hỏi")]
        [Required(ErrorMessage = "Thông tin người thăm hỏi không được để trống")]
        public string ThongTinNguoiThamHoi { get; set; }

        [DisplayName("Số lần thăm hỏi")]
        [Required(ErrorMessage = "Số lần thăm hỏi không được để trống")]
        public int SoLanThamHoi { get; set; }

        public virtual PhamNhan PhamNhan { get; set; }
        public virtual QuanNguc QuanNguc { get; set; }


    }
}