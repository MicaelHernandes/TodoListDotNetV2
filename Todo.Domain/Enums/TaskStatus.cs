using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Todo.Domain.Enums;

public enum TaskStatus
{
    Pending = 0,
    InProgress = 1,
    Completed = 2,
    Canceled = 3
}