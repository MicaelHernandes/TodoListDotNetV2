namespace Todo.Application.DTOs.Response.Task;

public class CreateTaskResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TotalPomodori { get; set; }
    public int PomodoroValue { get; set; }
    public int CompletedPomodori { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime TaskDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime AssignedAt { get; set; }
}