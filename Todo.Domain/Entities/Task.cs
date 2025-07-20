using System.ComponentModel.DataAnnotations;
using TaskStatus = Todo.Domain.Enums.TaskStatus;

namespace Todo.Domain.Entities;

public class Task
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int TotalPomodori { get; private set; }
    public int PomodoroValue { get; private set; }
    public int CompletedPomodori { get; private set; }
    public Enums.TaskStatus Status { get; private set; } = TaskStatus.Pending;
    public DateTime TaskDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? AssignedAt { get; private set; } = DateTime.Now;
    public DateTime? CompletedAt { get; private set; }
    
    protected Task() { }
    
    public Task(
        int userId,
        string title,
        string description,
        int totalPomodori,
        int pomodoroValue,
        DateTime taskDate,
        DateTime dueDate)
    {
        UserId = userId;
        Title = title;
        Description = description;
        TotalPomodori = totalPomodori;
        PomodoroValue = pomodoroValue;
        TaskDate = taskDate;
        DueDate = dueDate;
        Status = Enums.TaskStatus.Pending;
        CompletedPomodori = 0;
    }
    
    public void Assign(DateTime assignedAt)
    {
        AssignedAt = assignedAt;
        Status = Enums.TaskStatus.InProgress;
    }

    public void Complete(int completedPomodori, DateTime completedAt)
    {
        CompletedPomodori = completedPomodori;
        CompletedAt = completedAt;
        Status = Enums.TaskStatus.Completed;
    }

    public void Cancel()
    {
        Status = Enums.TaskStatus.Canceled;
    }
}