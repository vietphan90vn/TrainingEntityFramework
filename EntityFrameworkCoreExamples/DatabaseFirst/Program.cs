using DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
			var context = new SchoolDBContext();
			var studentNames = context.Student
				.Select(s => s.StudentName)
				.ToList();

			var studentWithRowVersion = context.Student
				.Where(s => s.StudentName.Contains("B"))
				.Include(s => s.StudentAddress)
				.ToList();

			studentNames.ForEach(Console.WriteLine);
			var course = studentWithRowVersion.SelectMany(s => s.RowVersion).ToList();
			course.ForEach(b => Console.WriteLine(b));
		}
    }
}