//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BarcodeVer1._0.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Attendance
    {
        public int ID { get; set; }
        public Nullable<int> ID_Lesson { get; set; }
        public Nullable<int> ID_Student { get; set; }
        public bool Status { get; set; }
        public string Note { get; set; }
    
        public virtual Lesson Lesson { get; set; }
        public virtual Member Member { get; set; }
    }
}
