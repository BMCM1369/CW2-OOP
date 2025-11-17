using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class User
{
    private int userId;
    private string userName;
    private string password;
    private string email;
    private string role;
    private bool isLoggedIn;

    public int UserId
    {
        get { return userId; }
        set { userId = value; }
    }
    public string UserName {  
        get { return userName; } 
        set { userName = value; } 
    }

    public bool IsLoggedIn { 
        get { return isLoggedIn; } 
        set { isLoggedIn = value; } 
    }
    public string Password { 
        get { return password; } 
        set { password = value; } 
    }   
    public string Email
    {
        get { return email; }
        set { email = value; }
    }
    public string Role
    {
        get { return role; }
        set { role = value; }
    }
    public bool Login(string userName, string password)
    {
        if (UserName == userName && Password == password)
        {
            IsLoggedIn = true;
            Console.WriteLine("Login Successful, Welcome" + userName);

        }
        else
        {
            IsLoggedIn = false;
            Console.WriteLine("Login failed, try again");
        }
        return IsLoggedIn;
    }
    public bool LogOut()
    {
        if (IsLoggedIn)
        {
            IsLoggedIn = false;
            Console.WriteLine("User logged out successfullyy");
        }
        else
        {
            Console.WriteLine("User is not logged in");
        }
        return IsLoggedIn;
    }

}
