using System;
using System.Collections.Generic;

#nullable disable

namespace InternshipAkvelonYERZHIGIT.Models
{
    public sealed partial class Task
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }

        public Project Project { get; set; }
    }
}
