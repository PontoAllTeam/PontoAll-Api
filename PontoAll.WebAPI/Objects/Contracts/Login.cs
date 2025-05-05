using System.ComponentModel.DataAnnotations;

namespace PontoAll.WebAPI.Objects.Contracts;

public class Login
{
    [Required(ErrorMessage = "O e-mail � requerido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha � requerida!")]
    public string Password { get; set; }
}