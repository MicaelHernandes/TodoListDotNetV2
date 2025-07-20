namespace Todo.Application.DTOs.Response.Task;

public class ListTasksResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TotalPomodori { get; set; }
    public int PomodoroValue { get; set; }
    public int CompletedPomodori { get; set; }
    public string Status { get; set; }
    public DateTime TaskDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
}