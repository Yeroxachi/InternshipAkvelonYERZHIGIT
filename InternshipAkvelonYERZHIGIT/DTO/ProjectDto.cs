using System;

namespace InternshipAkvelonYERZHIGIT.DTO
{
    public class ProjectDto
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}