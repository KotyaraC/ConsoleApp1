using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void DbAdd(ApplicationContext db)
        {
            Console.WriteLine("Enter user name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter user age:");
            int age = int.Parse(Console.ReadLine());

            User user = new User { Name = name, Age = age };
            db.Users.Add(user);
            db.SaveChanges();
            Console.WriteLine("User added successfully.");
        }

        static void DbSend(ApplicationContext db)
        {
            string separator = "---------------------------------------------";
            var users = db.Users.ToList();
            Console.WriteLine("List of users:");
            foreach (User u in users)
            {
                Console.WriteLine($"Id: {u.Id}\nName: {u.Name}\nAge: {u.Age}\n{separator}");
            }
        }

        static void DbEdit(ApplicationContext db)
        {
            Console.WriteLine("Enter user ID to edit:");
            int userId = int.Parse(Console.ReadLine());
            User user = db.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                Console.WriteLine($"Editing user with ID {userId}:");
                Console.WriteLine($"Current name: {user.Name}, Current age: {user.Age}");

                Console.WriteLine("Enter new name:");
                string newName = Console.ReadLine();
                Console.WriteLine("Enter new age:");
                int newAge = int.Parse(Console.ReadLine());

                user.Name = newName;
                user.Age = newAge;

                db.SaveChanges();
                Console.WriteLine("User information updated successfully.");
            }
            else
            {
                Console.WriteLine($"User with ID {userId} not found.");
            }
        }

        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite("Data Source=help.db");
            var options = optionsBuilder.Options;

            using (var db = new ApplicationContext(options))
            {
                while (true)
                {
                    Console.WriteLine("\t\tMenu\n1. Add to Database\n2. Edit Database\n3. Send Database\n4. Exit");
                    Console.WriteLine("Please enter your choice (1-4):");
                    int choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            DbAdd(db);
                            break;
                        case 2:
                            DbEdit(db);
                            break;
                        case 3:
                            DbSend(db);
                            break;
                        case 4:
                            Console.WriteLine("Exiting the program.");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                            break;
                    }
                }
            }
        }
    }
}
