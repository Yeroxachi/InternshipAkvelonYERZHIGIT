using System.Collections.Generic;
using System.Linq;
using InternshipAkvelonYERZHIGIT.DTO;
using InternshipAkvelonYERZHIGIT.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAkvelonYERZHIGIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class TaskController : Controller
    {
        private ProjectsContext _database; 
        public TaskController(ProjectsContext databaseContext)
        {
            _database = databaseContext;
        }
        
        [HttpGet]
        [Route("get/id")]
        public Task SelectInfoFromTaskById(int id)
        {
            var takedInfo = _database.Tasks.ToList();
            for (int i = 0; i < takedInfo.Count; i++)
            {
                if (takedInfo[i].TaskId == id)
                {
                    return takedInfo[i];
                }
            }
            return null;
        }
        [HttpGet]
        [Route("get/title")]
        public List<Task> SelectInfoFromTaskByTitle(string title)
        {
            var takedInfo = _database.Tasks.ToList();
            var infoForGive = new List<Task>();
            for (int i = 0; i < takedInfo.Count; i++)
            {
                if (takedInfo[i].Title.Contains(title))
                {
                    infoForGive.Add(takedInfo[i]);
                }
            }
            return infoForGive;
        }

        [HttpPost]
        [Route("add")]
        public ActionResult AddingInfoToTask([FromBody]TaskDto dto)
        {
            Task newTask = new Task();
            newTask.Title = dto.Title;
            newTask.Description = dto.Description;
            newTask.ProjectId = dto.ProjectId;
            newTask.Status = dto.Status;
            newTask.Priority = dto.Priority;

            _database.Tasks.Add(newTask);
            _database.SaveChanges();

            return Ok(newTask.TaskId);
        }
      
        [HttpPost]
        [Route("delete")]
        public ActionResult DeletingInfoFromTask(int id)
        {
            if (_database.Tasks.Any(task => task.TaskId == id))
            {
                var taskFromDb = new Task();
                _database.Remove(_database.Tasks.FirstOrDefault(task => task.TaskId == id));
                _database.SaveChanges();
                return Ok("Deleted");
            }
            else
            {
                return Ok("OMG! Really No info? Fuck!!!!");
            }
         
        }

        [HttpGet]
        [Route("view")]

        public List<Task> viewAllDataFromTask()
        {
            var takedInfo = _database.Tasks.ToList();
            return takedInfo;
        }
        
        [HttpPost]
        [Route("update/{id:int}")]
        public ActionResult EditInfoFromDbTasks([FromRoute]int id, [FromBody] TaskDto dto)
        {
            if (_database.Tasks.Any(task => task.TaskId == id))
            {
                Task newTask = _database.Tasks.FirstOrDefault(task => task.TaskId == id);
                newTask.Title = dto.Title;
                newTask.Description = dto.Description;
                newTask.ProjectId = dto.ProjectId;
                newTask.Status = dto.Status;
                newTask.Priority = dto.Priority;

                _database.Update(newTask);
                _database.SaveChanges();
                return Ok("Yeah! Updated, You are cool!");
            }
            return Ok("I Fuck this id. No info Mother Father");
        }
    }
}