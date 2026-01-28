using TaskBr.Communication.Enums;

namespace TaskBr.Communication.Responses;

public class ResponseShortTasksJson {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PriorityTask Priority { get; set; }
    public StatusTask Status { get; set; }
}
