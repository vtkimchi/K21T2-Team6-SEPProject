using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarcodeVer1._0.Models
{
    public class Logins
    {
        public int code { get; set; }
        public account data { get; set; }
        public string message { get; set; }
    }
    public class account
    {
        public string id { get; set; }
        public string secret { get; set; }
    }
}