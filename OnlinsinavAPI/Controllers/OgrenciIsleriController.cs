using OnlinsinavAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace OnlinsinavAPI.Controllers
{
    public class OgrenciIsleriController : Controller
    {
        // GET: OgrenciIsleriAnasayfa
        public ActionResult OgrenciIsleri()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {
                return View();
            }else
            {
                return RedirectToAction("Giris", "Login");
            }
            
        }
        
        OnlinesinavdbEntities1 db = new OnlinesinavdbEntities1();
        public ActionResult OgrenciIsleriEgitmenEkle()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {
               
                ViewModel model = new ViewModel();
                model.tblBolums = db.tblBolum.ToList();
                model.tblFakultes = db.tblFakulte.ToList();
                model.tblDerss = db.tblDers.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
     
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EgitmenKaydet(tblEgitmen tblegitmen, int fakulteler, int bolumler)
        {
            var egitmenad = db.tblEgitmen.FirstOrDefault(x => x.egitmenad == tblegitmen.egitmenad);
            var egitmensoyad = db.tblEgitmen.FirstOrDefault(x => x.egitmensoyad == tblegitmen.egitmensoyad);
            var egitmeneposta = db.tblEgitmen.FirstOrDefault(x => x.egitmeneposta == tblegitmen.egitmeneposta);
            if (egitmenad == null && egitmensoyad == null && egitmeneposta == null )
            {
                if (tblegitmen.egitmenid == 0)
                {
                    tblegitmen.fakulteid = fakulteler;
                    tblegitmen.bolumid = bolumler;

                    if (tblegitmen.bolumid == 1)
                    {
                        tblegitmen.dersid = 1;
                    }
                    if (tblegitmen.bolumid == 2)
                    {
                        tblegitmen.dersid = 6;
                    }
                    if (tblegitmen.bolumid == 3)
                    {
                        tblegitmen.dersid = 13;
                    }
                    if (tblegitmen.bolumid == 4)
                    {
                        tblegitmen.dersid = 12;
                    }
                    if (tblegitmen.bolumid == 6)
                    {
                        tblegitmen.dersid = 2;
                    }
                    if (tblegitmen.bolumid == 9)
                    {
                        tblegitmen.dersid = 7;
                    }
                    db.tblEgitmen.Add(tblegitmen);
                    tblegitmen.yetkiturid = 2;
                }
            }
            else
            {
                TempData["mesajegitmen"] = "Eklemek istediğiniz eğitmen kayıtlıdır.";
                return RedirectToAction("Egitmenler", "OgrenciIsleri");

            }
            db.SaveChanges();
            return RedirectToAction("OgrenciIsleri","OgrenciIsleri");
        }
        public ActionResult OgrenciEkle()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {
                ViewModel model = new ViewModel();
                model.tblBolums = db.tblBolum.ToList();
                model.tblFakultes = db.tblFakulte.Include(x => x.tblBolum).ToList();
                model.tblDerss = db.tblDers.ToList();
                model.tblOgrencis = db.tblOgrenci.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        [HttpPost]
        public JsonResult FakulteBolum( int? fakulteid,string tip)
        {
            //IlilceDBEntities ile db nesnesi oluşturduk.
            OnlinesinavdbEntities1 db = new OnlinesinavdbEntities1();
            //geriye döndüreceğim sonucListim
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "fakulteGetir":
                        //veritabanımızdaki fakülte tablomuzdan fakülteleri sonuc değişkenimze atıyoruz
                        foreach (var fakulte in db.tblFakulte.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = fakulte.fakultead,
                                Value = fakulte.fakulteid.ToString()
                            });
                        }
                        break;
                    case "bolumGetir":
                        //ilcelerimizi getireceğiz fakülte selecten seçilen fakülteid sine göre 
                        foreach (var bolum in db.tblBolum.Where(x => x.fakulteid== fakulteid).ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = bolum.bolumad,
                                Value = bolum.bolumid.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }
            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = basariliMi, text = sonuc });
        }
        [HttpPost]
        public JsonResult YetkiKisi(int? yetkiturid, string tip)
        {
            //IlilceDBEntities ile db nesnesi oluşturduk.
            OnlinesinavdbEntities1 db = new OnlinesinavdbEntities1();
            //geriye döndüreceğim sonucListim
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "yetkiTurGetir":
                        //veritabanımızdaki fakülte tablomuzdan fakülteleri sonuc değişkenimze atıyoruz
                        foreach (var yetki in db.tblYetkiTur.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = yetki.yetkiturad,
                                Value = yetki.yetkiturid.ToString()
                            });
                        }
                        break;
                    case "KisiGetir":
                        if (yetkiturid == 2)
                        {
                            //ilcelerimizi getireceğiz fakülte selecten seçilen fakülteid sine göre 
                            foreach (var egitmen in db.tblEgitmen.Where(x => x.yetkiturid == yetkiturid).ToList())
                            {
                                sonuc.Add(new SelectListItem
                                {
                                    Text = egitmen.egitmenad,
                                    Value = egitmen.egitmenid.ToString()
                                });
                            }
                        }
                        if (yetkiturid == 1)
                        {
                           
                                
                                //ilcelerimizi getireceğiz fakülte selecten seçilen fakülteid sine göre 
                                foreach (var ogrenci in db.tblOgrenci.Where(x => x.yetkiturid == yetkiturid).ToList())
                                {

                                    sonuc.Add(new SelectListItem
                                    {
                                        Text = ogrenci.ogrenciad,
                                        Value = ogrenci.ogrenciid.ToString()
                                    });
                                }

                            
                        }
                        if (yetkiturid == 5)
                        {
                            //ilcelerimizi getireceğiz fakülte selecten seçilen fakülteid sine göre 
                            foreach (var ogrenciisleri in db.tblOgrenciIsleri.Where(x => x.yetkiturid == yetkiturid).ToList())
                            {
                                sonuc.Add(new SelectListItem
                                {
                                    Text = ogrenciisleri.ogrenciisleriad,
                                    Value = ogrenciisleri.ogrenciisleriid.ToString()
                                });
                            }
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }
            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = basariliMi, text = sonuc });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult OgrenciKaydet(tblOgrenci tblogrenci,int fakulteler,int bolumler)
        {
            tblOgrenci ogrenci = new tblOgrenci();
            
            var bolum = (from y in db.tblBolum where y.bolumid==tblogrenci.bolumid select y).FirstOrDefault();
            var ogrenciler = db.tblOgrenci.FirstOrDefault(x => x.ogrencino ==tblogrenci.ogrencino);
            if (ogrenciler == null)
            {

                if (tblogrenci.ogrenciid == 0)
                {
                    tblogrenci.fakulteid = fakulteler;
                    tblogrenci.bolumid = bolumler;

                    if (tblogrenci.bolumid == 1)
                    {
                        tblogrenci.dersid = 1;
                    }
                    if (tblogrenci.bolumid == 2)
                    {
                        tblogrenci.dersid = 6;
                    }
                    if (tblogrenci.bolumid == 3)
                    {
                        tblogrenci.dersid = 13;
                    }
                    if (tblogrenci.bolumid == 4)
                    {
                        tblogrenci.dersid = 12;
                    }
                    if (tblogrenci.bolumid == 6)
                    {
                        tblogrenci.dersid = 2;
                    }
                    if (tblogrenci.bolumid == 9)
                    {
                        tblogrenci.dersid = 7;
                    }
                   
                    tblogrenci.yetkiturid = 1;
                    db.tblOgrenci.Add(tblogrenci);
                }
            }
            else
            {
                TempData["mesajogrenci"] = "Eklemek istediğiniz öğrenci kayıtlıdır.";

                return RedirectToAction("Ogrenciler", "OgrenciIsleri");


            }
            db.SaveChanges();

            return RedirectToAction("OgrenciIsleri", "OgrenciIsleri");
        }
        public ActionResult Kullanicilar()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {
                ViewModel model = new ViewModel();
                model.tblYetkis = db.tblYetki.Include(x => x.tblKullanici).Include(x => x.tblYetkiTur).ToList();
                model.tblKullanicis = db.tblKullanici.Include(x => x.tblYetkiTur).Include(x => x.tblYetki).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult KullaniciEkle()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {
                
                ViewModel model = new ViewModel();
                model.tblKullanicis = db.tblKullanici.Include(x => x.tblEgitmen).ToList();
                model.tblOgrencis = db.tblOgrenci.ToList();
                model.tblYetkiTurs = db.tblYetkiTur.ToList();
                model.tblEgitmens = db.tblEgitmen.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult KullaniciKaydet(string Ad,string Sifre,int yetkitur, int kisiler)
        {
            var kullanicilar = db.tblKullanici.FirstOrDefault(x => x.kullaniciad == Ad);

            tblKullanici kullanici = new tblKullanici();
            tblYetki yetki = new tblYetki();
            if (kullanicilar == null)
            {
                kullanici.kullaniciad = Ad;
                kullanici.kullanicisifre = Sifre;
                kullanici.yetkiturid = yetkitur;
                yetki.yetkiturid = yetkitur;
                if (yetkitur==2)
                {
                    kullanici.egitmenid = kisiler;

                }
           
                if (yetkitur==5)
                {
                    kullanici.ogrenciisleriid = kisiler;

                }
                if (yetkitur==1)
                {
                    kullanici.ogrenciid = kisiler;
                }
                db.tblKullanici.Add(kullanici);
                yetki.kullaniciid =kullanici.kullaniciid;
                yetki.yetkiturid =yetkitur;
                db.tblYetki.Add(yetki);
                db.SaveChanges();
            }
            else
            {
                TempData["mesaj"] = "Eklemek istediğiniz kullanıcı adı kullanılıyor.";
                return RedirectToAction("Kullanicilar", "OgrenciIsleri");
            }
            
            return RedirectToAction("OgrenciIsleri", "OgrenciIsleri");
        }
      
       
     
        public ActionResult Ogrenciler()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {

                ViewModel model = new ViewModel();
                model.tblOgrencis = db.tblOgrenci.Include(x => x.tblFakulte).Include(x=>x.tblBolum).Include(x=>x.tblDers).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult DersListe(int id)
        {
            
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            
            if (yetkiid == 5)
            {

                ViewModel model = new ViewModel();
                model.tblBolums = db.tblBolum.ToList();
                var ogrenci = (from k in db.tblOgrenci where k.ogrenciid == id select k).FirstOrDefault();


                if (ogrenci.ogrenciid ==id)
                {
                    Session["Ogrenciad"] = ogrenci.ogrenciad;
                    Session["Ogrencisoyad"] = ogrenci.ogrencisoyad;
                    var ders = (from o in db.tblOgrenciDers where o.ogrenciid == id select o).Include(x => x.tblDers).ToList();
                        return View(ders);
                }
                else
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult DersListeEgitmen(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {

                ViewModel model = new ViewModel();
                model.tblBolums = db.tblBolum.ToList();
                var egitmen = (from k in db.tblEgitmen where k.egitmenid == id select k).FirstOrDefault();
                Session["egitmenad"] = egitmen.egitmenad;
                Session["egitmensoyad"] = egitmen.egitmensoyad;
                if (egitmen.egitmenid == id)
                {
                    
                    var ders = (from o in db.tblEgitmenDers where o.egitmenid == id select o).Include(x => x.tblDers).ToList();
                    return View(ders);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }

        }
        public ActionResult DersEkle(int id,int id2)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {

                ViewModel model = new ViewModel();
                var ogrenci = (from k in db.tblOgrenci where k.ogrenciid == id2 select k).FirstOrDefault();
                var bolum = (from k in db.tblBolum where k.bolumid == id select k).FirstOrDefault();
                if (bolum.bolumid == id &&  ogrenci.ogrenciid==id2)
                {
                    model.tblOgrencis = (from o in db.tblOgrenci where o.ogrenciid == id2 select o).ToList();
                    model.tblDerss= (from o in db.tblDers where o.bolumid == id select o).ToList();
                    return View(model);
                }
              
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult EgitmenDersEkle(int id,int id2)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {

                ViewModel model = new ViewModel();
                var egitmen = (from k in db.tblEgitmen where k.egitmenid == id2 select k).FirstOrDefault();
                var bolum = (from k in db.tblBolum where k.bolumid == id select k).FirstOrDefault();
                if (bolum.bolumid == id &&  egitmen.egitmenid == id2)
                {
                    model.tblEgitmens = (from o in db.tblEgitmen where o.egitmenid == id2 select o).ToList();
                    model.tblDerss = (from o in db.tblDers where o.bolumid == id select o).ToList();
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
        [ValidateAntiForgeryToken]
        public ActionResult EgitmenDersKaydet(int selectders, int selectegitmen)
        {

            tblEgitmen egitmenler = new tblEgitmen();
            ViewModel model = new ViewModel();

            model.tblEgitmenDers = db.tblEgitmenDers.Include(x => x.tblDers).Include(x => x.tblEgitmen).ToList();
            var ders = db.tblEgitmenDers.FirstOrDefault(x => x.dersid == selectders);
            var egitmen = db.tblEgitmenDers.FirstOrDefault(x => x.egitmenid == selectegitmen);
            var egitmensders = (from dersid in db.tblEgitmenDers where dersid.dersid == selectders select dersid).FirstOrDefault();
            tblEgitmenDers egitmenders = new tblEgitmenDers();
            egitmenders.dersid = selectders;
            egitmenders.egitmenid = selectegitmen;
            if (ders == null && egitmen == null)
            {
                db.tblEgitmenDers.Add(egitmenders);
                db.SaveChanges();
            }
            else
            {
                TempData["mesaj"] = "Eklemek istediğiniz ders eğitmene kayıtlıdır.";
                return RedirectToAction("Egitmenler", "OgrenciIsleri");

            }
            return RedirectToAction("OgrenciIsleri", "OgrenciIsleri");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DersKaydet(int selectders,int selectogrenci)
        {

            tblOgrenci ogrenciler = new tblOgrenci();
            ViewModel model = new ViewModel();
            
            model.tblOgrenciDers = db.tblOgrenciDers.Include(x => x.tblDers).Include(x => x.tblOgrenci).ToList();
            var ders = db.tblOgrenciDers.FirstOrDefault(x => x.dersid==selectders);
            var ogrenci = db.tblOgrenciDers.FirstOrDefault(x => x.ogrenciid == selectogrenci);
            tblOgrenciDers ogrenciders = new tblOgrenciDers();
            if (ogrenci ==null || ders==null)
                {
                

                    ogrenciders.dersid = selectders;
                    ogrenciders.ogrenciid = selectogrenci;
                    db.tblOgrenciDers.Add(ogrenciders);
                    db.SaveChanges();
                }
                else
                {
                    TempData["mesaj"] = "Eklemek istediğiniz ders öğrenciye kayıtlıdır.";
                    return RedirectToAction("Ogrenciler", "OgrenciIsleri");

                }
            
            return RedirectToAction("OgrenciIsleri", "OgrenciIsleri");
        }
        public ActionResult OgrenciSil(int id)
        {
            var silinecekOgrenci = db.tblOgrenciDers.FirstOrDefault(x => x.ogrenciid == id);
            if (silinecekOgrenci == null)
            {
                var silinecekogr = db.tblOgrenci.FirstOrDefault(x => x.ogrenciid == id);
                db.tblOgrenci.Remove(silinecekogr);
                db.SaveChanges();
            }else
            {
              
                return View();
              
            }
            return RedirectToAction("Ogrenciler", "OgrenciIsleri");
        }
        public ActionResult OgrenciDersSil(int id)
        {
            var silinecekOgrenciDers = db.tblOgrenciDers.FirstOrDefault(x => x.ogrenciDersID == id);
           
             
                var silinecekDerss = db.tblOgrenciDers.FirstOrDefault(x => x.ogrenciDersID == id);
                db.tblOgrenciDers.Remove(silinecekDerss);
                db.SaveChanges();
            
           
            return RedirectToAction("Ogrenciler", "OgrenciIsleri");
        }
        public ActionResult EgitmenDersSil(int id)
        {
            
            var silinecekDerss = db.tblEgitmenDers.FirstOrDefault(x => x.egitmenDersid == id);
            db.tblEgitmenDers.Remove(silinecekDerss);
            db.SaveChanges();


            return RedirectToAction("Egitmenler", "OgrenciIsleri");
        }
        public ActionResult EgitmenSil(int id)
        {
            var silinecekEgitmen = db.tblEgitmenDers.FirstOrDefault(x => x.egitmenid == id);
            if (silinecekEgitmen == null)
            {
                var silinecekegt = db.tblEgitmen.FirstOrDefault(x => x.egitmenid == id);
                db.tblEgitmen.Remove(silinecekegt);
                db.SaveChanges();
            }
            else
            {

                return View();

            }
            return RedirectToAction("Egitmenler", "OgrenciIsleri");
        }
        public ActionResult KullaniciSil(int  id)
        {
            var silinecekKullanici= db.tblYetki.FirstOrDefault(x => x.kullaniciid == id);
            if (silinecekKullanici == null)
            {
                var silinecekkullnc = db.tblKullanici.FirstOrDefault(x => x.kullaniciid == id);
                db.tblKullanici.Remove(silinecekkullnc);
                db.SaveChanges();
            }
            else
            {
                var silinecekyetki = db.tblYetki.FirstOrDefault(x => x.kullaniciid == id);
                db.tblYetki.Remove(silinecekyetki);
                db.SaveChanges();
                var silinecekkullnc = db.tblKullanici.FirstOrDefault(x => x.kullaniciid == id);
                db.tblKullanici.Remove(silinecekkullnc);
                db.SaveChanges();
                

            }
            return RedirectToAction("Kullanicilar", "OgrenciIsleri");
        }
        public ActionResult Egitmenler()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 5)
            {
                ViewModel model = new ViewModel();

                model.tblEgitmens= db.tblEgitmen.Include(x => x.tblFakulte).Include(x => x.tblBolum).ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
    }
}