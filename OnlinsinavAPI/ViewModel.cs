using OnlinsinavAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlinsinavAPI
{
    public class ViewModel
    {
        public List<tblSinavSoru> tblSinavSorus { get; set; }
        public tblSinavSoru tblSinavSoru { get; set; }
    
        public List<tblSoru> tblSorus { get; set; }
        public List<tblSinav> tblSinavs { get; set; }
            public List<tblFakulte> tblFakultes { get; set; }
            public List<tblBolum> tblBolums { get; set; }
        
        public IEnumerable<tblEgitmen> tblEgitmens { get; set; }
        public IEnumerable<tblKullanici> tblKullaniciss { get; set; }
        public tblOgrenci tblOgrenci { get; set; }

        public tblEgitmen tblEgitmen { get; set; }
            public List<tblOgrenci> tblOgrencis { get; set;}
             public List<tblDers> tblDerss { get; set; }
        public List<tblOgrenciDers> tblOgrenciDers { get; set; }
        public List<tblEgitmenDers> tblEgitmenDers { get; set; }
        public List<tblKullanici> tblKullanicis { get; set; }
        public List<tblYetkiTur> tblYetkiTurs { get; set; }
        public List<tblYetki> tblYetkis { get; set; }
          
    }
}