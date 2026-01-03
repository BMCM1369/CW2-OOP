using System;
using System.Linq;
using Quiz_Class;
using Xunit;

namespace Quiz_Class.Tests
{
    // Unit tests for the Category class covering constructor, getters/setters, and collection operations.
    public class CategoryTests
    {
        [Fact(DisplayName = "Category constructor initializes properties and empty quiz list")]
        public void Constructor_InitializesFields()
        {
            // Arrange & Act: create a category using the constructor
            var category = new Category(1, "Programming Concepts", "Basics of programming");

            // Assert: properties should reflect constructor values, quizzes should start empty
            Assert.Equal(1, category.CategoryID); // verifies get property
            Assert.Equal("Programming Concepts", category.CategoryName);
            Assert.Equal("Basics of programming", category.CategoryDescription);
            Assert.Empty(category.Quizzes); // quizzes list starts empty
        }

        [Fact(DisplayName = "Category getters and setters work for min/max integer IDs and names")]
        public void Properties_GetSet_Work_MinMax()
        {
            // Arrange: base category
            var category = new Category(0, string.Empty, string.Empty);

            // Act & Assert: set min and max ID values and verify they persist
            category.CategoryID = int.MinValue;
            Assert.Equal(int.MinValue, category.CategoryID);

            category.CategoryID = int.MaxValue;
            Assert.Equal(int.MaxValue, category.CategoryID);

            // Act & Assert: set strings and ensure they are stored
            category.CategoryName = "A";
            category.CategoryDescription = new string('d', 256);
            Assert.Equal("A", category.CategoryName);
            Assert.Equal(256, category.CategoryDescription.Length);
        }

        [Fact(DisplayName = "AddQuiz adds quiz and RemoveQuiz removes by ID; GetQuiz returns the quiz")]
        public void ManageQuizzes_Add_Remove_Get()
        {
            // Arrange: create category and a quiz belonging to it
            var category = new Category(10, "Programming", "desc");
            var quiz = new Quiz(5, "Title", "Desc", category, DateTime.UtcNow);

            // Act: add quiz to category
            category.AddQuiz(quiz);
            // Assert: quiz appears in the category list
            Assert.Single(category.Quizzes);
            Assert.Same(quiz, category.Quizzes.First());

            // Act: retrieve quiz by ID
            var found = category.GetQuiz(5);
            // Assert: correct quiz is returned
            Assert.NotNull(found);
            Assert.Equal(5, found!.QuizID);

            // Act: remove quiz by ID
            var removed = category.RemoveQuiz(5);
            // Assert: removal succeeded and list is empty
            Assert.True(removed);
            Assert.Empty(category.Quizzes);
        }

        [Fact(DisplayName = "RemoveQuiz returns false when ID not found; GetQuiz returns null")]
        public void ManageQuizzes_NotFound()
        {
            // Arrange: category with no quizzes
            var category = new Category(10, "Programming", "desc");

            // Act: attempt to remove non-existent quiz
            var result = category.RemoveQuiz(999);
            // Assert: removal fails, and lookup returns null
            Assert.False(result);
            Assert.Null(category.GetQuiz(999));
        }
    }
}
