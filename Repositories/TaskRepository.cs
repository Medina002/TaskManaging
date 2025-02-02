using AuthenticationProject.Data;
using AuthenticationProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AuthenticationProject.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AuthenticationProjectDbContext _context;

        public TaskRepository(AuthenticationProjectDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AuthenticationProject.Models.Task> GetAllTasks()
        {
            return _context.Tasks.Include(t => t.User).ToList();
        }

        public AuthenticationProject.Models.Task GetTaskById(int id)
        {
            return _context.Tasks.Find(id);
        }

        public AuthenticationProject.Models.Task GetTaskWithUser(int id)
        {
            return _context.Tasks
                .Include(t => t.User)
                .FirstOrDefault(t => t.Id == id);
        }

        public void AddTask(AuthenticationProject.Models.Task task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
        }

        public void UpdateTask(AuthenticationProject.Models.Task task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }
        }
    }
}
