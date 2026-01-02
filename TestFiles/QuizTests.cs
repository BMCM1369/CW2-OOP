using System;
using System.Collections.Generic;
using Quiz_Class;
using Xunit;

namespace Quiz_Class.Tests
{
    // Unit tests for the Quiz class covering constructor, getters/setters, and question management. 
    public class QuizTests
    {
        [Fact(DisplayName = "Quiz constructor initializes properties and empty question list")]
        public void Constructor_InitializesFields()
        {
            // Arrange: prepare a category and date for the quiz
            var category = new Category(1, "Programming", "desc");
            var date = new DateTime(2025, 9, 1);

            // Act: create the quiz with the given values
            var quiz = new Quiz(1, "OOP", "Basics", category, date);

            // Assert: properties reflect constructor inputs and questions list is empty
            Assert.Equal(1, quiz.QuizID);
            Assert.Equal("OOP", quiz.QuizTitle);
            Assert.Equal("Basics", quiz.QuizDescription);
            Assert.Same(category, quiz.QuizCategory);
            Assert.Equal(date, quiz.QuizDate);
            Assert.Equal(0, quiz.QuestionCount());
        }

        [Fact(DisplayName = "Quiz getters and setters work for min/max IDs and strings")]
        public void Properties_GetSet_Work_MinMax()
        {
            // Arrange: create a quiz with baseline values
            var quiz = new Quiz(0, string.Empty, string.Empty, new Category(1, "C", "D"), DateTime.MinValue);

            // Act & Assert: test min/max ID
            quiz.QuizID = int.MinValue;
            Assert.Equal(int.MinValue, quiz.QuizID);
            quiz.QuizID = int.MaxValue;
            Assert.Equal(int.MaxValue, quiz.QuizID);

            // Act: set long title/description strings
            quiz.QuizTitle = new string('T', 128);
            quiz.QuizDescription = new string('D', 256);

            // Assert: verify lengths and values
            Assert.Equal(128, quiz.QuizTitle.Length);
            Assert.Equal(256, quiz.QuizDescription.Length);

            // Act: change category and date
            var newCat = new Category(2, "Programming Concepts", "desc");
            var newDate = DateTime.MaxValue;
            quiz.QuizCategory = newCat;
            quiz.QuizDate = newDate;

            // Assert: new references and values are set
            Assert.Same(newCat, quiz.QuizCategory);
            Assert.Equal(newDate, quiz.QuizDate);
        }

        [Fact(DisplayName = "AddQuestion adds; GetQuestion returns by index; RemoveQuestion removes by ID")]
        public void Questions_Add_Get_Remove()
        {
            // Arrange: create a quiz and two questions
            var quiz = new Quiz(10, "Title", "Desc", new Category(1, "C", "D"), DateTime.UtcNow);
            var q1 = new Question(1, "Q1", new List<string> { "A", "B" }, "A", "Easy");
            var q2 = new Question(2, "Q2", new List<string> { "A", "B" }, "B", "Hard");

            // Act: add questions
            quiz.AddQuestion(q1);
            quiz.AddQuestion(q2);

            // Assert: count reflects added questions, and GetQuestion returns them by index
            Assert.Equal(2, quiz.QuestionCount());
            var idx0 = quiz.GetQuestion(0);
            var idx1 = quiz.GetQuestion(1);
            Assert.Same(q1, idx0);
            Assert.Same(q2, idx1);

            // Act: remove by ID
            var removed = quiz.RemoveQuestion(1);

            // Assert: removal succeeded, count decreased, and out-of-range index returns null
            Assert.True(removed);
            Assert.Equal(1, quiz.QuestionCount());
            Assert.Null(quiz.GetQuestion(5)); // out-of-range index returns null
        }

        [Fact(DisplayName = "EditQuestion updates properties when ID exists; returns false if missing")]
        public void EditQuestion_Updates_Or_Fails()
        {
            // Arrange: create a quiz with one question
            var quiz = new Quiz(10, "Title", "Desc", new Category(1, "C", "D"), DateTime.UtcNow);
            var q1 = new Question(1, "Q1", new List<string> { "A", "B" }, "A", "Easy");
            quiz.AddQuestion(q1);

            // Act: edit the existing question by ID
            var updated = quiz.EditQuestion(1, "Q1-Updated", new List<string> { "X", "Y" }, "X", "Medium");

            // Assert: question properties should reflect the new values
            Assert.True(updated);
            Assert.Equal("Q1-Updated", q1.QuestionText);
            Assert.Equal("X", q1.QuestionCorrectAnswer);
            Assert.Equal("Medium", q1.QuestionDifficultyLevel);
            Assert.Equal(2, q1.QuestionOptions.Count);

            // Act: attempt to edit a non-existent question
            var failed = quiz.EditQuestion(999, "No", new List<string>(), "", "");

            // Assert: editing fails and returns false
            Assert.False(failed);
        }
    }
}
