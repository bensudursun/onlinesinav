//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlinsinavAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblEgitmenDers
    {
        public int egitmenDersid { get; set; }
        public Nullable<int> egitmenid { get; set; }
        public Nullable<int> dersid { get; set; }
    
        public virtual tblDers tblDers { get; set; }
        public virtual tblEgitmen tblEgitmen { get; set; }
    }
}
