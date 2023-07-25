using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class PhamNhan
    {
        public Guid ID { get; set; }

        [DisplayName("Tên phạm nhân")]
        [Required(ErrorMessage = "Tên phạm nhân không được để trống")]
        public string TenPhamNhan { get; set; }

        [DisplayName("Bí danh")]
        [Required(ErrorMessage = "Bí danh phạm nhân không được để trống")]
        public string BiDanh { get; set; }

        [DisplayName("Ảnh nhận dạng")]
        //[Required(ErrorMessage = "Ảnh nhận dạng không được để trống")]
        public string AnhNhanDien { get; set; }

        [DisplayName("Quê quán")]
        [Required(ErrorMessage = "Quê quán không được để trống")]
        public string QueQuan { get; set; }

        [DisplayName("Ngày sinh")]
        //[Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime? NgaySinh { get; set; }

        [DisplayName("Giới tính")]
        [Required(ErrorMessage = "Giới tính không được để trống")]
        public int GioiTinh { get; set; } //enum

        [DisplayName("Khu")]
        //[Required(ErrorMessage = "Khu không được để trống")]
        public int IDKhu { get; set; }

        [DisplayName("Tội danh")]
        [Required(ErrorMessage = "Tội danh không được để trống")]
        public int ToiDanh { get; set; } //enum

        [DisplayName("Mức độ nguy hiểm")]
        [Required(ErrorMessage = "Mức độ nguy hiểm không được để trống")]
        public int MucDoNguyHiem { get; set; } //enum

        [DisplayName("Thời gian giam giữ")]
        [Required(ErrorMessage = "Thời gian giam giữ không được để trống")]
        public int SoNgayGiamGiu { get; set; }

        [DisplayName("Số thẻ căn cước")]
        [Required(ErrorMessage = "Số thẻ căn cước không được để trống")]
        public string CMND { get; set; }

        [DisplayName("Quá trình gây án")]
        [Required(ErrorMessage = "Quá trình gây án không được để trống")]
        public string QuaTrinhGayAn { get; set; }

        [DisplayName("Địa điểm gây án")]
        [Required(ErrorMessage = "Địa điểm gây án không được để trống")]
        public string DiaDiemGayAn { get; set; }

        [DisplayName("Phòng giam")]
        //[Required(ErrorMessage = "Phòng giam không được để trống")]
        public int PhongGiamID { get; set; }

        [DisplayName("Ngày vào trại")] 
        //[Required(ErrorMessage = "Ngày vào trại không được để trống")]
        public DateTime? NgayVaoTrai { get; set; }

        public virtual Khu Khu { get; set; }
        public virtual PhongGiam PhongGiam { get; set; }

    }
}