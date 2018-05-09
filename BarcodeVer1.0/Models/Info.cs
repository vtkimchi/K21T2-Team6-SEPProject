﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarcodeVer1._0.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Info
        {

            public int code { get; set; }
            public List<Datum> data { get; set; }
            public string message { get; set; }

            public class Datum
            {

                [Key]
                public string id { get; set; }
                public string fullname { get; set; }
                public string birthday { get; set; }
                public string firstname { get; set; }
                public string lastname { get; set; }
            }
    }
}