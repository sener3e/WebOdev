using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore1C.Models
{
    public class Yazar
    {
        public int YazarID { get; set; }
        [Required(ErrorMessage ="Ad alanı boş geçilemez")]
        [MaxLength(50)]
        [Display(Name ="Yazar Ad")]
        public string YazarAd { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş geçilemez")]
        [MaxLength(50)]
        [Display(Name = "Yazar Soyad")]
        public  string YazarSoyad{ get; set; }
        public int YazarYas { get; set; }

        public  ICollection<Kitap>Kitap { get; set; }
    }
}
