using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quiz_Class
{

    public class QuizSystem
    {
        public List<Admin> adminUsers;
        public List<Student> studentUsers;
        private List<Quiz> quizzes;
        private List<Category> categories;

        public QuizSystem()
        {
            adminUsers = new List<Admin>();
            studentUsers = new List<Student>();
            quizzes = new List<Quiz>();
            categories = new List<Category>();

        }
        public List<Category> GetCategories()
        {
            return categories;
        }

        public void ShowMainMenu()
        {

            bool exit = false;
            while (!exit)
            {
                // Loads the main menu
                Console.Clear();
                Console.WriteLine("--- Main Menu ---");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Student Login");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                // checks user input
            switch (choice)
                {
                    case "1":
                        var admin = AuthenticateUser(adminUsers.Cast<User>().ToList());
                        if (admin is Admin)
                        {
                            Console.WriteLine("Admin logged in successfully!");
                        }
                        break;
                    case "2":
                        var student = AuthenticateUser(studentUsers.Cast<User>().ToList());
                        if (student is Student)
                        {
                            Console.Clear();
                            PlayQuiz();
                        }
                        else
                        {
                            Console.WriteLine("Wrong username or password. Retry press 1, or 0 to exit.");
                            string choice1 = Console.ReadLine();
                            if (choice1 == "0")
                                exit = true;
                        }
                        break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // Authentication method
        public User AuthenticateUser(List<User> users, string username, string password)
        {
            foreach (User user in users)
            {
                if (user.UserName == username && user.PasswordMatches(password))
                {
                    return user;
                }
            }

            return null;
        }
        public User AuthenticateUser(List<User> users)
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            return AuthenticateUser(users, username, password);
        }




        //Runs the quiz
        public void PlayQuiz()
        {
            Console.Clear();
            Console.WriteLine("=== Available Categories ===");

            // loads all category options
            foreach (Category category in categories)
            {
                Console.WriteLine($"{category.CategoryID}. {category.CategoryName} - {category.CategoryDescription}");
            }

            Console.Write("\nEnter Category ID to view quizzes: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryID))
            {
                Console.WriteLine("Invalid input. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Category selectedCategory = categories.FirstOrDefault(c => c.CategoryID == categoryID);

            if (selectedCategory == null || selectedCategory.Quizzes.Count == 0)
            {
                Console.WriteLine("Category not found or no quizzes available. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine($"=== Quizzes in {selectedCategory.CategoryName} ===");

            foreach (Quiz quiz in selectedCategory.Quizzes)
            {
                Console.WriteLine($"{quiz.QuizID}. {quiz.QuizTitle} - {quiz.QuizDescription}");
            }

            Console.Write("\nEnter Quiz ID to start: ");
            if (!int.TryParse(Console.ReadLine(), out int quizID))
            {
                Console.WriteLine("Invalid Quiz ID. Press any key to return.");
                Console.ReadKey();
                return;
            }

            Quiz selectedQuiz = selectedCategory.Quizzes.FirstOrDefault(q => q.QuizID == quizID);

            if (selectedQuiz == null)
            {
                Console.WriteLine("Quiz not found. Press any key to return.");
                Console.ReadKey();
                return;
            }

            // Starts quiz
            Console.Clear();
            Console.WriteLine($"=== Starting Quiz: {selectedQuiz.QuizTitle} ===\n");

            int score = 0;
            int questionNumber = 1;

            foreach (Question question in selectedQuiz.QuizQuestions)
            {
                Console.WriteLine($"Q{questionNumber}: {question.QuestionText}");

                for (int i = 0; i < question.QuestionOptions.Count; i++)
                    Console.WriteLine($"{i + 1}. {question.QuestionOptions[i]}");

                int answerIndex = 0;
                bool validAnswer = false;

                while (!validAnswer)
                {
                    Console.Write("Your answer (1-4): ");
                    if (int.TryParse(Console.ReadLine(), out answerIndex) &&
                        answerIndex >= 1 && answerIndex <= question.QuestionOptions.Count)
                        validAnswer = true;
                    else
                        Console.WriteLine("Invalid input. Enter a number between 1 and 4.");
                }

                if (question.QuestionOptions[answerIndex - 1] == question.QuestionCorrectAnswer)
                    score++;

                questionNumber++;
                Console.WriteLine();
            }

            Console.WriteLine($"Quiz finished! Your score: {score}/{selectedQuiz.QuizQuestions.Count}");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

        //sample data
        public void LoadSampleData()
        {
            var oopQuestions = new List<Question>
                {
                    new Question(1, "What does OOP stand for?",
                         new List<string> { "Object-Oriented Programming", "Operational Output Processing", "Open Order Protocol", "Overloaded Operator Procedure" },
                         "Object-Oriented Programming",
                         "Easy"),

                    new Question(2, "Which of the following is NOT a core principle of OOP?",
                         new List<string> { "Encapsulation", "Polymorphism", "Abstraction", "Compilation" },
                         "Compilation",
                         "Easy"),

                    new Question(3, "What is encapsulation in object-oriented programming?",
                         new List<string> { "Binding data and methods", "Inheritance", "Overloading", "Creating objects" },
                         "Binding data and methods",
                         "Medium"),

                    new Question(4, "Which keyword is used in C# to inherit a class?",
                         new List<string> { "extends", "inherits", ":", "base" },
                         ":",
                         "Medium"),

                    new Question(5, "What is the purpose of a constructor in a class?",
                         new List<string> { "To destroy objects", "To initialize objects", "To inherit methods", "To override properties" },
                         "To initialize objects",
                         "Easy"),

                    new Question(6, "Which concept allows multiple methods with the same name but different parameters?",
                         new List<string> { "Inheritance", "Polymorphism", "Overloading", "Encapsulation" },
                         "Overloading",
                         "Medium"),

                    new Question(7, "What is the base class for all classes in C#?",
                         new List<string> { "System.Object", "BaseClass", "RootClass", "MainClass" },
                         "System.Object",
                         "Hard"),

                    new Question(8, "What is the difference between a class and an object?",
                         new List<string> { "Class is an instance, object is a blueprint", "Class is a blueprint, object is an instance", "They are the same", "Object inherits class" },
                         "Class is a blueprint, object is an instance",
                         "Medium"),

                    new Question(9, "Which access modifier makes a member accessible only within its own class?",
                         new List<string> { "public", "private", "protected", "internal" },
                         "private",
                         "Easy"),

                    new Question(10, "What is polymorphism in OOP?",
                         new List<string> { "Ability to hide data", "Ability to inherit methods", "Ability to take many forms", "Ability to override constructors" },
                         "Ability to take many forms",
                         "Medium")
                };

            var programmingCategory = new Category(1, "Programming", "Concepts of object-oriented programming and coding principles");
            var dataStructuresCategory = new Category(2, "Data Structures", "Arrays, lists, stacks, queues, trees, and their applications");
            var softwareDesignCategory = new Category(3, "Software Design", "Design patterns, architecture principles, and system modelling");
            var webDevelopmentCategory = new Category(4, "Web Development", "HTML, CSS, JavaScript, and client-server interactions");
            var databaseSystemsCategory = new Category(5, "Database Systems", "SQL queries, relational models, normalization, and transactions");
            var cybersecurityCategory = new Category(6, "Cybersecurity Basics", "Encryption, authentication, and common security threats");
            var computerNetworksCategory = new Category(7, "Computer Networks", "Protocols, IP addressing, routing, and network layers");


            categories = new List<Category>
                {
                    programmingCategory,
                    dataStructuresCategory,
                    softwareDesignCategory,
                    webDevelopmentCategory,
                    databaseSystemsCategory,
                    cybersecurityCategory,
                    computerNetworksCategory
                };

            // Create the quiz
            Quiz oopQuiz = new Quiz(
                1,
                "OOP Fundamentals",
                "Covers basics of object-oriented programming",
                programmingCategory,
                DateTime.Now
            );

            // Add questions to the quiz
            foreach (var question in oopQuestions)
            {
                oopQuiz.AddQuestion(question);
            }

            // Add quiz to category
            programmingCategory.AddQuiz(oopQuiz);

            // Add the category to the list of categories
            categories.Add(programmingCategory);

            // Add sample admin and student users
            adminUsers.Add(new Admin(1, "Admin1", "admin123", "admin@example.com", "Admin"));
            studentUsers.Add(new Student(1, "Student1", "pass123", "student@example.com", "Student", "Active"));
    }



    public void SaveQuizToCSV(Quiz quiz, string filePath)
        {
            if (quiz == null)
            {
                Console.WriteLine("Quiz is null. Cannot save.");
                return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write CSV header
                    writer.WriteLine("QuizID,QuizTitle,QuizDescription,QuestionID,QuestionText,QuestionCorrectAnswer,QuestionOptions,QuestionDifficultyLevel");

                    foreach (var question in quiz.QuizQuestions)
                    {
                        string optionsCombined = string.Join("|", question.QuestionOptions);

                        string quizTitleEscaped = quiz.QuizTitle.Replace("\"", "\"\"");
                        string quizDescEscaped = quiz.QuizDescription.Replace("\"", "\"\"");
                        string questionTextEscaped = question.QuestionText.Replace("\"", "\"\"");
                        string correctAnswerEscaped = question.QuestionCorrectAnswer.Replace("\"", "\"\"");
                        string optionsEscaped = optionsCombined.Replace("\"", "\"\"");

                        writer.WriteLine($"{quiz.QuizID},\"{quizTitleEscaped}\",\"{quizDescEscaped}\",{question.QuestionID},\"{questionTextEscaped}\",\"{correctAnswerEscaped}\",\"{optionsEscaped}\",\"{question.QuestionDifficultyLevel}\"");
                    }
                }

                Console.WriteLine($"Quiz '{quiz.QuizTitle}' saved successfully to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving quiz to CSV: " + ex.Message);
            }
        }

    }
}