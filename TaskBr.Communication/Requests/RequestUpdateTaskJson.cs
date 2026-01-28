using TaskBr.Communication.Enums;

namespace TaskBr.Communication.Requests;

public class RequestUpdateTaskJson {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public PriorityTask Priority { get; set; }
    public DateTime DueDate { get; set; }
    public StatusTask Status { get; set; }
}
