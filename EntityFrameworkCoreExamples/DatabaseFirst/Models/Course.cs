using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Course
    {
        public Course()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int? TeacherId { get; set; }

        public Teacher Teacher { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
