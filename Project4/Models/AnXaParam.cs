using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class AnXaParam 
    { 
        public int ID { get; set; }
         
        [DisplayName("Phạm nhân")] 
        [Required(ErrorMessage = "Phạm nhân không được để trống")]
        public string PhamNhanID { get; set; }

        [DisplayName("Mức độ ân xá")]
        [Required(ErrorMessage = "Mức độ ân xá không được để trống")]
        public int MucDoAnXa { get; set; } //enum

        [DisplayName("Mức độ cải tạo")] 
        [Required(ErrorMessage = "Mức độ cải tạo không được để trống")]
        public int MucDoCaiTao { get; set; } //enum
    }
}