using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanosAPI.Models
{
    public class Plano
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int Minutos { get; set; }
        public int Franquia { get; set; }
        [MaxLength(2, ErrorMessage = "Limite de dois carateres. Unidades aceitas: MB ou GB.")]
        public string UnidadeFranquia { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public double Valor { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int IdTipoPlano { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public int IdOperadora { get; set; }

        [ForeignKey(nameof(IdTipoPlano))]
        public TipoPlano TipoPlano { get; set; }
        [ForeignKey(nameof(IdOperadora))]
        public Operadora Operadora { get; set; }
        public ICollection<PlanoDDD> PlanoDDD { get; set; }
    }
}
