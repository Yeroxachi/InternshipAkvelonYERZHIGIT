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
        private readonly ProjectsContext _database; 
        public TaskController(ProjectsContext databaseContext)
        {
            _database = databaseContext;
        }
        
        [HttpGet]
        [Route("get/id")]
        public Task SelectInfoFromTaskById(int id)
        {
            var takedInfo = _database.Tasks.ToList();
            return takedInfo.FirstOrDefault(t => t.TaskId == id);
            //Here we have the first ID filter that works based on the GET route. The result returns the element at the given id.
        }
        [HttpGet]
        [Route("get/title")]
        public List<Task> SelectInfoFromTaskByTitle(string title)
        {
            var takedInfo = _database.Tasks.ToList();
            return takedInfo.Where(t => t.Title.Contains(title)).ToList();
        }
        //Here we have a second filter that also works based on the get route and returns data for the specified title.
        [HttpPost]
        [Route("add")]
        public ActionResult AddingInfoToTask([FromBody]TaskDto dto) //This is our method of adding data to the database, which is done based on the Post route. The data is taken from the user through Swagger and written to the database.
        {
            var newTask = new Task
            {
                Title = dto.Title,
                Description = dto.Description,
                ProjectId = dto.ProjectId,
                Status = dto.Status,
                Priority = dto.Priority
            };

            _database.Tasks.Add(newTask);
            _database.SaveChanges();

            return Ok(newTask.TaskId);
        }
      
        [HttpPost]
        [Route("delete")]
        public ActionResult DeletingInfoFromTask(int id)
        {
            //This delete method which uses the Post route which, given the id of the string, removes it from the database.
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
            //Shows all the information that is in the table.
            var takedInfo = _database.Tasks.ToList();
            return takedInfo;
        }
        
        [HttpPost]
        [Route("update/{id:int}")]
        public ActionResult EditInfoFromDbTasks([FromRoute]int id, [FromBody] TaskDto dto)
        {
            ////This method allows you to change existing data based on the Post route, which takes the id from the user and change the rows in the table corresponding to this id.
            if (!_database.Tasks.Any(task => task.TaskId == id)) return Ok("I Fuck this id. No info Mother Father");
            {
                var newTask = _database.Tasks.FirstOrDefault(task => task.TaskId == id);
                newTask.Title = dto.Title;
                newTask.Description = dto.Description;
                newTask.ProjectId = dto.ProjectId;
                newTask.Status = dto.Status;
                newTask.Priority = dto.Priority;

                _database.Update(newTask);
                _database.SaveChanges();
                return Ok("Yeah! Updated, You are cool!!!");
            }
        }
    }
}