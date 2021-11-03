using OnlinsinavAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlinsinavAPI.Controllers
{
    public class EgitmenController : Controller
    {
        OnlinesinavdbEntities1 db = new OnlinesinavdbEntities1();

        // GET: Egitmen

        public ActionResult Egitmen()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {


                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult Sinavlar(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {
                ViewModel model = new ViewModel();
                tblEgitmen egitmens = new tblEgitmen();
                tblSinav sinavs = new tblSinav();
                egitmens.egitmenid = id;
                var egitmen = (from k in db.tblEgitmen where k.egitmenid == id select k).FirstOrDefault();
                if (egitmen.egitmenid == id)
                {
                    var egitmenders = (from egitmenid in db.tblEgitmen where egitmen.egitmenid == id select egitmen.egitmenid).FirstOrDefault();
                    var sinav = (from o in db.tblSinav where o.egitmenid == egitmenders select o).Include(x => x.tblDers).Include(x => x.tblEgitmen).ToList();
                    model.tblSinavs = (from o in db.tblSinav where o.egitmenid == id select o).ToList();
                    return View(model);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }

        public ActionResult EgitmenDersListesi(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {
                ViewModel model = new ViewModel();
                model.tblBolums = db.tblBolum.ToList();
                tblEgitmen egitmens = new tblEgitmen();
                egitmens.egitmenid = id;
                var egitmen = (from k in db.tblEgitmen where k.egitmenid == id select k).FirstOrDefault();
                if (egitmen.egitmenid == id)
                {
                    var egitmenders = (from egitmenid in db.tblEgitmen where egitmen.egitmenid == id select egitmen.egitmenid).FirstOrDefault();
                    var ders = (from o in db.tblEgitmenDers where o.egitmenid == egitmenders select o).Include(x => x.tblDers).ToList();


                    return View(ders);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult SinavEkle(int id, int id2)
        {

            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {

                ViewModel model = new ViewModel();
                var ders = (from k in db.tblEgitmenDers where k.dersid == id select k).FirstOrDefault();
                var egitmen = (from k in db.tblEgitmenDers where k.egitmenid == id2 select k).FirstOrDefault();
                if (ders.dersid == id && egitmen.egitmenid == id2)
                {
                    model.tblEgitmens = (from o in db.tblEgitmen where o.egitmenid == id2 select o).ToList();
                    model.tblDerss = (from o in db.tblDers where o.dersid == id select o).ToList();
                    return View(model);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        [HttpPost]
        public ActionResult SinavKaydet(int selectegitmen, int selectders, string sinavad, DateTime tarih,DateTime tarihbitis)
        {
            var sinavlar = db.tblSinav.FirstOrDefault(x => x.sinavad == sinavad);
            tblEgitmen egitmenler = new tblEgitmen();
            ViewModel model = new ViewModel();
            if (sinavlar == null)
            {
                model.tblSinavs = db.tblSinav.Include(x => x.tblDers).Include(x => x.tblEgitmen).ToList();
                var ders = db.tblEgitmenDers.FirstOrDefault(x => x.dersid == selectders);
                var egitmen = db.tblEgitmenDers.FirstOrDefault(x => x.egitmenid == selectegitmen);

                tblSinav sinav = new tblSinav();
                sinav.dersid = selectders;
                sinav.egitmenid = selectegitmen;
                sinav.sinavtarih = tarih;
                sinav.sinavbitis = tarihbitis;
                sinav.sinavad = sinavad;
                db.tblSinav.Add(sinav);
            }
            else
            {
                TempData["mesajsinav"] = "Bu derse ait sinav bulunmaktadır.";
            }
            db.SaveChanges();

            return RedirectToAction("Egitmen", "Egitmen");
        }
        public ActionResult SinavSil(int id)
        {
            var silinecekSoru = db.tblSinavSoru.FirstOrDefault(x => x.soruid == id);
            var silinecekSinav = db.tblSinav.FirstOrDefault(x => x.sinavid == id);
            var silinecekSinavSoru = db.tblSinavSoru.FirstOrDefault(x => x.sinavid == id);
            if (silinecekSinavSoru != null)
            {
                db.tblSinavSoru.Remove(silinecekSinavSoru);
                db.SaveChanges();
            }
            db.tblSinav.Remove(silinecekSinav);
            db.SaveChanges();


            return RedirectToAction("Egitmen", "Egitmen");
        }

      
       public ActionResult  SoruEkle(int id)
        {


            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {

                ViewModel model = new ViewModel();
                var sinav = (from k in db.tblSinav where k.sinavid == id select k).FirstOrDefault();
                var ders = (from o in db.tblDers where o.dersid == id select o).FirstOrDefault();
                if (ders.dersid == id)
                {
                    model.tblDerss = (from o in db.tblDers where o.dersid == id select o).ToList();
                    return View(model);
                }
               
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        
     
        [HttpPost]
        public ActionResult SoruKaydet(int selectders,string dogru_cevap, string soru,string A, string B,string C,string D,string E)
        {
            tblSoru sorular = new tblSoru();
            sorular.dersid = selectders;
            sorular.soru = soru;
            sorular.A = A;
            sorular.B = B;
            sorular.C = C;
            sorular.D= D;
            sorular.E = E;
            sorular.dogru_cevap = dogru_cevap;
            db.tblSoru.Add(sorular);
            db.SaveChanges();
            return RedirectToAction("Egitmen", "Egitmen");
        }
        public ActionResult Sorular(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {
                ViewModel model = new ViewModel();
                tblEgitmen egitmens = new tblEgitmen();
                tblSinav sinavs = new tblSinav();
                var ders = (from o in db.tblDers where o.dersid == id select o).FirstOrDefault();
              
                if (ders.dersid == id)
                {
                    model.tblSorus = (from o in db.tblSoru where o.dersid == id select o).ToList();
                    return View(model);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }

        }
        public ActionResult SoruDetay(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {
                ViewModel model = new ViewModel();
                tblEgitmen egitmens = new tblEgitmen();
                tblSinav sinavs = new tblSinav();
               
                var soru= (from o in db.tblSoru where o.soruid== id select o).FirstOrDefault();
                Session["dogru_cevap"] = soru.dogru_cevap;

                if (soru.soruid == id)
                {
                    model.tblSorus = (from o in db.tblSoru where o.soruid == id select o).ToList();
                    return View(model);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult DersSoruSil(int id)
        {
            var silinecekSoru= db.tblSoru.FirstOrDefault(x => x.soruid == id);

            db.tblSoru.Remove(silinecekSoru);
            db.SaveChanges();


            return RedirectToAction("Egitmen", "Egitmen");
        }
        public ActionResult SinavSoruEkle(int id,int id2)
        {

            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {

                ViewModel model = new ViewModel();
                var sinav = (from k in db.tblSinav where k.sinavid == id select k).FirstOrDefault();
                var ders = (from o in db.tblDers where o.dersid == id select o).FirstOrDefault();
                
                if (ders.dersid == id)
                {
                    model.tblDerss = (from o in db.tblDers where o.dersid == id select o).ToList();
                     model.tblSinavs = (from k in db.tblSinav where k.sinavid == id2 select k).ToList();
                    model.tblSorus = (from k in db.tblSoru where k.dersid== id select k).ToList();
                    return View(model);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        [HttpPost]
        public ActionResult SinavSoruKaydet(int selectsinav,int selectsoru)
        {
            tblSinav sinav= new tblSinav();
            tblSinavSoru sinavsoru = new tblSinavSoru();
            sinavsoru.sinavid =selectsinav;
            sinavsoru.soruid = selectsoru;
            db.tblSinavSoru.Add(sinavsoru);
            db.SaveChanges();
            return RedirectToAction("Egitmen", "Egitmen");
        }
        public ActionResult SinavDetay(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 2)
            {

                ViewModel model = new ViewModel();
                var sinav = (from k in db.tblSinav where k.sinavid == id select k).FirstOrDefault();
                var ders = (from o in db.tblDers where o.dersid == id select o).FirstOrDefault();
                var soru = (from soruid in db.tblSoru  select soruid).FirstOrDefault();
           
                if (ders.dersid == id)
                {
                    model.tblDerss = (from o in db.tblDers where o.dersid == id select o).ToList();
                    model.tblSinavs = (from k in db.tblSinav where k.sinavid == id select k).ToList();
                    model.tblSinavSorus = (from k in db.tblSinavSoru where k.sinavid == id select k).Include(x=>x.tblSoru).ToList();
              
                    return View(model);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult SinavSoruSil(int id)
        {
            var silinecekSinavSoru = db.tblSinavSoru.FirstOrDefault(x => x.sinavSoruid == id);
            db.tblSinavSoru.Remove(silinecekSinavSoru);
            db.SaveChanges();


            return RedirectToAction("Egitmen", "Egitmen");
        }
      
    }
}