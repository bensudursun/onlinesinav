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
    
    public partial class tblDers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblDers()
        {
            this.tblEgitmen = new HashSet<tblEgitmen>();
            this.tblEgitmenDers = new HashSet<tblEgitmenDers>();
            this.tblOgrenci = new HashSet<tblOgrenci>();
            this.tblOgrenciDers = new HashSet<tblOgrenciDers>();
            this.tblSinav = new HashSet<tblSinav>();
            this.tblSoru = new HashSet<tblSoru>();
        }
    
        public int dersid { get; set; }
        public string dersad { get; set; }
        public string derskod { get; set; }
        public Nullable<int> derssaat { get; set; }
        public Nullable<int> derskredi { get; set; }
        public Nullable<int> dersdonem { get; set; }
        public Nullable<int> dersakts { get; set; }
        public Nullable<int> bolumid { get; set; }
    
        public virtual tblBolum tblBolum { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEgitmen> tblEgitmen { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblEgitmenDers> tblEgitmenDers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOgrenci> tblOgrenci { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblOgrenciDers> tblOgrenciDers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSinav> tblSinav { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblSoru> tblSoru { get; set; }
    }
}
