using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlanosAPI.Models
{
    public class DDD
    {
        [Key]
        [Display(Name = "DDD")]
        public string Codigo { get; set; }
        [JsonIgnore]
        public ICollection<PlanoDDD> PlanoDDD { get; set; }

    }
}
