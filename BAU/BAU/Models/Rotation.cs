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

    public partial class Rotation
    {
        public int Rotation_ID { get; set; }
        [Required]
        [Display(Name = "Start Rotation Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> Start_Date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Rotation Date")]
        public Nullable<System.DateTime> End_Date { get; set; }
    }
}
