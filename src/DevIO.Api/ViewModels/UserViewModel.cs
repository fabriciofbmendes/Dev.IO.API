using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [EmailAddress(ErrorMessage = "O campo {0} esta em formato invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [StringLength(100,ErrorMessage ="O campo {0} precisa ter entre {2} e {1} caracteres",MinimumLength =6)]
        public string Password { get; set; }
        [Compare("Password",ErrorMessage ="As senhas não conferem.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [EmailAddress(ErrorMessage = "O campo {0} esta em formato invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} e obrigatorio")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
