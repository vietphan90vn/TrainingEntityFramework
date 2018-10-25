using System;
using System.Collections.Generic;

namespace DatabaseFirst.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentCourse = new HashSet<StudentCourse>();
        }

        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int? StandardId { get; set; }
        public byte[] RowVersion { get; set; }

        public Standard Standard { get; set; }
        public StudentAddress StudentAddress { get; set; }
        public ICollection<StudentCourse> StudentCourse { get; set; }
    }
}
