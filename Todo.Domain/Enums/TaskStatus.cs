using System.Runtime.Serialization;

namespace Todo.Domain.Enums;

public enum TaskStatus
{
    [EnumMember(Value = "pending")]
    Pending = 0,

    [EnumMember(Value = "in_progress")]
    InProgress = 1,

    [EnumMember(Value = "completed")]
    Completed = 2,

    [EnumMember(Value = "canceled")]
    Canceled = 3
}