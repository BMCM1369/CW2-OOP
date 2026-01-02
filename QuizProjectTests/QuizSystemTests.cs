using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiz_Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace QuizSystemTests
{
    [TestClass]
    public class QuizSystemUnitTests
    {
        private QuizSystem quizSystem;

        [TestInitialize]
        public void Setup()
        {
            // loads quiz system
            quizSystem = new QuizSystem();
            quizSystem.LoadSampleData();
        }


        [TestMethod]
        public void AdminLogin_Successful()
        {
            // Arrange
            var users = quizSystem.adminUsers.Cast<User>().ToList();

            // Act
            var result = quizSystem.AuthenticateUser(users, "Admin1", "admin123");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Admin));
        }

        [TestMethod]
        public void StudentLogin_Successful()
        {
            // Arrange
            var users = quizSystem.studentUsers.Cast<User>().ToList();

            // Act
            var result = quizSystem.AuthenticateUser(users, "Student1", "pass123");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Student));
        }

        [TestMethod]
        public void AuthenticateUser_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var users = quizSystem.adminUsers.Cast<User>().ToList();

            // Act
            var result = quizSystem.AuthenticateUser(users, "WrongUser", "WrongPass");

            // Assert
            Assert.IsNull(result);
        }


        [TestMethod]
        public void ProgrammingCategory_Exists()
        {
            // Act
            var category = quizSystem.GetCategories()
                .FirstOrDefault(c => c.CategoryName == "Programming");

            // Assert
            Assert.IsNotNull(category);
        }

        [TestMethod]
        public void ProgrammingQuiz_HasTenQuestions()
        {
            // Arrange
            var category = quizSystem.GetCategories()
                .First(c => c.CategoryName == "Programming");

            var quiz = category.Quizzes.First();

            // Act
            int questionCount = quiz.QuizQuestions.Count;

            // Assert
            Assert.AreEqual(10, questionCount);
        }


        [TestMethod]
        public void SaveQuizToCSV_CreatesFile()
        {
            // Arrange
            var category = quizSystem.GetCategories()
                .First(c => c.CategoryName == "Programming");

            var quiz = category.Quizzes.First();

            string filePath = Path.Combine(Path.GetTempPath(), "TestQuiz.csv");

            if (File.Exists(filePath))
                File.Delete(filePath);

            // Act
            quizSystem.SaveQuizToCSV(quiz, filePath);

            // Assert
            Assert.IsTrue(File.Exists(filePath));

            // Cleanup
            File.Delete(filePath);
        }
    }
}
