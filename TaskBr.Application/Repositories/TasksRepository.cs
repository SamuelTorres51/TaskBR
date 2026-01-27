using TaskBr.Application.Entity;

namespace TaskBr.Application.Repositories;

public class TasksRepository {
    public class TaskRepository {
        private readonly List<Tasks> _tasks = new();

        public List<Tasks> GetAll() => _tasks;

        public Tasks? GetById(Guid id)
            => _tasks.FirstOrDefault(b => b.Id == id);

        public void Add(Tasks Task)
            => _tasks.Add(Task);

        public void Remove(Tasks Task)
            => _tasks.Remove(Task);

        public bool Exists(Guid id)
            => _tasks.Any(b => b.Id == id);

        public void Update(Tasks Task) {
            var existingTask = GetById(Task.Id);
            if (existingTask != null) {
                existingTask.Name = Task.Name;
                existingTask.Description = Task.Description;
                existingTask.Priority = Task.Priority;
                existingTask.DueDate = Task.DueDate;
                existingTask.Status = Task.Status;
            }
        }
    }

}
