namespace BankSystem.User
{
    public class FakeUser : IUser
    {
        public string Login { get; }
        public string Password { get; }
        public decimal Balance { get; set; }

        public FakeUser(string login, string password, decimal balance)
        {
            Login = login;
            Password = password;
            Balance = balance;
        }
    }
}
