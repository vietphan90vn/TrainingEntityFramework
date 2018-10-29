using System;
using System.Linq;
using CodeFirst.Models;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SchoolContext())
			{
				var std = new Student() {Name = "Bill"};

				context.Students.Add(std);
				context.SaveChangesAsync().Wait();

				// Show name
				var studentName = context.Students
									.Select(s => s.StudentId + s.Name)
									.ToList();

				studentName.ForEach(Console.WriteLine);
			}
        }
    }
}
