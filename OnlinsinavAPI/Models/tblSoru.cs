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
    
    public partial class tblSoru
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSoru()
        {
            this.tblSinavSoru = new HashSet<tblSinavSoru>();
            this.tblOgrenciCevap = new HashSet<tblOgrenciCevap>();
            this.tblOgrenciPuan = new HashSet<tblOgrenciPuan>();
        }
    
        public int soruid { get; set; }
        public string soru { get; set; }
        public string dogru_cevap { get; set; }
        public Nullable<int> dersid { get; set; }
        public Nullable<int> sinavid { get; set; }
        public Nullable<int> soruno { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
    
        public virtual tblDers tblDers { get; set; }
        public virtual tblSinav tblSinav { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSinavSoru> tblSinavSoru { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOgrenciCevap> tblOgrenciCevap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOgrenciPuan> tblOgrenciPuan { get; set; }
    }
}
