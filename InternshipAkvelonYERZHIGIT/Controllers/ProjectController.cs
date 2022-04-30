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
        private ProjectsContext _database; 
        public ProjectController(ProjectsContext databaseContext)
        {
            _database = databaseContext;
        }

        [HttpGet]
        [Route("get/id")]
        public Project SelectInfoFromProjectById(int id)
        {
            var takedInfo = _database.Projects.ToList();
            for (int i = 0; i < takedInfo.Count; i++)
            {
                if (takedInfo[i].ProjectId == id)
                {
                    return takedInfo[i];
                }
            }
            return null;
        }
        [HttpGet]
        [Route("get/title")]
        public List<Project> SelectInfoFromProjectByTitle(string title)
        {
            var takedInfo = _database.Projects.ToList();
            var infoForGive = new List<Project>();
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
        public ActionResult AddingInfoToProject([FromBody]ProjectDto dto)
        {
            Project newProject = new Project();
            newProject.Title = dto.Title;
            newProject.Descroiption = dto.Description;
            newProject.StartDate = dto.StartDate;
            newProject.CompletionDate = dto.CompletionDate;
            newProject.Status = dto.Status;
            newProject.Priority = dto.Priority;

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
                var projectFromDb = new Project();
                _database.Remove(_database.Projects.FirstOrDefault(project => project.ProjectId == id));
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

        public List<Project> ViewAllDataFromProjects()
        {
            var takedInfo = _database.Projects.ToList();
            return takedInfo;
        }

        [HttpPost]
        [Route("update/{id:int}")]
        public ActionResult EditInfoFromDb([FromRoute]int id, [FromBody] ProjectDto dto)
        {
            if (_database.Projects.Any(project => project.ProjectId == id))
            {
                Project newProject = _database.Projects.FirstOrDefault(project => project.ProjectId == id);
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
            return Ok("I Fuck this id. No info Mother Father");
        }

        [HttpGet]
        [Route("view-task")]
        public List<Task> ShowAllTasksOfProject(int projectId)
        {
            return  _database.Tasks.Where(task => task.ProjectId == projectId).ToList();
        }
        
    }
}