namespace AdminTests
{
    using User_Class;
    [TestClass]
    public sealed class AdminTests
    {
        [TestMethod]
        public void Admin_ConstructorInitialisation()
        {
            //Arrange
            DateOnly loginDate = DateOnly.FromDateTime(DateTime.Now);
            //Act
            Admin admin = Admin.CreateSampleAdmin();
            //Assert
            Assert.AreEqual(loginDate, admin.LoginDate);
        }
        [TestMethod]
        public void Admin_LoginDateGetAndSet()
        {
            //Arrange
            Admin admin = Admin.CreateSampleAdmin();
            DateOnly newLoginDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            //Act
            admin.LoginDate = newLoginDate;
            //Assert
            Assert.AreEqual(newLoginDate, admin.LoginDate);
        }
        [TestMethod]
        public void CreateSampleAdmin_ShouldInitializeCorrectly()
        {
            // Act
            var admin = Admin.CreateSampleAdmin();

            // Assert
            Assert.IsNotNull(admin);
            Assert.AreEqual("sampleAdmin", admin.UserName);
            Assert.AreEqual("Admin", admin.Role);
            Assert.AreNotEqual(default, admin.LoginDate);
        }

        [TestMethod]
        public void ManageUsers_NoUsers_ShouldShowMessage()
        {
            // Arrange
            var users = new List<User>();
            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            Admin.ManageUsers(users);

            // Assert
            Assert.IsTrue(output.ToString().Contains("No users available"));
        }

        [TestMethod]
        public void AddUser_ShouldIncreaseUserCount()
        {
            // Arrange
            var users = new List<User>();
            Console.SetIn(new StringReader("testuser\npass123\ntest@email.com\nStudent\n"));
            Console.SetOut(new StringWriter());

            // Act
            Admin.AddUser(users);

            // Assert
            Assert.AreEqual("testuser", users[0].UserName);
        }

        [TestMethod]
        public void RemoveUser_ValidId_ShouldRemoveUser()
        {
            // Arrange
            var users = new List<User>
            {
                new User(1, "user1", "pass", "u1@mail.com", "Student")
            };

            Console.SetIn(new StringReader("1\n"));
            Console.SetOut(new StringWriter());

            // Act
            Admin.RemoveUser(users);

            // Assert
            Assert.IsNull(users);
        }

        [TestMethod]
        public void UpdateUserRole_ShouldChangeRole()
        {
            // Arrange
            var users = new List<User>
            {
                new User(1, "user1", "pass", "u1@mail.com", "Student")
            };

            Console.SetIn(new StringReader("1\nAdmin\n"));
            Console.SetOut(new StringWriter());

            // Act
            Admin.UpdateUserRole(users);

            // Assert
            Assert.AreEqual("Admin", users[0].Role);
        }
    }
}
