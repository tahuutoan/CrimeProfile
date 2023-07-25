﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class ThamGuiParam
    {
        public int ID { get; set; }

        [DisplayName("Phạm nhân")] 
        public string PhamNhanID { get; set; }
         
        [DisplayName("Quản ngục coi")] 
        public string QuanNgucID { get; set; }

        [DisplayName("Kế hoạch thăm gửi")]
        public int KeHoachThamGui { get; set; } //enum

        [DisplayName("Ngày thăm")]
        public DateTime NgayThamGui { get; set; }

        [DisplayName("Thông tin người thăm hỏi")]
        public string ThongTinNguoiThamHoi { get; set; }

        [DisplayName("Số lần thăm hỏi")]
        public int SoLanThamHoi { get; set; }
    }
}