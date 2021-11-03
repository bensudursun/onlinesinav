using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using OnlinsinavAPI.Models;
using System.Web.Mvc;

namespace OnlinsinavAPI
{
    public partial class LoginProvider:OAuthAuthorizationServerProvider
    {
        

        public tblKullanici Login(string kullaniciad, string Sifre)
        {
            var db = new OnlinesinavdbEntities1();
            var kullanici = (from k in db.tblKullanici where k.kullaniciad == kullaniciad && k.kullanicisifre == Sifre select k).FirstOrDefault();
            List < tblKullanici > kullanicilar = new List<tblKullanici>();
            if (kullanici!=null )
            {
                var yetki = (from y in db.tblYetki where y.kullaniciid == kullanici.kullaniciid select y).FirstOrDefault();
               
            }
            else
            {
                return null;
            }
            return null;
        }
  
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var kullanici = Login(context.UserName,context.Password);
            if (kullanici == null)
            {
                //hata
            //    context.SetError("invalid_grant", "Hatali ogrenci bilgileri");
                
            }
            else
            {

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("kullaniciad",context.UserName));
                identity.AddClaim(new Claim("Sifre", context.Password));
                identity.AddClaim(new Claim("kullaniciid", kullanici.kullaniciid.ToString()));
                context.Validated(identity);

                //basarili
            }
          
            return base.GrantResourceOwnerCredentials(context);
        }

     
    }
}