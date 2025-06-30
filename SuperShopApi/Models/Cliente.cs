using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperShopApi.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength(80)]
        public string Apelido { get; set; }

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O NIF deve ter 9 dígitos.")]
        public string Nif { get; set; }

        [Required]
        [StringLength(200)]
        public string Morada { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string Telefone { get; set; }
    }
}