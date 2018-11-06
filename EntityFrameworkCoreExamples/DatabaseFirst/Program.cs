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

				Console.WriteLine(student.Count);
			}

			//Explicit load
			Console.WriteLine("========Explicit load=========");

			// This allows you to do things such as running an aggregate operator over the
			// related entities without loading them into memory.
			using (var context3 = new SchoolDBContext())
			{
				var student = context3.Student
					.Single(s => s.StudentName.Contains("B"));

				// Load single navigation property
				context3.Entry(student)
					.Reference(s => s.Standard)
					.Load();

				// Load collection navigation property
				context3.Entry(student)
					.Collection(s => s.StudentCourse)
					.Load();

				Console.WriteLine(student.Standard.StandardName);
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

			SavingData.AddingData();
			SavingData.UpdatingData();
			SavingData.DeletingData();

		}
    }
}
