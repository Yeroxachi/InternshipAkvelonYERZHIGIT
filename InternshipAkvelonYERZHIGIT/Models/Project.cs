using System;
using System.Collections.Generic;

#nullable disable

namespace InternshipAkvelonYERZHIGIT.Models
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int ProjectId { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Status { get; set; }
        public string Descroiption { get; set; }
        public int Priority { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
