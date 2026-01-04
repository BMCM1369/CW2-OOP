using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class User
{
    private int userId;
    private string userName = string.Empty;
    private string password = string.Empty;
    private string email = string.Empty;
    private string role = string.Empty;
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
    } // All the usual constructors and such for the attributes listed in the UML diagram
    
    public User(int userId, string userName, string password, string email, string role) //Constructor to create a user
    {
        this.UserId = userId;
        this.UserName = userName;
        this.Password = password;
        this.Email = email;
        this.Role = role;
        this.IsLoggedIn = false; //When a user is created they are not logged in by default
    }

    public bool Login(string userName, string password) //Created this method to complement the LogOut method, can be changed if need be but this is what makes sense to me
    {
        if (UserName == userName && Password == password) //checking if the username and password is valid
        {
            IsLoggedIn = true;
            Console.WriteLine("Login Successful, Welcome" + userName); //if it's valid, logs them in

        }
        else
        {
            IsLoggedIn = false;
            Console.WriteLine("Login failed, try again"); //if it's not, login attempt fails
        }
        return IsLoggedIn;
    }
    public bool LogOut() //LogOut method, checks if the user is loggedIn, if they are sets IsLoggedIn to false to log them out
    {
        if (IsLoggedIn)
        {
            IsLoggedIn = false;
            Console.WriteLine("User logged out successfully");
        }
        else
        {
            Console.WriteLine("User is not logged in"); 
        }
        return IsLoggedIn;
    }
    public void UpdateProfile( //This is what I assume the update profile method is supposed to be
    string? username = null, // "?" allows the string to be null (optional parameter), if the user hasn't entered anything the strings are set to "null"
    string? password = null,
    string? email = null,
    string? role = null)
    {
        if (!string.IsNullOrWhiteSpace(username)) //checking if the user wants to update the field, if it's null it remains unchanged
            UserName = username;

        if (!string.IsNullOrWhiteSpace(password))
            Password = password;

        if (!string.IsNullOrWhiteSpace(email))
            Email = email;

        if (!string.IsNullOrWhiteSpace(role))
            Role = role;

        // Didn't include UserID, didn't think that should be allowed to be changed
    }


}
