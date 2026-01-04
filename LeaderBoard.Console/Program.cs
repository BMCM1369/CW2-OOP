using System;
using System.Collections.Generic;
using Quiz_Class;
using User_Class;

namespace LeaderBoardConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Leaderboard Console";

            int quizId = ReadInt("Enter quiz ID: ", 1, 1000);
            var leaderboard = new LeaderBoard(quizId);

            // store students to allow score updates to those already in the leaderboard
            var students = new Dictionary<string, Student>(StringComparer.OrdinalIgnoreCase);

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("===== LEADERBOARD MENU =====");
                Console.WriteLine("1) Add student (or update score)");
                Console.WriteLine("2) Update leaderboard");
                Console.WriteLine("3) Show leaderboard");
                Console.WriteLine("4) Get rank (by username)");
                Console.WriteLine("5) Show Top N users");
                Console.WriteLine("6) Seed sample users (Oisin & Gavin)");
                Console.WriteLine("0) Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddOrUpdateStudent(leaderboard, students);
                        break;

                    case "2":
                        leaderboard.UpdateLeaderboard();
                        Console.WriteLine("Leaderboard updated.");
                        break;

                    case "3":
                        Console.WriteLine();
                        Console.WriteLine(leaderboard.Display());
                        break;

                    case "4":
                        {
                            var username = ReadRequiredString("Enter username: ");
                            var rank = leaderboard.GetRank(username);
                            Console.WriteLine(rank == -1
                                ? $"'{username}' not found on leaderboard."
                                : $"{username} rank: {rank}");
                            break;
                        }

                    case "5":
                        {
                            var n = ReadInt("How many users? (1-50): ", 1, 50);
                            var topUsers = leaderboard.TopUsers(n);

                            if (topUsers == null || topUsers.Count == 0)
                            {
                                Console.WriteLine("No users to display.");
                                break;
                            }

                            Console.WriteLine();
                            Console.WriteLine($"Top {n}:");
                            foreach (var item in topUsers)
                                Console.WriteLine(item); // relies on ToString() in LeaderBoardRank
                            break;
                        }

                    case "6":
                        SeedSampleUsers(leaderboard, students);
                        Console.WriteLine("Seeded Oisin & Gavin. Now choose option 2 then option 3.");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddOrUpdateStudent(LeaderBoard leaderboard, Dictionary<string, Student> students)
        {
            Console.WriteLine();
            var username = ReadRequiredString("Username: ");

            if (!students.TryGetValue(username, out var student))
            {
                int userId = ReadInt("User ID (number): ", 1, 1_000_000);
                string password = ReadRequiredString("Password: ");
                string email = ReadRequiredString("Email: ");
                string role = "Student";

                student = new Student(userId, username, password, email, role)
                {
                    Status = "Active",
                    Score = 0,
                    HighScore = 0,
                    Rank = 0
                    // do NOT call CompletedQuizzes
                };

                students[username] = student;
                leaderboard.Add_User(student);

                Console.WriteLine($"Added '{username}' to leaderboard users.");
            }

            int newScore = ReadInt("Set Score to: ", 0, 1_000_000);
            student.Score = newScore;

            if (newScore > student.HighScore)
                student.HighScore = newScore;

            Console.WriteLine($"Updated {student.UserName}: Score={student.Score}, HighScore={student.HighScore}");
            Console.WriteLine("Now choose option 2 to Update leaderboard (recalculate ranks).");
        }

        static void SeedSampleUsers(LeaderBoard leaderboard, Dictionary<string, Student> students)
        {
            var oisin = new Student(1, "Oisin", "Temp123!", "oisin@example.com", "Student")
            {
                Status = "Active",
                Score = 40,
                HighScore = 40,
                Rank = 0
            };
            students["Oisin"] = oisin;
            leaderboard.Add_User(oisin);

            var gavin = new Student(2, "Gavin", "Temp123!", "gavin@example.com", "Student")
            {
                Status = "Active",
                Score = 70,
                HighScore = 70,
                Rank = 0
            };
            students["Gavin"] = gavin;
            leaderboard.Add_User(gavin);
        }

        static string ReadRequiredString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                    return input.Trim();

                Console.WriteLine("Please enter a value.");
            }
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                var input = Console.ReadLine();

                if (int.TryParse(input, out var val) && val >= min && val <= max)
                    return val;

                Console.WriteLine($"Enter a whole number between {min} and {max}.");
            }
        }
    }
}
