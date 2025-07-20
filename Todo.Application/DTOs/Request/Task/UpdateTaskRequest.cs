using System.ComponentModel.DataAnnotations;
using TaskStatus = Todo.Domain.Enums.TaskStatus;

namespace Todo.Application.DTOs.Request.Task;

public class UpdateTaskRequest
{
    [Required(ErrorMessage = "O campo título é obrigatório.")]
    [StringLength(50, ErrorMessage = "O título deve ter no máximo 50 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [MinLength(10, ErrorMessage = "A descrição deve ter no mínimo 10 caracteres.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "O total de pomodoros é obrigatório.")]
    [Range(1, 48, ErrorMessage = "O total de pomodoros deve estar entre 1 e 48.")]
    public int TotalPomodori { get; set; }

    [Required(ErrorMessage = "O valor do pomodoro é obrigatório.")]
    [Range(10, 60, ErrorMessage = "O valor do pomodoro deve estar entre 10 e 60 minutos.")]
    public int PomodoroValue { get; set; }

    [Required(ErrorMessage = "A data da tarefa é obrigatória.")]
    [DataType(DataType.DateTime, ErrorMessage = "A data da tarefa deve estar em um formato válido.")]
    public DateTime TaskDate { get; set; }

    [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
    [DataType(DataType.DateTime, ErrorMessage = "A data de vencimento deve estar em um formato válido.")]
    public DateTime DueDate { get; set; }
    
    [Required(ErrorMessage = "O status da tarefa é obrigatório.")]
    [EnumDataType(typeof(TaskStatus), ErrorMessage = "O status da tarefa deve ser um valor válido.")]
    public TaskStatus Status { get; set; }
}