using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Category
    {
        private int categoryID;
        private string categoryName;
        private string categoryDescription;

        // This list holds the Quiz objects, fulfilling the Composition (1-to-many) requirement
        private List<Quiz> quizzes;

        public int CategoryID
        {
            get { return categoryID; }
            set { categoryID = value; }
        }

        public string CategoryName
        {
            get { return categoryName; }
            set { categoryName = value; }
        }

        public string CategoryDescription
        {
            get { return categoryDescription; }
            set { categoryDescription = value; }
        }

        // Expose the list of quizzes (read-only access to the list itself)
        public List<Quiz> Quizzes
        {
            get { return quizzes; }
        }

        // Constructor
        public Category(int id, string name, string description)
        {
            categoryID = id;
            categoryName = name;
            categoryDescription = description;
            quizzes = new List<Quiz>(); // Initialize the list
        }

        // Methods to manage the collection of quizzes
        public void AddQuiz(Quiz newQuiz)
        {
            if (newQuiz != null)
            {
                quizzes.Add(newQuiz);
            }
        }

        public bool RemoveQuiz(int quizID)
        {
            // Remove the quiz based on its ID
            return quizzes.RemoveAll(q => q.QuizID == quizID) > 0;
        }

        public Quiz GetQuiz(int quizID)
        {
            // Find and return a specific quiz
            return quizzes.Find(q => q.QuizID == quizID);
        }
    }
