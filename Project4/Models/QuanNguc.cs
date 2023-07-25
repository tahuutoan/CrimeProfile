using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class QuanNguc
    {
        public Guid ID { get; set; }

        [DisplayName("Tên quản ngục")]
        [Required(ErrorMessage = "Tên quản ngục không được để trống")]
        public string TenQuanNguc { get; set; }

        [DisplayName("Ảnh nhận dạng")] 
        //[Required(ErrorMessage = "Ảnh nhận dạng không được để trống")]
        public string AnhNhanDien { get; set; }

        [DisplayName("Ngày sinh")]
        //[Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Quê quán")]
        [Required(ErrorMessage = "Quê quán không được để trống")]
        public string QueQuan { get; set; }

        [DisplayName("Giới tính")]
        //[Required(ErrorMessage = "Giới tính không được để trống")] 
        public int GioiTinh { get; set; }

        [DisplayName("Khu")]
        //[Required(ErrorMessage = "Khu không được để trống")]
        public int KhuID { get; set; }

        [DisplayName("Năm công tác")]
        //[Required(ErrorMessage = "Năm công tác không được để trống")]
        public DateTime? NamCongTac { get; set; }

        [DisplayName("Thời hạn công tác")]
        //[Required(ErrorMessage = "Thời hạn công tác không được để trống")]
        public DateTime? ThoiHanCongTac { get; set; }

        [DisplayName("Số thẻ căn cước")]
        [Required(ErrorMessage = "Số thẻ căn cước không được để trống")]
        public string CMND { get; set; }

        [DisplayName("Chức vụ")]
        [Required(ErrorMessage = "Chức vụ không được để trống")]
        public int ChucVu { get; set; } //enum
         
        [DisplayName("Quân hàm")] 
        [Required(ErrorMessage = "Quân hàm không được để trống")]
        public int QuanHam { get; set; } //enum

        public virtual Khu Khu { get; set; } 
    }
}