using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore1C.Models
{
    public class Kitap
    {
        public int KitapID { get; set; }
        [Required(ErrorMessage = "Kitap adı boş geçilemez")]
        [MaxLength(50)]
        [Display(Name = "Kitap Ad")]
        public string KitapAd { get; set; }

        public int YazarID { get; set; }
        public Yazar Yazar { get; set; }
    }
}
