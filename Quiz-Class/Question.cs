using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Question//THIS IS A PLACEHOLDER CLASS, REPLACE WITH FINAL QUESTION CLASS CODE!!
                         // Represents a question in the quiz application for dependency purposes.
{
    private int questionID;
        private string questionText;
        private List<string> questionOptions;
        private string questionCorrectAnswer;
        private string questionDifficultyLevel;

        public int QuestionID
        {
            get { return questionID; }
            set { questionID = value; }
        }

        public string QuestionText
        {
            get { return questionText; }
            set { questionText = value; }
        }

        public List<string> QuestionOptions
        {
            get { return questionOptions; }
            set { questionOptions = value; }
        }

        public string QuestionCorrectAnswer
        {
            get { return questionCorrectAnswer; }
            set { questionCorrectAnswer = value; }
        }

        public string QuestionDifficultyLevel
        {
            get { return questionDifficultyLevel; }
            set { questionDifficultyLevel = value; }
        }

        public Question(int id, string text, List<string> options, string correctAnswer, string difficulty)
        {
            questionID = id;
            questionText = text;
            questionOptions = options ?? new List<string>();
            questionCorrectAnswer = correctAnswer;
            questionDifficultyLevel = difficulty;
        }

        public bool CheckAnswer(string userAnswer)
        {
            return true;
        }

        public string ReturnResult(string userAnswer)
        {
            return "Result Placeholder";
        }//TO BE REPLACED WITH FINAL METHOD CODE BY CARA!
    }
