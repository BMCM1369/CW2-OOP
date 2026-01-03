using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Class
{
    public class Admin: User
    {
        private DateOnly loginDate;

        public DateOnly LoginDate
        {
            get { return loginDate; }
            set { loginDate = value; }
        }
        public Admin (DateOnly loginDate, int userId, string userName, string password, string email, string role) : base(userId, userName, password, email, role)
        {
            this.loginDate = loginDate;
        }

        public Admin(int userId, string userName, string password, string email, string role) : base(userId, userName, password, email, role)
        {
        }

        public static Admin CreateSampleAdmin()
        {
            return new Admin(
                userId: 1,
                userName: "sampleAdmin",
                password: "Password123!",
                email: "Admin@example.com",
                role: "Admin"
            )
            {
                loginDate = DateOnly.FromDateTime(DateTime.Now)
            };
        }
        public static void ManageUsers(List<User> users)
        {
            if (users == null || users.Count == 0)
            {
                Console.WriteLine("No users available.");
                return;
            }

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== MANAGE USERS =====");
                Console.WriteLine("1. View All Users");
                Console.WriteLine("2. Add User");
                Console.WriteLine("3. Remove User");
                Console.WriteLine("4. Update User Role");
                Console.WriteLine("5. Back");
                Console.WriteLine("========================");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice) 
                {
                    case "1":
                        ViewAllUsers(users);
                        break;

                    case "2":
                        AddUser(users);
                        break;

                    case "3":
                        RemoveUser(users);
                        break;

                    case "4":
                        UpdateUserRole(users);
                        break;

                    case "5":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        public static void ViewAllUsers(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("===== USERS =====");

            foreach (var user in users)
            {
                Console.WriteLine(
                    $"ID: {user.UserId} | " +
                    $"Username: {user.UserName} | " +
                    $"Email: {user.Email} | " +
                    $"Role: {user.Role}"
                );
            }
        }
        public static void AddUser(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("===== ADD USER =====");

            Console.Write("Username: ");
            string userName = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Role (Admin/Student): ");
            string role = Console.ReadLine();

            int newId = users.Count > 0 ? users.Max(u => u.UserId) + 1 : 1;

            users.Add(new User(newId, userName, password, email, role));

            Console.WriteLine("User added successfully.");
        }
        public static void RemoveUser(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("===== REMOVE USER =====");
            Console.Write("Enter User ID to remove: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            User user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            users.Remove(user);
            Console.WriteLine("User removed successfully.");
        }
        public static void UpdateUserRole(List<User> users)
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE USER ROLE =====");
            Console.Write("Enter User ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            User user = users.FirstOrDefault(u => u.UserId == id);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.Write($"Current Role: {user.Role}. New Role: ");
            user.Role = Console.ReadLine();

            Console.WriteLine("User role updated successfully.");
        }






    }
}
