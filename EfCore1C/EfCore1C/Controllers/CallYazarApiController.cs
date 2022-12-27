using EfCore1C.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EfCore1C.Controllers
{
    public class CallYazarApiController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Yazar> Yazarlar = new List<Yazar>();
            var hhtc = new HttpClient();
            var response = await hhtc.GetAsync("https://localhost:44375/api/YazarApi");
            string resString = await response.Content.ReadAsStringAsync();
            Yazarlar = JsonConvert.DeserializeObject<List<Yazar>>(resString);
            return View(Yazarlar);
        }
    }
}
