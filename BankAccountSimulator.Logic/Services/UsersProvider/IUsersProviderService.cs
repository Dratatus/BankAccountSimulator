using BankAccountSimulator.Logic.Models;

namespace BankAccountSimulator.Logic.Services.UsersProvider
{
    public interface IUsersProviderService
    {
        public List<User> GetUsers();
        public void AddNewUser(string message = null);
        public User GetUserDataToLogin(string message = null, string message2 = null);
        public bool IsUserExist();
        public decimal CheckUserBalance();


    }
}
