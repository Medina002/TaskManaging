using System.Collections.Generic;

namespace AuthenticationProject.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<AuthenticationProject.Models.Task> GetAllTasks();
        AuthenticationProject.Models.Task GetTaskById(int id);
        AuthenticationProject.Models.Task GetTaskWithUser(int id); 
        void AddTask(AuthenticationProject.Models.Task task);
        void UpdateTask(AuthenticationProject.Models.Task task);
        void DeleteTask(int id);
    }
}
