namespace BankSystem.User
{
    public interface IUser
    {
        string Login { get; }
        string Password { get; }

        decimal Balance { get; set; }
    }
}
