﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO {
    public class RevenueDTO {

        private string month;
        private Double tongTien;

        public string Month { get => month; set => month = value; }
        public double TongTien { get => tongTien; set => tongTien = value; }
    }
}
