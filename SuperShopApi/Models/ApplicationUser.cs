using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuperShopApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(80)]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Apelido { get; set; } = string.Empty;

        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "O NIF deve ter 9 dígitos.")]
        public string Nif { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Morada { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string Telefone { get; set; } = string.Empty;
    }
}