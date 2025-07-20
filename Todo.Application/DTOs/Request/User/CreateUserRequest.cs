using System.ComponentModel.DataAnnotations;

namespace Todo.Application.DTOs.Request.User;

public class CreateUserRequest
{
    [Required(ErrorMessage = "O campo nome não pode ser vazio")]
    [MinLength(3, ErrorMessage = "O campo nome deve conter pelo menos 3 caracteres!")]
    [MaxLength(100, ErrorMessage = "O campo nome deve conter no maximo 100 caracteres!")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O campo e-mail é obrigatorio!")]
    [EmailAddress(ErrorMessage = "O campo deve ser um e-mail valido!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O campo password é obrigatorio!")]
    [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres!")]
    [MaxLength(256, ErrorMessage = "A senha deve conter pelo menos 256 caracteres!")]
    public string Password { get; set; }
}