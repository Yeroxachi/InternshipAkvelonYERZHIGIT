using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using InternshipAkvelonYERZHIGIT.DTO;
using InternshipAkvelonYERZHIGIT.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternshipAkvelonYERZHIGIT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectController : Controller
    {
        private readonly ProjectsContext _database; 
        public ProjectController(ProjectsContext databaseContext)
        {
            _database = databaseContext;
        }

        [HttpGet]
        [Route("get/id")]
        public Project SelectInfoFromProjectById(int id)
        {
            var takedInfo = _database.Projects.ToList();
            return takedInfo.FirstOrDefault(t => t.ProjectId == id);
            //Here we have the first ID filter that works based on the GET route. The result returns the element at the given id.
        }
        [HttpGet]
        [Route("get/title")]
        public List<Project> SelectInfoFromProjectByTitle(string title)
        {
            var takedInfo = _database.Projects.ToList();
            return takedInfo.Where(t => t.Title.Contains(title)).ToList();
            //Here we have a second filter that also works based on the get route and returns data for the specified title.
        }

        [HttpPost]
        [Route("add")]
        public ActionResult AddingInfoToProject([FromBody]ProjectDto dto)
        {
            //This is our method of adding data to the database, which is done based on the Post route. The data is taken from the user through Swagger and written to the database.
            var newProject = new Project
            {
                Title = dto.Title,
                Descroiption = dto.Description,
                StartDate = dto.StartDate,
                CompletionDate = dto.CompletionDate,
                Status = dto.Status,
                Priority = dto.Priority
            };

            _database.Projects.Add(newProject);
            _database.SaveChanges();

            return Ok(newProject.ProjectId);
            
        }
      
        [HttpPost]
        [Route("delete")]
        public ActionResult DeletingInfoFromProject(int id)
        {
            if (_database.Projects.Any(project => project.ProjectId == id))
            {
                //This delete method which uses the Post route which, given the id of the string, removes it from the database.
                var projectFromDb = new Project();
                _database.Remove(_database.Projects.FirstOrDefault(project => project.ProjectId == id));
                _database.SaveChanges();
                return Ok("Deleted");
            }
            else
            {
                return Ok("OMG! Really No info? Fuck!!!");
            }
         
        }
        
        [HttpGet]
        [Route("view")]

        public List<Project> ViewAllDataFromProjects()
        {
            var takedInfo = _database.Projects.ToList();
            return takedInfo;
            //Shows all the information that is in the table.
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public ActionResult EditInfoFromDb([FromRoute]int id, [FromBody] ProjectDto dto)
        {
            //This method allows you to change existing data based on the Post route, which takes the id from the user and change the rows in the table corresponding to this id.
            if (!_database.Projects.Any(project => project.ProjectId == id))
                return Ok("I Fuck this id. No info Mother Father");
            {
                var newProject = _database.Projects.FirstOrDefault(project => project.ProjectId == id);
                newProject.Title = dto.Title;
                newProject.Descroiption = dto.Description;
                newProject.StartDate = dto.StartDate;
                newProject.CompletionDate = dto.CompletionDate;
                newProject.Status = dto.Status;
                newProject.Priority = dto.Priority;

                _database.Update(newProject);
                _database.SaveChanges();
                return Ok("Yeah! Updated, You are cool!");
            }
        }

        [HttpGet]
        [Route("view-task")]
        public List<Task> ShowAllTasksOfProject(int projectId)
        {
            return  _database.Tasks.Where(task => task.ProjectId == projectId).ToList();
        }
        
    }
}