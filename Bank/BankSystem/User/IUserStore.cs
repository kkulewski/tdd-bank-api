namespace BankSystem.User
{
    public interface IUserStore
    {
        IUser GetUserByLogin(string login);
    }
}
