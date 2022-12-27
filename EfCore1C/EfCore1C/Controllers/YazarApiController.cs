using EfCore1C.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EfCore1C.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YazarApiController : ControllerBase
    {
        KitapLikContext k = new KitapLikContext();
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<List<Yazar>>> Get()
        {
            var y = await k.Yazarlar.ToListAsync();
            if (y is null)
            {
                return NoContent();
            }
            return y;

        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Yazar>> Get(int id)
        {
            var y = await k.Yazarlar.FirstOrDefaultAsync(x=>x.YazarID==id);
            if (y is null)
            {
                return NoContent();
            }
            return y;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Yazar y)
        {
            k.Yazarlar.Add(y);
            k.SaveChanges();
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Yazar y)
        {
            var y1 = k.Yazarlar.FirstOrDefault(x => x.YazarID == id);
            if (y1 is null)
            {
                return NotFound();
            }
            y1.YazarAd = y.YazarAd;
            y1.YazarSoyad = y.YazarSoyad;
            y1.YazarYas = y.YazarYas;
            k.Update(y1);
            k.SaveChanges();
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var y1 = k.Yazarlar.FirstOrDefault(x => x.YazarID == id);
            if (y1 is null)
            {
                return NotFound();
            }
            if (k.Kitaplar.Any(x => x.YazarID == id))
            {
                return NotFound("Yazara ait Kitaplar var");
            }
            k.Yazarlar.Remove(y1);
            k.SaveChanges();
            return Ok();
        }
    }
}
