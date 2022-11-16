using BankAccountSimulator.Logic.Models;

namespace BankAccountSimulator.Logic.Services.UsersProvider
{
    public class HardcodedUsersProvider : IUsersProviderService
    {
        private readonly List<User> _users = new List<User>
        {
            new User {login = "admin", passwd = "1qaz", balance = 999999, AccountHistory = { ""} },
        };

        public List<User> GetUsers()
        {
            // Metoda ToList tworzy nową instancję listy. W ten sposób zwracasz INNĄ listę z tymi samymi danymi
            // Jak ktoś pobierze sobie listę walut i doda do niej nową walutę to ona nie uwzględni się w tej liście,
            // dlatego, że ToList stworzyło nową referencję

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

        public User getUserData()
        {
            Console.WriteLine("Podaj login");
            string userLogin = Console.ReadLine();

            Console.WriteLine("Podaj hasło");
            string userPsswd = Console.ReadLine();
            
        }

        private Tuple<string, string> GetUserData(string message = null, string message2 = null)
        {

            Console.WriteLine(message);
            string login = Console.ReadLine();



            Console.WriteLine(message2);
            string passwd = Console.ReadLine();

            


            return Tuple.Create(login, passwd);
        }

        public bool IsUserExist()
        {
            var users = GetUsers();

            var userData = GetUserData("Podaj login", "Podaj hasło");

            bool isUserExist = _users.Any(us => us.login == userData.Item1 && us.passwd == userData.Item2);

            if (isUserExist)
            {
                return isUserExist;
            }
            else
            {
                Console.WriteLine("Nie Znaleziono użykownika");
                return isUserExist;
            }

        }

        public decimal CheckUserBalance()
        {
            var users = GetUsers();
            bool isUserExist = IsUserExist();

            if (isUserExist)
            {
                decimal balance = _users.Single(b => b.balance == users)
            }
        }

    }
}

