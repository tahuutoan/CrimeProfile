using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class CheDoNghiPhepCuaQuanNguc
    {
        public int ID { get; set; }

        [DisplayName("Quản ngục")]
        //[Required(ErrorMessage = "Quản ngục không được để trống")] 
        public Guid QuanNgucID { get; set; }

        [DisplayName("Số ngày nghỉ")]  
        [Required(ErrorMessage = "Số ngày nghỉ không được để trống")]
        public int SoNgayNghi { get; set; }

        [DisplayName("Lý do nghỉ")] 
        [Required(ErrorMessage = "Lý do nghỉ không được để trống")]
        public string LyDoNghi { get; set; } 

        public virtual QuanNguc QuanNguc { get; set; }
    }
}