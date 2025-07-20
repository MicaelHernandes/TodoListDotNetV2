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
    
    public void Update(
        string title,
        string description,
        int totalPomodori,
        int pomodoroValue,
        TaskStatus status,
        DateTime taskDate,
        DateTime dueDate)
    {
        Title = title;
        Description = description;
        TotalPomodori = totalPomodori;
        PomodoroValue = pomodoroValue;
        Status = status;
        TaskDate = taskDate;
        DueDate = dueDate;
        if (status == TaskStatus.Completed)
        {
            CompletedAt = DateTime.Now;
        }
        else
        {
            CompletedAt = null;
        }
    }
    
    public void UpdatePartial(
        string? title = null,
        string? description = null,
        int? totalPomodori = null,
        int? pomodoroValue = null,
        int? completedPomodori = null,
        TaskStatus? status = null,
        DateTime? taskDate = null,
        DateTime? dueDate = null)
    {
        if (title != null)
            Title = title;
        if (description != null)
            Description = description;
        if (totalPomodori.HasValue)
            TotalPomodori = totalPomodori.Value;
        if (pomodoroValue.HasValue)
            PomodoroValue = pomodoroValue.Value;
        if (completedPomodori.HasValue)
            CompletedPomodori = completedPomodori.Value;
        if (status.HasValue)
            Status = status.Value;
        if (taskDate.HasValue)
            TaskDate = taskDate.Value;
        if (dueDate.HasValue)
            DueDate = dueDate.Value;
        if (status == TaskStatus.Completed)
            CompletedAt = DateTime.Now;
        else if (status != null)
            CompletedAt = null;
    }
}