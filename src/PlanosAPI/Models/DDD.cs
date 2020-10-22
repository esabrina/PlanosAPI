using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PlanosAPI.Models
{
    public class DDD
    {
        [Key]
        [Display(Name = "DDD")]
        public string Codigo { get; set; }
        public ICollection<PlanoDDD> PlanoDDD { get; set; }

    }
}
