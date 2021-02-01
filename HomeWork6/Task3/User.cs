namespace Task3
{
    public class User
    {
        public string Login { get; }
        public string Password { get; }

        public User(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public User(string[] data) : this(data[0], data[1])
        {
        }
    }
}