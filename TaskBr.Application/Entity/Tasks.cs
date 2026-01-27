using TaskBr.Application.Enums;

namespace TaskBr.Application.Entity;

public class Tasks {
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PriorityTask Priority { get; set; }
    public DateTime DueDate { get; set; }
    public StatusTask Status { get; set; }
}
