using System.ComponentModel.DataAnnotations;

namespace Todo.Application.DTOs.Request.User;

public class CreateUserRequest
{
    [Required(ErrorMessage = "O campo nome não pode ser vazio")]
    public string Name { get; set; }
    [Required(ErrorMessage = "O campo email é obrigatório!")]
    [EmailAddress(ErrorMessage = "É necessário ser um email valido!")]
    public string Email { get; set; }
    [Required(ErrorMessage = "O campo password obrigatório!")]
    public string Password { get; set; }
}