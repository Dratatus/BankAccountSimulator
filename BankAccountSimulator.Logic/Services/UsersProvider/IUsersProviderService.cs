using BankAccountSimulator.Logic.Models;

namespace BankAccountSimulator.Logic.Services.UsersProvider
{
    public interface IUsersProviderService
    {
        public List<User> GetUsers();
        public void AddNewUser(string message = null);
    }
}
