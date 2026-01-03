using System;
using System.Linq;
using Quiz_Class;
using Xunit;

namespace Quiz_Class.Tests
{
    // Unit tests for the SampleData initializer to ensure categories/quizzes are created and linked correctly.
    public class SampleDataTests
    {
        [Fact(DisplayName = "SampleData creates 7 categories with quizzes assigned and sorted")]
        public void CreateSampleCategories_BuildsData()
        {
            // Act: build the sample categories and quizzes
            var categories = SampleData.CreateSampleCategories();

            // Assert: there should be 7 categories as specified in SampleData
            Assert.Equal(7, categories.Count);

            // Assert: within each category, quizzes should be sorted by QuizID
            foreach (var c in categories)
            {
                var quizIds = c.Quizzes.Select(q => q.QuizID).ToArray();
                var sorted = quizIds.OrderBy(id => id).ToArray();
                Assert.Equal(sorted, quizIds);
            }
        }

        [Fact(DisplayName = "OOP Fundamentals quiz is in Programming Concepts category and has 10 questions")]
        public void OopQuiz_HasQuestions()
        {
            // Act: build sample data then locate the 'Programming Concepts' category
            var categories = SampleData.CreateSampleCategories();
            var programmingConcepts = categories.First(c => c.CategoryName == "Programming Concepts");

            // Act: find the OOP Fundamentals quiz by ID within the category
            var oop = programmingConcepts.Quizzes.First(q => q.QuizID == 1);

            // Assert: title matches and the quiz has 10 provided questions
            Assert.Equal("OOP Fundamentals", oop.QuizTitle);
            Assert.Equal(10, oop.QuestionCount());
        }
    }
}
