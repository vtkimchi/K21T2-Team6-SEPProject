using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarcodeVer1._0.Models
{
    public class Course
    {
        public int code { get; set; }
        public List<Datum> data { get; set; }
        public string message { get; set; }

        public class Datum
        {
            public string id { get; set; }
            public string name { get; set; }
            public string fnfo { get; set; }
        }

    }
}