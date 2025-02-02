using AuthenticationProject.Models;  
using AuthenticationProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenticationProject.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        
        public IActionResult Index()
        {
            var tasks = _taskRepository.GetAllTasks();
            return View(tasks);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AuthenticationProject.Models.Task task)
        {
            if (!ModelState.IsValid)
            {
                return View(task);
            }

            task.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            _taskRepository.AddTask(task);

            return RedirectToAction("Index", "Task"); 
        }


        
        public IActionResult Edit(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound(); 
            }
            return View(task);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AuthenticationProject.Models.Task task) 
        {
            if (id != task.Id) 
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _taskRepository.UpdateTask(task);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                   
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }
            return View(task); 
        }

        
        public IActionResult Delete(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound(); 
            }
            return View(task);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var task = _taskRepository.GetTaskById(id);
            if (task == null)
            {
                return NotFound(); 
            }

            try
            {
                _taskRepository.DeleteTask(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                
                ModelState.AddModelError("", "Unable to delete the task. Please try again.");
                return View("Delete", task); 
            }
        }

        
        public IActionResult Details(int id)
        {
            var task = _taskRepository.GetTaskWithUser(id);
            if (task == null)
            {
                return NotFound(); 
            }
            return View(task);
        }
    }


}
