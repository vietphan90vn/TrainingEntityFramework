using System;
using System.Linq;
using DatabaseFirst.Models;

namespace DatabaseFirst
{
    public static class SavingData
    {
		public static void AddingData()
		{
			Console.WriteLine("======== AddingData ========");
			// Add - remove student
			using (var context = new SchoolDBContext())
			{
				//Add student
				var std = new Student() {StudentName = "New name"};
				context.Student.Add(std);
				context.SaveChangesAsync().Wait();

				// Show students
				context.Student
					.Select(s => string.Concat(s.StudentId, ":", s.StudentName))
					.ToList()
					.ForEach(Console.WriteLine);
			}
		}
		public static void UpdatingData()
		{
			Console.WriteLine("======== UpdatingData ========");
			using (var context = new SchoolDBContext())
			{
				// Remove student
				var std = context.Student.Single(s => s.StudentName.Equals("New name"));
				std.StudentName = "new Name update";
				context.SaveChangesAsync().Wait();

				// Show students
				context.Student
					.Select(s => string.Concat(s.StudentId, ":", s.StudentName))
					.ToList()
					.ForEach(Console.WriteLine);
			}
		}

		public static void DeletingData()
		{
			Console.WriteLine("======== DeletingData ========");
			using (var context = new SchoolDBContext())
			{
				// Remove student
				var std = context.Student.Where(s => s.StudentName.Equals("new Name update"));
				context.RemoveRange(std);
				context.SaveChangesAsync().Wait();

				// Show students
				context.Student
					.Select(s => string.Concat(s.StudentId, ":", s.StudentName))
					.ToList()
					.ForEach(Console.WriteLine);
			}
		}
    }
}
