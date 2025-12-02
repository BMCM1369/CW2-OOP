using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeController
{
    public class Quiz
    {
        // Private fields (Data storage)
        private int quizID;
        private string quizTitle;
        private string quizDescription;
        private Category quizCategory;
        private List<Question> quizQuestions; // List to hold all questions in the quiz
        private DateTime quizDate;

        // Public Properties
        public int QuizID
        {
            get { return quizID; }
            set { quizID = value; }
        }

        public string QuizTitle
        {
            get { return quizTitle; }
            set { quizTitle = value; }
        }

        public string QuizDescription
        {
            get { return quizDescription; }
            set { quizDescription = value; }
        }

        public Category QuizCategory
        {
            get { return quizCategory; }
            set { quizCategory = value; }
        }

        public List<Question> QuizQuestions
        {
            get { return quizQuestions; }
        }

        public DateTime QuizDate
        {
            get { return quizDate; }
            set { quizDate = value; }
        }

        // Constructor: Initializes a new Quiz object
        public Quiz(int id, string title, string description, Category category, DateTime date)
        {
            quizID = id;
            quizTitle = title;
            quizDescription = description;
            quizCategory = category;
            quizDate = date;
            quizQuestions = new List<Question>(); // Initialize the question list
        }

        // Methods

        public Question GetQuestion(int questionIndex)
        {
            // Get a question based on its index
            if (questionIndex >= 0 && questionIndex < quizQuestions.Count)
            {
                return quizQuestions[questionIndex];
            }
            return null;
        }

        public void AddQuestion(Question newQuestion)
        {
            // Add a new question to the quiz
            if (newQuestion != null)
            {
                quizQuestions.Add(newQuestion);
            }
        }

        public bool EditQuestion(int questionID, string newText, List<string> newOptions, string newCorrectAnswer, string newDifficulty)
        {
            // Find a question by ID and update its properties
            Question questionToEdit = quizQuestions.Find(q => q.QuestionID == questionID);

            if (questionToEdit != null)
            {
                questionToEdit.QuestionText = newText;
                questionToEdit.QuestionOptions = newOptions;
                questionToEdit.QuestionCorrectAnswer = newCorrectAnswer;
                questionToEdit.QuestionDifficultyLevel = newDifficulty;
                return true;
            }
            return false;
        }

        public bool RemoveQuestion(int questionID)
        {
            // Remove a question from the list using its ID
            return quizQuestions.RemoveAll(q => q.QuestionID == questionID) > 0;
        }

        public int QuestionCount()
        {
            // Return the total number of questions
            return quizQuestions.Count;
        }
    }
}
public class Quiz
    {
        // Private fields (Data storage)
        private int quizID;
        private string quizTitle;
        private string quizDescription;
        private Category quizCategory;
        private List<Question> quizQuestions; // List to hold all questions in the quiz
        private DateTime quizDate;

        // Public Properties
        public int QuizID
        {
            get { return quizID; }
            set { quizID = value; }
        }

        public string QuizTitle
        {
            get { return quizTitle; }
            set { quizTitle = value; }
        }

        public string QuizDescription
        {
            get { return quizDescription; }
            set { quizDescription = value; }
        }

        public Category QuizCategory
        {
            get { return quizCategory; }
            set { quizCategory = value; }
        }

        public List<Question> QuizQuestions
        {
            get { return quizQuestions; }
        }

        public DateTime QuizDate
        {
            get { return quizDate; }
            set { quizDate = value; }
        }

        // Constructor: Initializes a new Quiz object
        public Quiz(int id, string title, string description, Category category, DateTime date)
        {
            quizID = id;
            quizTitle = title;
            quizDescription = description;
            quizCategory = category;
            quizDate = date;
            quizQuestions = new List<Question>(); // Initialize the question list
        }

        // Methods

        public Question GetQuestion(int questionIndex)
        {
            // Get a question based on its index
            if (questionIndex >= 0 && questionIndex < quizQuestions.Count)
            {
                return quizQuestions[questionIndex];
            }
            return null;
        }

        public void AddQuestion(Question newQuestion)
        {
            // Adds new questions to the quiz
            if (newQuestion != null)
            {
                quizQuestions.Add(newQuestion);
            }
        }

        public bool EditQuestion(int questionID, string newText, List<string> newOptions, string newCorrectAnswer, string newDifficulty)
        {
            // Finds and updates questions by their properties
            Question questionToEdit = quizQuestions.Find(q => q.QuestionID == questionID);

            if (questionToEdit != null)
            {
                questionToEdit.QuestionText = newText;
                questionToEdit.QuestionOptions = newOptions;
                questionToEdit.QuestionCorrectAnswer = newCorrectAnswer;
                questionToEdit.QuestionDifficultyLevel = newDifficulty;
                return true;
            }
            return false;
        }

        public bool RemoveQuestion(int questionID)
        {
        // Removes question from the list
            return quizQuestions.RemoveAll(q => q.QuestionID == questionID) > 0;
        }

        public int QuestionCount()
        {
            // Returns total number of questions
            return quizQuestions.Count;
        }
    }
