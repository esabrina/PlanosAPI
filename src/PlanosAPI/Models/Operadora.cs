using System.ComponentModel.DataAnnotations;

namespace PlanosAPI.Models
{
    public class Operadora
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }
    }
}
