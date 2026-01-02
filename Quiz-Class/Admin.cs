using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Class
{
    public class Admin : User // placeholder for dependencies
    {
        private DateTime loginDate;
        public Admin(int id, string userName, string password, string email, string role)
            : base(id, userName, password, email, role)
        {
            this.loginDate = loginDate;
        }

        public static List<Admin> CreateSampleAdmins()
        {
            return new List<Admin>
            {
            new Admin(1, "admin1", "adminpass", "admin01@example.com", "Admin"),
            new Admin(2, "admin2", "adminpass", "admin02@example.com", "Admin")
            };
        }
        public void AddCategory(List<Category> categories, Category newCategory)
        {
            categories.Add(newCategory);
            Console.WriteLine($"Category added");
        }
        public void RemoveCategory(List<Category> categories, int categoryid)
        {
            Category categoriestoremove = categories.FirstOrDefault(c => c.CategoryID == categoryid);
            if (categories[categoryid] != null)
            {
                categories.RemoveAt(categoryid);
                Console.WriteLine($"Category number {categoryid} was removed.");
            }
            else
            {
                Console.WriteLine($"Category number {categoryid} not found.");
            }
        }
        public void ManageUsers()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("--- Manage Users Menu ---");
                Console.WriteLine("1. View all students");
                Console.WriteLine("2. Update student status");
                Console.WriteLine("3. Remove student");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Select an option");
                String option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine("");
                        break;
                    case "3":
                        Console.WriteLine("");
                        break;
                    case "4":
                        Console.WriteLine("");
                        break;
                    default:
                        Console.WriteLine("");
                        break;
                }

            }
        }
    }
}
