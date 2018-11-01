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

			// Eager loading
			Console.WriteLine("=========Eager loading=========");
			using (var context2 = new SchoolDBContext())
			{
				var student = context2.Student
					//.Include(std => std.StudentAddress)
					.Include(std => std.StudentCourse)
						.ThenInclude(c => c.Course)
					.ToList();

				Console.WriteLine(student);
			}

			//Explicit load
			Console.WriteLine("========Explicit load=========");
			using (var context3 = new SchoolDBContext())
			{
				var student = context3.Student
					.Single(s => s.StudentName.Contains("B"));

				Console.WriteLine(student.StudentName);

				var result = context3.Entry(student)
					.Collection(s => s.StudentCourse)
					.Query()
					.ToList();

				result.ForEach(a => Console.WriteLine(a.Course));

			}
		}
    }
}
