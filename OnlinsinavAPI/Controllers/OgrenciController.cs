using OnlinsinavAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace OnlinsinavAPI.Controllers
{
    public class OgrenciController : Controller
    {
        OnlinesinavdbEntities1 db = new OnlinesinavdbEntities1();

        // GET: Ogrenci

        public ActionResult Ogrenci()
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 1)
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
            if (yetkiid == 1)
            {
                ViewModel model = new ViewModel();
                tblOgrenci ogrencis = new tblOgrenci();
                tblSinav sinavs = new tblSinav();
                ogrencis.ogrenciid = id;
                var ogrenci = (from k in db.tblOgrenci where k.ogrenciid == id select k).FirstOrDefault();
                TempData["süre"] = (sinavs.sinavtarih - sinavs.sinavbitis).ToString();
                if (ogrenci.ogrenciid == id)
                {
                    var ogrenciders = (from dersid in db.tblOgrenci where ogrenci.ogrenciid == id select ogrenci.dersid).FirstOrDefault();
                    var sinav = (from o in db.tblSinav where o.dersid == ogrenciders select o).Include(x => x.tblDers).ToList();
                    model.tblSinavs = (from o in db.tblSinav where o.dersid== id select o).ToList();
                    return View(model);
                }
                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
        public ActionResult OgrenciDersListesi(int id)
        {
            int yetkiid = Convert.ToInt32(Session["yetkiid"]);
            if (yetkiid == 1)
            {
                ViewModel model = new ViewModel();
                model.tblBolums = db.tblBolum.ToList();
                tblOgrenci ogrencis = new tblOgrenci();
                ogrencis.ogrenciid = id;
                var ogrenci = (from k in db.tblOgrenci where k.ogrenciid == id select k).FirstOrDefault();
                if (ogrenci.ogrenciid== id)
                {
                    var ogrenciders = (from ogrenciid in db.tblOgrenci where ogrenci.ogrenciid == id select ogrenci.ogrenciid).FirstOrDefault();
                    var ders = (from o in db.tblOgrenciDers where o.ogrenciid == ogrenciders select o).Include(x => x.tblDers).ToList();


                    return View(ders);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Giris", "Login");
            }
        }
    }
}