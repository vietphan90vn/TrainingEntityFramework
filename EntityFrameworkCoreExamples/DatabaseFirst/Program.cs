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

			// This allows you to do things such as running an aggregate operator over the
			// related entities without loading them into memory.
			using (var context3 = new SchoolDBContext())
			{
				var student = context3.Student
					.Single(s => s.StudentName.Contains("B"));

				var result = context3.Entry(student)
					.Reference(s => s.Standard)
					.Query()
					.FirstOrDefault();

				Console.WriteLine(result.StandardName);
			}

			// You can also filter which related entities are loaded into memory.
			using (var context3 = new SchoolDBContext())
			{
				var student = context3.Student
					.Single(s => s.StudentName.Contains("B"));

				var result = context3.Entry(student)
					.Reference(s => s.StudentAddress)
					.Query()
					.Select(address => address.City)
					.ToList();

				Console.WriteLine(result.FirstOrDefault());
			}

			// Lazy load: support EF Core 2.1 or higher

			// Add - remove student
			using (var context3 = new SchoolDBContext())
			{
				// Add student
				//var std = new Student() {StudentName = "New name"};
				// context3.Student.
				// context3.Student.Add(std);
				// context3.SaveChangesAsync().Wait();

				// Remove student
				var stds = context3.Student.Where(s => s.StudentName.Equals("New name"));
				context3.RemoveRange(stds);
				context3.SaveChangesAsync().Wait();

				// Show students
				context3.Student
					.Select(s => string.Concat(s.StudentId, ":", s.StudentName))
					.ToList()
					.ForEach(Console.WriteLine);
			}
		}
    }
}
