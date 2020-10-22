using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlanosAPI.Models
{
    public class PlanoDDD
    {
        [Key]
        public string CodigoDDD { get; set; }
        [Key]
        public int IdPlano { get; set; }
        [JsonIgnore]
        public DDD DDD { get; set; }
        [JsonIgnore]
        public Plano Plano { get; set; }
    }
}
