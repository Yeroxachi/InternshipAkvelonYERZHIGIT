using System.Linq;
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
        
        public Task SelectInfoFromTask(int id)
        {
            var list = _database.Tasks.ToList();
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].TaskId == id)
                {
                    return list[i];
                }
            }
            return null;
        } 
    }
}