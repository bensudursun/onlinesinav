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
    
    public partial class tblOgrenciPuan
    {
        public int puanid { get; set; }
        public Nullable<int> puan { get; set; }
        public Nullable<int> soruid { get; set; }
        public Nullable<int> sinavid { get; set; }
        public Nullable<int> ogrenciid { get; set; }
    
        public virtual tblOgrenci tblOgrenci { get; set; }
        public virtual tblSinav tblSinav { get; set; }
        public virtual tblSoru tblSoru { get; set; }
    }
}