using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace QuizApp
{
    class Authentication
    {
        private Dictionary<string, string> userData;
        private string adminLogin;
        private string adminPassword;
        private string userLogin;
        private string userPassword;
        private bool isAuthentication;
        private int numberOfThemes;
        public string Birthday { get; set; }
        public string LoginKey { get; private set; }
        public Authentication()
        {
            userData = new Dictionary<string, string>();
            isAuthentication = false;
        }
        public Dictionary<string, string> GetUserData()
        {
            return userData;
        }
        public void ReadUserData()
        {
            using (var sr = new StreamReader($"logins.txt"))
            {
                while (!sr.EndOfStream)
                {
                    userData.Add(sr.ReadLine(), "");
                }
            }
            adminLogin = userData.ElementAt(0).Key;
            using (var sr = new StreamReader($"passwords.txt"))
            {
                for (int i = 0; i < userData.Count; i++)
                {
                    userData[userData.ElementAt(i).Key] = sr.ReadLine();
                }
            }
            adminPassword = userData[userData.ElementAt(0).Key];
            userData.Remove(userData.ElementAt(0).Key);
        }
        public bool AuthenticationMenu(int number)
        {
            ReadUserData();
            numberOfThemes = number;
            Menu menu = new Menu("    ВИКТОРИНА", new List<string> { "Авторизация", "Регистрация"});
            while (!isAuthentication)
            {       
                if (menu.Run() == 0)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Введите логин: ");
                        userLogin = Console.ReadLine();
                        PasswordMask(ref userPassword, "Введите пароль: ");
                        Console.WriteLine();
                        if (userLogin == adminLogin && userPassword == adminPassword)
                            return true;
                        isAuthentication = Authorization();
                        if (!isAuthentication)
                        {
                            string warning = $"Неверный логин или пароль.";
                            Menu confirmation = new Menu(warning, new List<string> { "Новый ввод", "Назад" });
                            if (confirmation.Run() == 0)
                                continue;
                            else
                                break;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.Write("Введите логин: ");
                        userLogin = Console.ReadLine();
                        if (userLogin.Length < 3)
                        {
                            Console.WriteLine("Длина логина должна быть не менее 3 символов!");
                            Thread.Sleep(2000);
                            continue;
                        }
                        if (!CheckLogin())
                        {
                            Console.WriteLine("Такой логин уже есть!");
                            Thread.Sleep(2000);
                            continue;
                        }
                        Console.Clear();
                        PasswordMask(ref userPassword, "Введите пароль: ");
                        Console.WriteLine();
                        if (userPassword.Length < 4)
                        {
                            Console.WriteLine("Длина пароля должна быть не менее 4 символов!");
                            Thread.Sleep(2000);
                            continue;
                        }
                        string repeatPassword = "";
                        Console.Clear();
                        PasswordMask(ref repeatPassword, "Повторите пароль: ");
                        Console.WriteLine();
                        if (userPassword != repeatPassword)
                        {
                            Console.WriteLine("Несовпадение паролей!");
                            Thread.Sleep(2000);
                            continue;
                        }
                        DateOfBirth();
                        break;
                    }
                    Registration();
                    Console.WriteLine("\nРегистрация завершена!\nНажмите ENTER для продолжения.");
                    Console.ReadLine();
                }
            }
            return false;
        }
        public void PasswordMask(ref string input, string message)
        {
            ConsoleKey keyPressed;
            input = "";
            string value = "";
            while (true)
            {
                Console.Clear();
                Console.Write(message + value);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyPressed == ConsoleKey.Backspace)
                {
                    if (value.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        value = value.Remove(value.Length - 1);
                        input = input.Remove(input.Length - 1);
                    }

                }
                else
                {
                    input += keyInfo.KeyChar;
                    value += '*';
                }
            }
        }
        public void DateOfBirth()
        {
            Birthday = "";
            ConsoleKey keyPressed;
            while (true)
            {
                while (true)
                {
                    Console.Clear();
                    if (Birthday.Length == 2)
                        Birthday += ".";
                    if (Birthday.Length == 5)
                        Birthday += ".";
                    Console.Write("Введите дату рождения: " + Birthday);
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    keyPressed = keyInfo.Key;

                    if (keyPressed == ConsoleKey.Enter)
                    {
                        if (Birthday.Length == 10)
                            break;
                    }
                    else if (keyPressed == ConsoleKey.Backspace)
                    {
                        if (Birthday.Length != 0 && Birthday[Birthday.Length - 1] == '.')
                            Birthday = Birthday.Remove(Birthday.Length - 1);
                        if (Birthday.Length == 0)
                            continue;
                        else
                            Birthday = Birthday.Remove(Birthday.Length - 1);
                    }
                    else
                    {
                        Birthday += keyInfo.KeyChar;
                        if (Birthday.Length > 10)
                            Birthday = Birthday.Remove(Birthday.Length - 1);
                    }
                }
                DateTime dDate;

                if (DateTime.TryParse(Birthday, out dDate))
                {
                    String.Format("{0:d/MM/yyyy}", dDate);
                    break;
                }
                else
                {
                    Console.WriteLine("\nНеверный формат даты.");
                    Thread.Sleep(2000);
                    continue;
                }
            }

        }
        public bool Authorization()
        {
            foreach (var item in userData)
            {
                if (userLogin == item.Key && userPassword == item.Value)
                {
                    LoginKey = userLogin;
                    return true;
                }
            }
            return false;
        }
        public void Registration()
        {
            List<string> temp = new List<string>();
            using (var sr = new StreamReader($"logins.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp.Add(userLogin);
            File.WriteAllText($"logins.txt", string.Join("\n", temp));
            temp.Clear();

            using (var sr = new StreamReader($"passwords.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp.Add(userPassword);
            File.WriteAllText($"passwords.txt", string.Join("\n", temp));
            temp.Clear();

            using (var sr = new StreamReader($"datesOfBirth.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp.Add(Birthday);
            File.WriteAllText($"datesOfBirth.txt", string.Join("\n", temp));
            temp.Clear();

            using (var sr = new StreamReader($"results.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            List<int> list = new List<int>();
            for (int i = 0; i < numberOfThemes; i++)
            {
                list.Add(0);
            }
            temp.Add(string.Join(" ", list));
            File.WriteAllText($"results.txt", string.Join("\n", temp));
            temp.Clear();
            using (var sr = new StreamReader($"results_mixed_quiz.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp.Add("0");
            File.WriteAllText($"results_mixed_quiz.txt", string.Join("\n", temp));
            userData.Clear();
            ReadUserData();
        }
        public bool CheckLogin()
        {
            foreach (var item in userData)
            {
                if (userLogin == item.Key)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
