using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project4.Models
{
    public class DoiMatKhauParams
    {
        [DataType(DataType.Password)]
        public string MatKhauHienTai { get; set; }

        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }

        //[Compare(CompareField = MatKhauMoi)]
        [DataType(DataType.Password)] 
        public string XacNhanMatKhauMoi { get; set; }
    }
}