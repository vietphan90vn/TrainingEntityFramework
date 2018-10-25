using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Course = new HashSet<Course>();
        }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int? StandardId { get; set; }
        public int? TeacherType { get; set; }

        public Standard Standard { get; set; }
        public ICollection<Course> Course { get; set; }
    }
}
