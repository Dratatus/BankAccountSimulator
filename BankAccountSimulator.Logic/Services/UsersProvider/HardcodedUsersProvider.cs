using BankAccountSimulator.Logic.Models;

namespace BankAccountSimulator.Logic.Services.UsersProvider
{
    public class HardcodedUsersProvider : IUsersProviderService
    {
        private User userToLogin = new User();


        private readonly List<User> _users = new List<User>
        {
            new User  {login = "admin", passwd = "1qaz", balance = 999999 }
        };

        public List<User> GetUsers()
        {
            var users = _users.ToList();

            return users;
        }


        public void AddNewUser(string message = null)
        {
            Console.WriteLine("Podaj login");
            string userLogin = Console.ReadLine();

            Console.WriteLine("Podaj hasło");
            string userPsswd = Console.ReadLine();

            User newUser = new User
            {
                login = userLogin,
                passwd = userPsswd
            };

            _users.Add(newUser);
            Console.WriteLine(message);
        }

        public User GetUserDataToLogin(string message = null, string message2 = null)
        {
            Console.WriteLine(message);
            string userLogin = Console.ReadLine();

            Console.WriteLine(message2);
            string userPsswd = Console.ReadLine();

            userToLogin.login = userLogin;
            userToLogin.passwd = userPsswd;

            return userToLogin;
        }

        public bool IsUserExist()
        {
            var users = GetUsers();

            var userData = userToLogin;


            bool isUserExist = _users.Any(us => us.login == userData.login && us.passwd == userData.passwd);

            return isUserExist;
        }

        public decimal CheckUserBalance()
        {
            var users = GetUsers();
            bool isUserExist = IsUserExist();
            var userData = userToLogin;

            User user = _users.Single(u => u.login == userData.login && u.passwd == userData.passwd);
            bool isUserHaveBalance = user.balance > 0;


            if (isUserExist && isUserHaveBalance)
            {
                Console.Clear();
                Console.WriteLine($"saldo twojego konta wynosi: {user.balance} \n");
                return user.balance;
            }
            else if (!isUserHaveBalance)
            {
                Console.WriteLine("nie posiadasz żadnych środków na koncie \n");
                return 0;
            }
            else
            {
                Console.WriteLine("Nie Znaleziono użykownika \n");
                return 0;
            }
        }


        public decimal DepostitMoney()
        {
            string userAmountOfMoney.
            var userData = userToLogin;

            userData.balance
        }


    }
}

