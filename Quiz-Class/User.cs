using System;

namespace Quiz_Class
{
    public class User // placeholder for dependencies
    {
        private int id;
        private string userName;
        private string passWord;
        private string email;
        private string role;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return passWord; }
            set { passWord = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Role
        {
            get { return role; }
        }

        public User(int id, string username, string password, string email, string role)
        {
            this.id = id;
            this.userName = username;
            this.passWord = password;
            this.email = email;
            this.role = role;
        }

        public void UpdateProfile(string userName, string password)
        {
            this.userName = userName;
            this.passWord = password;
        }

        public void Logout()
        {
            Environment.Exit(0);
        }

        public bool PasswordMatches(string input)
        {
            return passWord == input;
        }
    }
}
