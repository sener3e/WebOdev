using EfCore1C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore1C.Controllers
{
  
    public class YazarController : Controller
    {
        KitapLikContext k = new KitapLikContext();

        public IActionResult LinqSorgu()
        {
            var yz = k.Yazarlar.Include(x => x.Kitap).ToList();
            var yz1 = from y in k.Yazarlar
                      where y.Kitap.Count > 0
                      select y;

            var yz2 = (from ktp in k.Kitaplar
                      where ktp.KitapAd.Contains("Ceza")
                      select ktp).FirstOrDefault();
            var yz3 = k.Kitaplar.Where(x => x.KitapAd.Contains("Ceza")).FirstOrDefault();
            var yz4 = from y in k.Yazarlar
                      where y.YazarYas > 40
                      orderby y.YazarSoyad descending
                      select y;
            var ktp2 = from ktp in k.Kitaplar
                       where ktp.Yazar.YazarYas > 40
                       select ktp;

            var ktp3 = k.Kitaplar.Where(bk => bk.Yazar.YazarYas > 40).ToList();

            var yz5 = (from y in k.Yazarlar
                       where y.Kitap.Count > 0
                       select y.YazarYas).Sum();
            var yz6 = (from y in k.Yazarlar
                       join bk in k.Kitaplar
                       on y.YazarID equals bk.YazarID
                       select new
                       {
                           YAd = y.YazarAd + " " + y.YazarSoyad,
                           KAd = bk.KitapAd
                       }).ToList() ;
            return View();
        }

        public IActionResult Index()
        {
            var yazarlar = k.Yazarlar;
            return View(yazarlar);
        }
        public IActionResult YazarOlustur()
        {
            return View();
        }

       [HttpPost]
        public IActionResult YazarDuzenle(int? id, [Bind("YazarAd,YazarSoyad,YazarID")] Yazar y)
        {
            if (id is null)
            {
                TempData["hata"] = "Düzenleme işlemi  başarısız";
                return View("Hata");

            }
            if (id != y.YazarID)
            {
                TempData["hata"] = "Düzenleme işlemi  başarısız";
                return View("Hata");
            }
            if (ModelState.IsValid)
            {
                
                k.Yazarlar.Update(y);
                k.SaveChanges();
                TempData["msj"] = y.YazarAd+ " adlı yazar güncellendi";
                RedirectToAction("Index");

            }
            TempData["hata"] = "Lütfen verileri düzgün giriniz";
            return View("Hata");
        }
        public IActionResult YazarDuzenle(int? id)
        {
            if (id is null)
            {
                TempData["hata"] = "Düzenleme işlemi  başarısız";
                return View("Hata");
            }
            var yz = k.Yazarlar.FirstOrDefault(t => t.YazarID == id);
            if (yz is null)
            {
                TempData["hata"] = "Düzenlenecek yazar bulunamadı";
                return View("Hata");
            }
            return View(yz);
        }
        public IActionResult YazarSil(int? id)
        {
            if (id is null)
            {
                TempData["hata"] = "Silme işlemi  başarısız";
                return View("Hata");
            }
            var yz = k.Yazarlar.Include(x => x.Kitap).FirstOrDefault(x => x.YazarID == id);
            if (yz is null)
            {
                TempData["hata"] = "Silinecek herhangi bir yazar bulunamadı";
                return View("Hata");
            }
            if (yz.Kitap.Count >0)
            {
                TempData["hata"] = "Silme işlemi başarısız. Yazara ait kitaplar var";
                return View("Hata");
            }
           // k.Remove(yz);
            k.Yazarlar.Remove(yz);
            k.SaveChanges();
            TempData["msj"] = yz.YazarAd + " adlı yazar silindi";
            return RedirectToAction("Index");
        }
        public IActionResult YazarDetay(int? id)
        {
            if (id is null)
            {
                TempData["hata"] = "Detay gösterimi başarısız";
                return View("Hata");
            }

            //foreach (var y in k.Yazarlar)
            //{
            //    if (y.YazarID == id)
            //    {
            //        var yz = y;
            //    }
            //}

            var yz = k.Yazarlar.Include(x=>x.Kitap).FirstOrDefault(x => x.YazarID == id);
            if (yz is null)
            {
                TempData["hata"] = "Herhangibir Yazar bulunamadı";
                return View("Hata");
            }
            return View(yz);
        }
        [HttpPost]
        public IActionResult YazarOlustur(Yazar y)
        {
           if (ModelState.IsValid)
            {
                k.Add(y);
                k.SaveChanges();
                TempData["msj"] = y.YazarAd + " Yazarı Eklendi";
                return RedirectToAction("Index");
            }
         
            return NotFound();
        }
    }
}
