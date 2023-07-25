using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class QuanNgucprTessssstt
    {
        public Guid ID { get; set; }  

        [DisplayName("Tên cán bộ")] 
        public string TenQuanNguc { get; set; }

        [DisplayName("Ngày sinh")]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Quê quán")]
        public string QueQuan { get; set; }

        [DisplayName("Giới tính")]
        public int GioiTinh { get; set; }
         
        [DisplayName("Khu Quản lý")] 
        public string KhuID { get; set; }

        [DisplayName("Năm công tác")]
        public DateTime? NamCongTac { get; set; } 

        [DisplayName("Thời hạn công tác")] 
        public DateTime? ThoiHanCongTac { get; set; } 

        [DisplayName("Số thẻ căn cước")]
        public string CMND { get; set; }

        [DisplayName("Chức vụ")]
        public string ChucVu { get; set; } //enum

        [DisplayName("Quân hàm")] 
        public string QuanHam { get; set; } //enum

        [DisplayName("Ảnh nhận dạng")]
        public string AnhNhanDien { get; set; }
    }
}