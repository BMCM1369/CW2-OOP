namespace StudentTests
{
    using User_Class;

    [TestClass]
    public class StudentTests
    {
        [TestMethod]
        public void Student_ConstructorInitialisation()
        {
            //Arrange
            string status = "Active";
            int score = 85;
            int highScore = 95;
            int rank = 3;
            List<string> completedQuizzes = new List<string>
            {
                "Math Basics",
                "C# Fundamentals",
                "OOP Concepts"
            };
            //Act
            Student student = Student.CreateSampleStudent();
            //Assert
            Assert.AreEqual(status, student.Status);
            Assert.AreEqual(score, student.Score);
            Assert.AreEqual(highScore, student.HighScore);
            Assert.AreEqual(rank, student.Rank);
            CollectionAssert.AreEqual(completedQuizzes, student.CompletedQuizzes);
        }
        [TestMethod]
        public void Student_StatusGetAndSet()
        {
            //Arrange
            Student student = Student.CreateSampleStudent();
            string newStatus = "Inactive";
            //Act
            student.Status = newStatus;
            //Assert
            Assert.AreEqual(newStatus, student.Status);
        }
        [TestMethod]
        public void Student_ScoreGetAndSet()
        {
            //Arrange
            Student student = Student.CreateSampleStudent();
            int newScore = 90;
            //Act
            student.Score = newScore;
            //Assert
            Assert.AreEqual(newScore, student.Score);

        }
        [TestMethod]
        public void Student_HighScoreGetAndSet()
        {
            //Arrange
            Student student = Student.CreateSampleStudent();
            int newHighScore = 100;
            //Act
            student.HighScore = newHighScore;
            //Assert
            Assert.AreEqual(newHighScore, student.HighScore);
        }
        [TestMethod]
        public void Student_RankGetAndSet()
        {
            //Arrange
            Student student = Student.CreateSampleStudent();
            int newRank = 1;
            //Act
            student.Rank = newRank;
            //Assert
            Assert.AreEqual(newRank, student.Rank);
        }
        [TestMethod]
        public void Student_CompletedQuizzesGetAndSet()
        {
            //Arrange
            Student student = Student.CreateSampleStudent();
            List<string> newCompletedQuizzes = new List<string>
            {
                "Advanced C#",
                "Data Structures"
            };
            //Act
            student.CompletedQuizzes = newCompletedQuizzes;
            //Assert
            CollectionAssert.AreEqual(newCompletedQuizzes, student.CompletedQuizzes);
        }
        [TestMethod]
        public void Student_InheritedUserProperties()
        {
            //Arrange
            int userId = 1;
            string userName = "sampleStudent";
            string password = "Password123!";
            string email = "student@email.com;";
            string role = "Student";
            //Act
            Student student = Student.CreateSampleStudent();
            //Assert
            Assert.AreEqual(userId, student.UserId);
            Assert.AreEqual(userName, student.UserName);
            Assert.AreEqual(password, student.Password);
            Assert.AreEqual(email, student.Email);
            Assert.AreEqual(role, student.Role);

        }
        [TestMethod]
        public void CreateSampleStudent_ShouldInitializeCorrectly()
        {
            // Act
            var student = Student.CreateSampleStudent();

            // Assert
            Assert.IsNotNull(student);
            Assert.AreEqual("sampleStudent", student.UserName);
            Assert.AreEqual("Active", student.Status);
            Assert.AreEqual(85, student.Score);
            Assert.AreEqual(95, student.HighScore);
            Assert.AreEqual(3, student.Rank);
            
        }

        [TestMethod]
        public void DisplayStudentMenu_NullStudent_ShouldShowMessage()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Student.DisplayStudentMenu(student: null);

            // Assert
            Assert.AreEqual("No student logged in", output.ToString());
        }

        [TestMethod]
        public void UpdateScore_ShouldUpdateScoreAndHighScore()
        {
            // Arrange
            var student = Student.CreateSampleStudent();
            Console.SetIn(new StringReader("100\n"));
            Console.SetOut(new StringWriter());

            // Act (invoke through menu)
            Student.DisplayStudentMenu(student);

            // Assert
            Assert.AreEqual(100, student.Score);
            Assert.AreEqual(100, student.HighScore);
        }

        [TestMethod]
        public void SubmitQuiz_ShouldAddQuizAndIncreaseScore()
        {
            // Arrange
            var student = Student.CreateSampleStudent();
            int initialScore = student.Score;

            Console.SetIn(new StringReader("2\nNew Quiz\n3\n"));
            Console.SetOut(new StringWriter());

            // Act
            Student.DisplayStudentMenu(student);

            // Assert
            Assert.IsTrue(student.Score > initialScore);
        }

        [TestMethod]
        public void SubmitQuiz_DuplicateQuiz_ShouldNotAddAgain()
        {
            // Arrange
            var student = Student.CreateSampleStudent();
            Console.SetIn(new StringReader("2\nMath Basics\n3\n"));
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Student.DisplayStudentMenu(student);

            // Assert
            Assert.AreEqual(3, student.CompletedQuizzes.Count);
           
        }
    }
}


   
