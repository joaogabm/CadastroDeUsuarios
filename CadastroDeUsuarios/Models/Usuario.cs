using System.ComponentModel.DataAnnotations;

namespace CadastroDeUsuarios.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Digite um nome!")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "Digite um sobrenome!")]
        public string Sobrenome { get; set; }


        [Required(ErrorMessage = "Digite um e-mail!"),
            EmailAddress(ErrorMessage = "E-mail inválido!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Digite um cargo!")]
        public string Cargo { get; set; }
    }
}
