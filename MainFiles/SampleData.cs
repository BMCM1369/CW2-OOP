using System;
using System.Collections.Generic;

namespace Quiz_Class
{
    public static class SampleData
    {
        // Creates sample categories and quizzes, and assigns quizzes to categories by QuizID
        public static List<Category> CreateSampleCategories()
        {
            // Define categories (unique by name)
            var categoriesByName = new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase)
            {
                { "Programming Concepts", new Category(100, "Programming Concepts", "Core programming fundamentals and concepts.") },
                { "Programming", new Category(101, "Programming", "General programming topics and practices.") },
                { "Software Design", new Category(102, "Software Design", "Design patterns, architecture principles, and system modeling.") },
                { "Web Development", new Category(103, "Web Development", "HTML, CSS, JavaScript, and client-server interactions.") },
                { "Database Systems", new Category(104, "Database Systems", "SQL, relational models, normalization, and transactions.") },
                { "Cybersecurity Basics", new Category(105, "Cybersecurity Basics", "Encryption, authentication, and common security threats.") },
                { "Computer Networks", new Category(106, "Computer Networks", "Protocols, IP addressing, routing, and network layers.") }
            };

            // Define sample quizzes (without questions by default)
            var quizzes = new List<Quiz>
            {
                new Quiz(
                    id: 1,
                    title: "OOP Fundamentals",
                    description: "Covers basics of object-oriented programming",
                    category: categoriesByName["Programming Concepts"],
                    date: new DateTime(2025, 9, 1)
                ),
                new Quiz(
                    id: 2,
                    title: "Data Structures",
                    description: "Focuses on arrays, lists, stacks, queues, trees, and their applications.",
                    category: categoriesByName["Programming"],
                    date: new DateTime(2025, 9, 1)
                ),
                new Quiz(
                    id: 3,
                    title: "Software Design",
                    description: "Includes design patterns, architecture principles, and system modelling.",
                    category: categoriesByName["Software Design"],
                    date: new DateTime(2025, 9, 1)
                ),
                new Quiz(
                    id: 4,
                    title: "Web Development",
                    description: "HTML, CSS, JavaScript, and client-server interactions",
                    category: categoriesByName["Web Development"],
                    date: new DateTime(2025, 9, 7)
                ),
                new Quiz(
                    id: 5,
                    title: "Database Systems",
                    description: "SQL queries, relational models, normalization, and transactions.",
                    category: categoriesByName["Database Systems"],
                    date: new DateTime(2025, 9, 7)
                ),
                new Quiz(
                    id: 6,
                    title: "Cybersecurity Basics",
                    description: "Encryption, authentication, and common security threats",
                    category: categoriesByName["Cybersecurity Basics"],
                    date: new DateTime(2025, 9, 11)
                ),
                new Quiz(
                    id: 7,
                    title: "Computer Networks",
                    description: "Protocols, IP addressing, routing, and network layers",
                    category: categoriesByName["Computer Networks"],
                    date: new DateTime(2025, 9, 13)
                )
            };

            // Assign quizzes to their categories
            foreach (var quiz in quizzes)
            {
                quiz.QuizCategory.AddQuiz(quiz);
            }

            // Add provided 10 OOP questions to Quiz ID 1
            var oopQuiz = quizzes.Find(q => q.QuizID == 1);
            if (oopQuiz != null)
            {
                oopQuiz.AddQuestion(new Question(1, "What does OOP stand for?",
                    new List<string> { "Object-Oriented Programming", "Operational Output Processing", "Open Order Protocol", "Overloaded Operator Procedure" },
                    "Object-Oriented Programming", "Easy"));

                oopQuiz.AddQuestion(new Question(2, "Which of the following is NOT a core principle of OOP?",
                    new List<string> { "Encapsulation", "Polymorphism", "Abstraction", "Compilation" },
                    "Compilation", "Easy"));

                oopQuiz.AddQuestion(new Question(3, "What is encapsulation in object-oriented programming?",
                    new List<string> { "Binding data and methods", "Inheritance", "Overloading", "Creating objects" },
                    "Binding data and methods", "Medium"));

                oopQuiz.AddQuestion(new Question(4, "Which keyword is used in C# to inherit a class?",
                    new List<string> { "extends", "inherits", ":", "base" },
                    ":", "Medium"));

                oopQuiz.AddQuestion(new Question(5, "What is the purpose of a constructor in a class?",
                    new List<string> { "To destroy objects", "To initialize objects", "To inherit methods", "To override properties" },
                    "To initialize objects", "Easy"));

                oopQuiz.AddQuestion(new Question(6, "Which concept allows multiple methods with the same name but different parameters?",
                    new List<string> { "Inheritance", "Polymorphism", "Overloading", "Encapsulation" },
                    "Overloading", "Medium"));

                oopQuiz.AddQuestion(new Question(7, "What is the base class for all classes in C#?",
                    new List<string> { "System.Object", "BaseClass", "RootClass", "MainClass" },
                    "System.Object", "Hard"));

                oopQuiz.AddQuestion(new Question(8, "What is the difference between a class and an object?",
                    new List<string> { "Class is an instance, object is a blueprint", "Class is a blueprint, object is an instance", "They are the same", "Object inherits class" },
                    "Class is a blueprint, object is an instance", "Medium"));

                oopQuiz.AddQuestion(new Question(9, "Which access modifier makes a member accessible only within its own class?",
                    new List<string> { "public", "private", "protected", "internal" },
                    "private", "Easy"));

                oopQuiz.AddQuestion(new Question(10, "What is polymorphism in OOP?",
                    new List<string> { "Ability to hide data", "Ability to inherit methods", "Ability to take many forms", "Ability to override constructors" },
                    "Ability to take many forms", "Medium"));
            }

            // Sort quizzes within categories by QuizID
            foreach (var category in categoriesByName.Values)
            {
                category.Quizzes.Sort((a, b) => a.QuizID.CompareTo(b.QuizID));
            }

            // Return the list of categories with their quizzes
            return new List<Category>(categoriesByName.Values);
        }
    }
}
