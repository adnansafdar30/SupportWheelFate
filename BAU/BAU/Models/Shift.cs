//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BAU.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Shift
    {
        public int Day_ID { get; set; }
        public Nullable<int> Morning_Shift { get; set; }
        public Nullable<int> Evening_Shift { get; set; }
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Shift_Date { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual Employee Employee1 { get; set; }
    }
}
