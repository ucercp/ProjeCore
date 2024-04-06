using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjeCore.Models;

namespace ProjeCore.Controllers
{
    public class DefaultController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.Birims.ToList();
            return View(degerler);
        }
        [HttpGet]
        public IActionResult YeniBirim()
        {
            return View();
        }
        [HttpPost]
        public IActionResult YeniBirim(Birim b) 
        {
           
            c.Birims.Add(b);
            c.SaveChanges(); 
            return RedirectToAction("Index"); 

        }
        public IActionResult BirimSil(int id) //id parametresi gonder
        {
            var dep = c.Birims.Find(id); //dep id ye gore gonderdiğim satırın tamamını tutuyor
            c.Birims.Remove(dep); //depten gelen değeri sil.İd ye gore butun satırı sil
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimGetir(int id)
        {
            var depart = c.Birims.Find(id);

            return View("BirimGetir", depart);
        }
        public IActionResult BirimGuncelle(Birim d) // departmanlardan bir tane değer türet
        {
            var dep = c.Birims.Find(d.BirimID); // id ye gore ilgili dep'i bul
            dep.BirimAd = d.BirimAd; // atama yaparak parametreden gelen değerle değiştir.
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult BirimDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.BirimID == id).ToList(); //dısardan gonderdiğimiz id birimid ye esitse bize listelesin
            var brmad = c.Birims.Where(x => x.BirimID == id).Select(y => y.BirimAd).FirstOrDefault();
            ViewBag.brm = brmad;
            return View(degerler);
        }
    }
}