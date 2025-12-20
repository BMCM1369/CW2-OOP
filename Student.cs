using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User_Class
{
    internal class Student : User
    {
        private string status;
        private int score;
        private int highScore;
        private int rank;
        private List<string> completedQuizzes;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public int HighScore
        {
            get { return HighScore; }
            set { HighScore = value; }
        }
        public int Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        public List<string> CompletedQuizzes
        {
            get { return CompletedQuizzes; }
            set { CompletedQuizzes = value; }
        }



        public Student(int userId, string userName, string password, string email, string role) : base(userId, userName, password, email, role)
        {
        }

        public static Student CreateSampleStudent()
        {
            return new Student(
                userId: 1,
                userName: "sampleStudent",
                password: "Password123!",
                email: "student@example.com",
                role: "Student"
            )
            {
                Status = "Active",
                Score = 85,
                HighScore = 95,
                Rank = 3,
                CompletedQuizzes = new List<string>
        {
            "Math Basics",
            "C# Fundamentals",
            "OOP Concepts"
        }
            };
        }

        public static void DisplayStudentMenu(Student student)
        {
            if (student == null)
            {
                Console.WriteLine("No student logged in.");
                return;
            }

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== STUDENT MENU =====");
                Console.WriteLine($"Welcome, {student.UserName}");
                Console.WriteLine();
                Console.WriteLine("1. Update Score");
                Console.WriteLine("2. Submit Quiz");           
                Console.WriteLine("3. Logout");
                Console.WriteLine("========================");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UpdateStudentScore(student);
                        break;

                    case "2":
                        SubmitQuiz(student);
                        break;

                 
                    case "3":
                        exit = true;
                        Console.WriteLine("Logging out...");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
        private static void UpdateStudentScore(Student student)
        {
            Console.Clear();
            Console.WriteLine("===== UPDATE SCORE =====");
            Console.Write("Enter new score: ");

            if (int.TryParse(Console.ReadLine(), out int newScore))
            {
                student.Score = newScore;

                if (newScore > student.HighScore)
                {
                    student.HighScore = newScore;
                    Console.WriteLine("New high score achieved!");
                }

                Console.WriteLine("Score updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid score input.");
            }
        }
        private static void SubmitQuiz(Student student)
        {
            Console.Clear();
            Console.WriteLine("===== SUBMIT QUIZ =====");
            Console.Write("Enter quiz name: ");

            string quizName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(quizName))
            {
                Console.WriteLine("Quiz name cannot be empty.");
                return;
            }

            if (student.CompletedQuizzes.Contains(quizName))
            {
                Console.WriteLine("You have already submitted this quiz.");
                return;
            }

            student.CompletedQuizzes.Add(quizName);

            int quizScore = new Random().Next(10, 21);
            student.Score += quizScore;

            if (student.Score > student.HighScore)
            {
                student.HighScore = student.Score;
            }

            Console.WriteLine($"Quiz '{quizName}' submitted successfully.");
            Console.WriteLine($"You earned {quizScore} points.");
        }















    }
}
