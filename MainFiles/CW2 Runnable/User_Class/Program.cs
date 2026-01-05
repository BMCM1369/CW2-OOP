using System;
using System.Collections.Generic;
using User_Class;

class Program
{
    static void Main()
    {
        // Create sample users
        var users = new List<User>
        {
            Admin.CreateSampleAdmin(),
            Student.CreateSampleStudent()
        };

        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("===== LOGIN =====");
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            // Find matching user
            User user = users.Find(u => u.UserName == username);

            if (user == null || !user.Login(username, password))
            {
                Console.WriteLine("Press any key to try again...");
                Console.ReadKey();
                continue;
            }

            // Redirect based on role
            if (user.Role == "Admin")
            {
                Admin.ManageUsers(users);
            }
            else if (user.Role == "Student")
            {
                Student.DisplayStudentMenu((Student)user);
            }

            user.LogOut();
        }
    }
}
