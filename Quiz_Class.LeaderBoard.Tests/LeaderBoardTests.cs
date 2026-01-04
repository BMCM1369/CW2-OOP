using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quiz_Class;
using User_Class;
using System;

namespace QuizClassLeaderBoardTests
{
    [TestClass]
    public class LeaderBoardTests
    {
        // method creates a Student with the required constructor and sets HighScore for leaderboard rank tests        
        private Student MakeStudent(string username, int highScore, int userId = 1)
        {
            Student s = new Student(
                userId: userId,
                userName: username,
                password: "Temp123!",
                email: "temp@example.com",
                role: "Student"
            );

            s.HighScore = highScore;
            return s;
        }

        // constructor and properties tests

        [TestMethod]
        public void Constructor_SetsQuizID_AndInitialisesLists()
        {
            // test that QuizID is stored correctly - lists are created and empty
            LeaderBoard lb = new LeaderBoard(7);

            Assert.AreEqual(7, lb.QuizID);
            Assert.IsNotNull(lb.Users);
            Assert.AreEqual(0, lb.Users.Count);
        }

        [TestMethod]
        public void QuizID_Property_GetSet_WorksCorrectly()
        {
            // test that QuizId can be set and value available
            LeaderBoard lb = new LeaderBoard(1);

            lb.QuizID = 99;

            Assert.AreEqual(99, lb.QuizID);
        }

        [TestMethod]
        public void Users_Property_SetNull_ResultsInEmptyList()
        {
            // edge case for Users property - users becomes a non-null empty list
            LeaderBoard lb = new LeaderBoard(1);

            lb.Users = null;

            Assert.IsNotNull(lb.Users);
            Assert.AreEqual(0, lb.Users.Count);
        }

        // adding users tests

        [TestMethod]
        public void AddUser_AddsStudent_WhenNotAlreadyPresent()
        {
            // test to add a User - Users count increases by 1 and correct user added
            LeaderBoard lb = new LeaderBoard(1);
            Student s = MakeStudent("Oisin", 10);

            lb.Add_User(s);

            Assert.AreEqual(1, lb.Users.Count);
            Assert.AreEqual("Oisin", lb.Users[0].UserName);
        }

        [TestMethod]
        public void AddUser_DoesNotAddDuplicateUsername()
        {
            // test to add a duplicate User - same userName not added
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("Oisin", 10, 1));
            lb.Add_User(MakeStudent("Oisin", 99, 2)); // duplicate userName - different score and userId

            Assert.AreEqual(1, lb.Users.Count);
        }

        [TestMethod]
        public void AddUser_IsCaseInsensitive_ForDuplicateCheck()
        {
            // test to add different case userName - oisin vs OISIN not added
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("oisin", 10));
            lb.Add_User(MakeStudent("OISIN", 20));

            Assert.AreEqual(1, lb.Users.Count);
        }

        [TestMethod]
        public void AddUser_NullStudent_ThrowsArgumentNullException()
        {
            // test validation for null argument for Add_User - throws ArgumentNullException
            LeaderBoard lb = new LeaderBoard(1);

            Assert.ThrowsException<ArgumentNullException>(() => lb.Add_User(null));
        }

        [TestMethod]
        public void AddUser_EmptyUsername_ThrowsArgumentException()
        {
            // test validation for blank userName for Add_User - throws ArgumentException
            LeaderBoard lb = new LeaderBoard(1);
            Student bad = MakeStudent(" ", 10);

            Assert.ThrowsException<ArgumentException>(() => lb.Add_User(bad));
        }

        // removing users tests

        [TestMethod]
        public void RemoveUser_RemovesExistingUser_ReturnsTrue()
        {
            // test Remove_user for an existing userName - user removed and returns true
            LeaderBoard lb = new LeaderBoard(1);
            lb.Add_User(MakeStudent("Oisin", 10));

            bool removed = lb.Remove_User("Oisin");

            Assert.IsTrue(removed);
            Assert.AreEqual(0, lb.Users.Count);
        }

        [TestMethod]
        public void RemoveUser_UserNotFound_ReturnsFalse()
        {
            
            // test Remove_user for an non-existing userName - user not removed and returns false
            LeaderBoard lb = new LeaderBoard(1);
            lb.Add_User(MakeStudent("Oisin", 10));

            bool removed = lb.Remove_User("Gavin");

            Assert.IsFalse(removed);
            Assert.AreEqual(1, lb.Users.Count);
        }

        [TestMethod]
        public void RemoveUser_BlankUsername_ReturnsFalse()
        {
            
            // edge case for Remove_User empty input - returns false
            LeaderBoard lb = new LeaderBoard(1);

            bool removed = lb.Remove_User(" ");

            Assert.IsFalse(removed);
        }

        // update leaderboard tests - sorting and ranking rules

        [TestMethod]
        public void UpdateLeaderboard_SortsByHighScoreDescending()
        {
            // test Update_leaderboard sort - highest score first
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("A", 5));
            lb.Add_User(MakeStudent("B", 20));
            lb.Add_User(MakeStudent("C", 10));

            lb.Update_leaderboard();

            Assert.AreEqual("B", lb.Ranking[0].Username);
            Assert.AreEqual(20, lb.Ranking[0].Score);
        }

        [TestMethod]
        public void UpdateLeaderboard_TiesUseDenseRanking()
        {
            
            // test dense ranking - ties get same rank next rank increments by 1
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("A", 10));
            lb.Add_User(MakeStudent("B", 10));
            lb.Add_User(MakeStudent("C", 7));

            lb.Update_leaderboard();

            Assert.AreEqual(1, lb.Ranking[0].Rank);
            Assert.AreEqual(1, lb.Ranking[1].Rank);
            Assert.AreEqual(2, lb.Ranking[2].Rank);
        }

        [TestMethod]
        public void UpdateLeaderboard_SetsStudentRankProperty()
        {
            
            // test Update_leaderboard sets Student.Rank - correct rank values assigned
            LeaderBoard lb = new LeaderBoard(1);
            Student top = MakeStudent("Top", 100);
            Student low = MakeStudent("Low", 10);

            lb.Add_User(low);
            lb.Add_User(top);

            lb.Update_leaderboard();

            Assert.AreEqual(1, top.Rank);
            Assert.AreEqual(2, low.Rank);
        }

        [TestMethod]
        public void UpdateLeaderboard_HandlesMinMaxScores()
        {
            
            // test Update_leaderboard with edge case scores - 0 and int.MaxValue
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("MinUser", 0));
            lb.Add_User(MakeStudent("MaxUser", int.MaxValue));

            lb.Update_leaderboard();

            Assert.AreEqual("MaxUser", lb.Ranking[0].Username);
            Assert.AreEqual(int.MaxValue, lb.Ranking[0].Score);
            Assert.AreEqual(1, lb.Ranking[0].Rank);
        }

        // rank tests

        [TestMethod]
        public void GetRank_ReturnsCorrectRank_ForExistingUser()
        {
            
            // test get_rank for existing users - correct rank returned based on HighScore
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("Gavin", 10));
            lb.Add_User(MakeStudent("Oisin", 8));

            lb.Update_leaderboard();

            int rank = lb.get_rank("Oisin");
            Assert.AreEqual(2, rank);
        }

        [TestMethod]
        public void GetRank_ReturnsMinus1_ForMissingUser()
        {
            
            // test get_rank for missing user - returns -1
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("A", 10));
            lb.Update_leaderboard();

            int rank = lb.get_rank("Missing");
            Assert.AreEqual(-1, rank);
        }

        [TestMethod]
        public void GetRank_BlankUsername_ThrowsArgumentException()
        {
            
            // test get_rank for blank input - throws ArgumentException
            LeaderBoard lb = new LeaderBoard(1);

            Assert.ThrowsException<ArgumentException>(() => lb.get_rank(""));
        }

        // top users tests

        [TestMethod]
        public void TopUsers_ReturnsTopN_InCorrectOrder()
        {
            
            // test Top_Users for top N users - returns correct users in order highest to lowest
            LeaderBoard lb = new LeaderBoard(1);

            lb.Add_User(MakeStudent("A", 1));
            lb.Add_User(MakeStudent("B", 2));
            lb.Add_User(MakeStudent("C", 3));

            var top2 = lb.Top_Users(2);

            Assert.AreEqual(2, top2.Count);
            Assert.AreEqual("C", top2[0].Username);
            Assert.AreEqual("B", top2[1].Username);
        }

        [TestMethod]
        public void TopUsers_WhenNIsGreaterThanCount_ReturnsAll()
        {
            
            // edge case for Top_Users n > Users.Count - returns all users
            LeaderBoard lb = new LeaderBoard(1);
            lb.Add_User(MakeStudent("A", 10));
            lb.Add_User(MakeStudent("B", 5));

            var top10 = lb.Top_Users(10);

            Assert.AreEqual(2, top10.Count);
        }

        [TestMethod]
        public void TopUsers_NonPositiveN_ReturnsEmptyList()
        {
            
            // edge case for Top_Users n <= 0 - returns empty list
            LeaderBoard lb = new LeaderBoard(1);
            lb.Add_User(MakeStudent("A", 10));

            Assert.AreEqual(0, lb.Top_Users(0).Count);
            Assert.AreEqual(0, lb.Top_Users(-3).Count);
        }

        // display tests

        [TestMethod]
        public void Display_WhenEmpty_IncludesNoScoresMessage()
        {
            
            // test display out with no users - displays No scores avaliable message
            LeaderBoard lb = new LeaderBoard(1);

            string output = lb.display();

            StringAssert.Contains(output, "No scores available");
        }

        [TestMethod]
        public void Display_IncludesUsernameAndScore()
        {
            
            // test display output with users - displays username and score
            LeaderBoard lb = new LeaderBoard(1);
            lb.Add_User(MakeStudent("Oisin", 15));

            string output = lb.display();

            StringAssert.Contains(output, "Oisin");
            StringAssert.Contains(output, "15");
        }
    }
}
