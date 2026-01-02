using System;

namespace Quiz_Class
{
    public class Student : User // placeholder for dependencies
    {
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public Student(int id, string userName, string password, string email, string role, string status)
            : base(id, userName, password, email, role)
        {
            this.status = status;
        }

        public static Student CreateSampleStudent()
        {
            return new Student(1, "Student1", "pass123", "Student01@example.com", "Student", "Active");
        }

        public void DisplayStudentMenu()
        {
            Console.WriteLine("== Student Menu ==");
            Console.WriteLine("1. Play Quiz");
            Console.WriteLine("2. Update Profile");
            Console.WriteLine("3. Logout");
        }
    }
}
