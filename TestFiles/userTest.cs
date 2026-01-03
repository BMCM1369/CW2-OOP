using System; // Add this at the top if not present
using Microsoft.VisualStudio.TestTools.UnitTesting; // Ensure this is present
using User_Class;



namespace UsersTest
{
    [TestClass]
    public class userTest
    {
        [TestMethod]
        public void UserConstructor_initialisation()
        {
            // Arrange
            int userId = 1;
            string userName = "testUser";
            string password = "Password123!";
            string email = "testUser@email.com";
            string role = "Student";
            // Act
            User user = new User(userId, userName, password, email, role);
            // Assert
            Assert.AreEqual(userId, user.UserId);
            Assert.AreEqual(userName, user.UserName);
            Assert.AreEqual(password, user.Password);
            Assert.AreEqual(email, user.Email);
            Assert.AreEqual(role, user.Role);
        }
        [TestMethod]
        public void UserID_getAndSet()
        {
            // Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            int newUserId = 2;
            // Act
            user.UserId = newUserId;
            // Assert
            Assert.AreEqual(newUserId, user.UserId);
        }
        [TestMethod]
        public void UserName_setAndSet()
        {
            //Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            string newUserName = "updatedUser";
            //Act
            user.UserName = newUserName;
            //Assert
            Assert.AreEqual(newUserName, user.UserName);

        }
        [TestMethod]
        public void Password_getAndSet()
        {

            //Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            string newPassword = "NewPassword456!";
            //Act
            user.Password = newPassword;
            //Assert
            Assert.AreEqual(newPassword, user.Password);

        }
        [TestMethod]
        public void Email_getAndSet()
        {

            //Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            string newEmail = "anotherUser@email.com";
            //Act
            user.Email = newEmail;
            //Assert
            Assert.AreEqual(newEmail, user.Email);
        }
        [TestMethod]
        public void Role_getAndSet()
        {

            //Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            string newRole = "Admin";
            //Act
            user.Role = newRole;
            //Assert
            Assert.AreEqual(newRole, user.Role);


        }
        [TestMethod]
        public void IsLoggedIn_getAndSet()
        {
            //Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            bool loginStatus = true;
            //Act
            user.IsLoggedIn = loginStatus;
            //Assert
            Assert.AreEqual(loginStatus, user.IsLoggedIn);

        }
        [TestMethod]
        public void Login_setToTrueWithValidCredentials()
        {
            // Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            // Act
            bool result = user.Login("testUser", "Password123!");
            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(user.IsLoggedIn);
        }
        [TestMethod]
        public void Login_setToFalseWithInvalidCredentials()
        {
            // Arrange
            User user = new User(1, "testUser", "Password123!", "testUser@email.com", "Student");
            // Act
            bool result = user.Login("testUser", "!321drowssaP");
            // Assert
            Assert.IsFalse(result);
            Assert.IsFalse(user.IsLoggedIn);
        }
        [TestMethod]

        public void UpdateUser_WithValidValues_UpdatesAllFields()
        {
            // Arrange
            var user = new User(1, "oldName", "oldPass", "old@email.com", "Student");

            // Act
            user.UpdateProfile("newName", "newPass", "new@email.com", "Admin");

            // Assert
            Assert.AreEqual("newName", user.UserName);
            Assert.AreEqual("newPass", user.Password);
            Assert.AreEqual("new@email.com", user.Email);
            Assert.AreEqual("Admin", user.Role);
        }

        [TestMethod]
        public void UpdateUser_WithEmptyStrings_DoesNotChangeValues()
        {
            // Arrange
            var user = new User(1, "name", "pass", "email@test.com", "Student");

            // Act
            user.UpdateProfile("", "", "", "");

            // Assert
            Assert.AreEqual("name", user.UserName);
            Assert.AreEqual("pass", user.Password);
            Assert.AreEqual("email@test.com", user.Email);
            Assert.AreEqual("Student", user.Role);
        }

        [TestMethod]
        public void UpdateUser_WithNullValues_DoesNotChangeValues()
        {
            // Arrange
            var user = new User(1, "name", "pass", "email@test.com", "Student");

            // Act
            user.UpdateProfile(null, null, null, null);

            // Assert
            Assert.AreEqual("name", user.UserName);
            Assert.AreEqual("pass", user.Password);
            Assert.AreEqual("email@test.com", user.Email);
            Assert.AreEqual("Student", user.Role);
        }

        [TestMethod]
        public void UpdateUser_WithPartialValues_OnlyUpdatesProvidedFields()
        {
            // Arrange
            var user = new User(1, "name", "pass", "email@test.com", "Student");

            // Act
            user.UpdateProfile("newName", null, "", "Admin");

            // Assert
            Assert.AreEqual("newName", user.UserName);
            Assert.AreEqual("pass", user.Password);
            Assert.AreEqual("email@test.com", user.Email);
            Assert.AreEqual("Admin", user.Role);
        }

        [TestMethod]
        public void UpdateUser_WithWhitespaceStrings_DoesNotChangeValues()
        {
            // Arrange
            var user = new User(1, "name", "pass", "email@test.com", "Student");

            // Act
            user.UpdateProfile("   ", "   ", "   ", "   ");

            // Assert
            Assert.AreEqual("name", user.UserName);
            Assert.AreEqual("pass", user.Password);
            Assert.AreEqual("email@test.com", user.Email);
            Assert.AreEqual("Student", user.Role);
        }

       
        

        

        

    }
}