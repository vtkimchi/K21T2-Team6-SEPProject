
namespace BarcodeVer1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Info
        {

            public int code { get; set; }
            public List<Student> data { get; set; }
            public string message { get; set; }


    }
    public class Student
    {
        [Key]
        public string id { get; set; }
        public string fullname { get; set; }
        public string birthday { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}