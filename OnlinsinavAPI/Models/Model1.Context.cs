﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnlinesinavdbEntities1 : DbContext
    {
        public OnlinesinavdbEntities1()
            : base("name=OnlinesinavdbEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblBolum> tblBolum { get; set; }
        public virtual DbSet<tblBolumEgitmen> tblBolumEgitmen { get; set; }
        public virtual DbSet<tblDers> tblDers { get; set; }
        public virtual DbSet<tblEgitmen> tblEgitmen { get; set; }
        public virtual DbSet<tblEgitmenDers> tblEgitmenDers { get; set; }
        public virtual DbSet<tblFakulte> tblFakulte { get; set; }
        public virtual DbSet<tblKullanici> tblKullanici { get; set; }
        public virtual DbSet<tblOgrenci> tblOgrenci { get; set; }
        public virtual DbSet<tblOgrenciDers> tblOgrenciDers { get; set; }
        public virtual DbSet<tblOgrenciIsleri> tblOgrenciIsleri { get; set; }
        public virtual DbSet<tblSinav> tblSinav { get; set; }
        public virtual DbSet<tblYetki> tblYetki { get; set; }
        public virtual DbSet<tblYetkiTur> tblYetkiTur { get; set; }
        public virtual DbSet<tblSinavSoru> tblSinavSoru { get; set; }
        public virtual DbSet<tblSoru> tblSoru { get; set; }
        public virtual DbSet<tblOgrenciCevap> tblOgrenciCevap { get; set; }
        public virtual DbSet<tblOgrenciPuan> tblOgrenciPuan { get; set; }
    }
}
