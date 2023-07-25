using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models 
{
    public class PhongGiam 
    {
        public int ID { get; set; }

        [DisplayName("Tên Phòng")]
        //[Required(ErrorMessage = "Tên Phòng không được để trống")]
        public string TenPhong { get; set; }

        [DisplayName("Khu")] 
        //[Required(ErrorMessage = "Khu không được để trống")]
        public int KhuID { get; set; }

        [DisplayName("Số phạm nhân tối đa")]
        public int SoLuongPhamNhanMax { get; set; }
    }
}