using System;
using System.Collections.Generic;
using System.Text;
using User_Class;

namespace Quiz_Class
{
    public class LeaderBoard
    {
        // private fields
        private int quizID;
        private List<Student> users;
        private List<Student> sorted_users;
        private List<LeaderBoardRank> ranking;

        // public properties
        public int QuizID
        {
            get { return quizID; }
            set { quizID = value; }
        }

        public List<Student> Users
        {
            get { return users; }
            set { users = (value == null) ? new List<Student>() : value; }
        }

        public List<Student> Sorted_Users
        {
            get { return sorted_users; }
            private set { sorted_users = value; }
        }

        public List<LeaderBoardRank> Ranking
        {
            get { return ranking; }
            private set { ranking = value; }
        }

        // constructor
        public LeaderBoard(int quizID)
        {
            this.quizID = quizID;
            users = new List<Student>();
            sorted_users = new List<Student>();
            ranking = new List<LeaderBoardRank>();
        }

        // adds a user if not in the list by username
        public void Add_User(Student student)
        {
            if (student == null)
                throw new ArgumentNullException(nameof(student));

            if (string.IsNullOrWhiteSpace(student.UserName))
                throw new ArgumentException("Student UserName must be valid.", nameof(student));

            for (int i = 0; i < users.Count; i++)
            {
                if (string.Equals(users[i].UserName, student.UserName, StringComparison.OrdinalIgnoreCase))
                {
                    // if user exists do not add again
                    return;
                }
            }

            users.Add(student);
        }

        // removes a user by username
        public bool Remove_User(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return false;

            for (int i = 0; i < users.Count; i++)
            {
                if (string.Equals(users[i].UserName, username, StringComparison.OrdinalIgnoreCase))
                {
                    users.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        // update the leaderboard assign rankings and sort - use of dense ranking so ranks not skipped
        public void Update_leaderboard()
        {
            // copy users into sorted_users
            sorted_users = new List<Student>();
            for (int i = 0; i < users.Count; i++)
            {
                sorted_users.Add(users[i]);
            }

            // sort sorted_users by HighScore DESC and UserName ASC
            sorted_users.Sort(CompareStudentsForLeaderboard);

            // build the ranking list
            ranking = new List<LeaderBoardRank>();

            int currentRank = 0;
            int lastScore = int.MinValue;
            bool first = true;

            for (int i = 0; i < sorted_users.Count; i++)
            {
                Student s = sorted_users[i];

                if (first)
                {
                    currentRank = 1;
                    lastScore = s.HighScore;
                    first = false;
                }
                else
                {
                    if (s.HighScore != lastScore)
                    {
                        currentRank++;
                        lastScore = s.HighScore;
                    }
                }

                // update the student's Rank field
                s.Rank = currentRank;

                // add to ranking output list
                ranking.Add(new LeaderBoardRank(s.UserName, s.HighScore, currentRank));
            }
        }

        // method to sort
        private int CompareStudentsForLeaderboard(Student a, Student b)
        {
            // HighScore DESC
            if (a.HighScore > b.HighScore) return -1;
            if (a.HighScore < b.HighScore) return 1;

            // UserName ASC - ignore case
            return string.Compare(a.UserName, b.UserName, StringComparison.OrdinalIgnoreCase);
        }

        // get rank of a user - returns -1 if not found
        public int get_rank(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty.", nameof(username));

            if (ranking.Count == 0)
                Update_leaderboard();

            for (int i = 0; i < ranking.Count; i++)
            {
                if (string.Equals(ranking[i].Username, username, StringComparison.OrdinalIgnoreCase))
                {
                    return ranking[i].Rank;
                }
            }

            return -1;
        }

        // return top N users
        public List<LeaderBoardRank> Top_Users(int n = 10)
        {
            List<LeaderBoardRank> result = new List<LeaderBoardRank>();

            if (n <= 0)
                return result;

            if (ranking.Count == 0)
                Update_leaderboard();

            int limit = n;
            if (ranking.Count < limit)
                limit = ranking.Count;

            for (int i = 0; i < limit; i++)
            {
                result.Add(ranking[i]);
            }

            return result;
        }

        // display leaderboard
        public string display()
        {
            if (ranking.Count == 0)
                Update_leaderboard();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("===== LEADERBOARD =====");
            sb.AppendLine("QuizID: " + quizID);
            sb.AppendLine("------------------------");

            if (ranking.Count == 0)
            {
                sb.AppendLine("No scores available.");
                return sb.ToString();
            }

            for (int i = 0; i < ranking.Count; i++)
            {
                LeaderBoardRank r = ranking[i];
                sb.AppendLine(r.Rank + ". " + r.Username + " - " + r.Score);
            }

            return sb.ToString();
        }

        // PascalCase aliases are provided for public methods to match C# conventions while keeping original method names intact
        public void UpdateLeaderboard() { Update_leaderboard(); }
        public int GetRank(string username) { return get_rank(username); }
        public List<LeaderBoardRank> TopUsers(int n = 10) { return Top_Users(n); }
        public string Display() { return display(); }
    }

    // class for output ranks in leaderboard
    public class LeaderBoardRank
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }

        public LeaderBoardRank(string username, int score, int rank)
        {
            Username = username;
            Score = score;
            Rank = rank;
        }
    }
}
