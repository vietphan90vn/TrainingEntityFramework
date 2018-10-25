using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Standard
    {
        public Standard()
        {
            Student = new HashSet<Student>();
            Teacher = new HashSet<Teacher>();
        }

        public int StandardId { get; set; }
        public string StandardName { get; set; }
        public string Description { get; set; }

        public ICollection<Student> Student { get; set; }
        public ICollection<Teacher> Teacher { get; set; }
    }
}
