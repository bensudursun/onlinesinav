using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlinsinavAPI.Models;

namespace OnlinsinavAPI.Controllers
{
    public class LoginController : Controller
    {
        OnlinesinavdbEntities1 db = new OnlinesinavdbEntities1();
        [AllowAnonymous]

        public ActionResult Giris()
        {
            return View();
        }
        [AllowAnonymous]

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Giris(string kullaniciad, string parola)
        {
            var kullanici = (from k in db.tblKullanici where k.kullaniciad == kullaniciad && k.kullanicisifre == parola select k).FirstOrDefault();
            if (kullanici != null)
            {
                Session["kullaniciad"] = kullanici.kullaniciad;
                Session["kullanicid"] = kullanici.kullaniciid;

                var yetki = (from y in db.tblYetki where y.kullaniciid ==kullanici.kullaniciid select y).FirstOrDefault();
                Session["yetkiid"] = yetki.yetkiturid;
                if (yetki.yetkiturid == 1)
                {

                    FormsAuthentication.SetAuthCookie(kullanici.kullaniciad,false);
                    Session["ogrenciid"] = kullanici.ogrenciid;
                    return RedirectToAction("Ogrenci", "Ogrenci");
                }
                if (yetki.yetkiturid == 5)
                {
                    FormsAuthentication.SetAuthCookie(kullanici.kullaniciad, false);

                    return RedirectToAction("OgrenciIsleri", "OgrenciIsleri");
                }
                if (yetki.yetkiturid == 2)
                {
                    FormsAuthentication.SetAuthCookie(kullanici.kullaniciad, false);
                    Session["egitmenid"] = kullanici.egitmenid;
                    return RedirectToAction("Egitmen", "Egitmen");
                }
            }
            else
            {
                TempData["mesajgiris"] = "Hatalı kulanıcı adı veya şifre. Lütfen tekrar deneyiniz.";
                return View();
            }
            return View();
        }
        public ActionResult Cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Giris", "Login");
        }
    }   
}