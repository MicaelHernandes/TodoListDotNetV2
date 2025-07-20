using System.ComponentModel.DataAnnotations;
using TaskStatus = Todo.Domain.Enums.TaskStatus;

namespace Todo.Application.DTOs.Request.Task;

public class UpdatePartialTaskRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? TotalPomodori { get; set; }
    public int? PomodoroValue { get; set; }
    public int? CompletedPomodori { get; set; }
    public DateTime? TaskDate { get; set; }
    public DateTime? DueDate { get; set; }
    [EnumDataType(typeof(TaskStatus), ErrorMessage = "O status da tarefa deve ser um valor válido.")]
    public TaskStatus? Status { get; set; }
}