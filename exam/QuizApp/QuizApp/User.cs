using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace QuizApp
{
    class User
    {
        private Dictionary<string, string> themes;
        private Dictionary<string, string> userData;
        private string login;
        private bool flag;

        Authentication authentication;
        public User(Dictionary<string, string> themes, Authentication authentication)
        {
            this.authentication = authentication;
            this.themes = themes;
            flag = true;
        }
        public void Run(string login, Dictionary<string, string> userData, Quiz quiz)
        {
            flag = true;
            this.login = login;
            this.userData = userData;
            List<string> items = new List<string>() { "Начать новую викторину", "Моя статистика", "Топ-20", "Настройки", "Выход"};
            Menu menu = new Menu("    МЕНЮ", items);
            while (flag)
            {
                switch (menu.Run())
                {
                    case 0:
                        Quizzes(quiz);
                        break;
                    case 1:
                        Statistics();
                        break;
                    case 2:
                        Top();
                        break;
                    case 3:
                        Settings();
                        break;
                    case 4:
                        Menu exit = new Menu("    ХОТИТЕ ВЫЙТИ?", new List<string> { "Да", "Нет"});
                        if (exit.Run() == 0)
                            Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        public void Quizzes(Quiz quiz)
        {
            bool isThemes = true;
            List<string> quizzes = new List<string>() { "Тематические викторины", "Смешанная викторина", "Назад" };
            Menu menu = new Menu("    ВИКТОРИНЫ", quizzes);
            while (isThemes)
            {
                flag = true;
                switch (menu.Run())
                {
                    case 0:
                        isThemes = quiz.Menu();
                        flag = false;
                        break;
                    case 1:
                        isThemes = quiz.MixedQuiz();
                        flag = false;
                        break;
                    case 2:
                        isThemes = false;
                        break;
                    default:
                        break;
                }
            }
        }
        private void Settings()
        {
            Menu menu = new Menu("    НАСТРОЙКИ", new List<string> { "Изменить пароль", "Изменить дату рождения", "Назад"});
            switch (menu.Run())
            {
                case 0:
                    ChangePassword();
                    break;
                case 1:
                    ChangeDateOfBirth();
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
        private void ChangeDateOfBirth()
        {
            authentication.DateOfBirth();
            List<string> temp = new List<string>();
            using (var sr = new StreamReader($"datesOfBirth.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp[userData.Keys.ToList().IndexOf(login)] = authentication.Birthday;
            File.WriteAllText($"datesOfBirth.txt", string.Join("\n", temp));
        }
        private void ChangePassword()
        {
            Console.Clear();
            string userPassword = "";
            string newPassword = "";
            List<string> listOfPasswords = new List<string>();
            using (var sr = new StreamReader($"passwords.txt"))
            {
                while (!sr.EndOfStream)
                {
                    listOfPasswords.Add(sr.ReadLine());
                }
            }
            while (true)
            {
                authentication.PasswordMask(ref userPassword, "Введите старый пароль: ");
                if (userPassword == userData[login])
                {
                    authentication.PasswordMask(ref newPassword, "Введите новый пароль: ");
                    if (newPassword.Length < 4)
                    {
                        Console.WriteLine("\nДлина пароля должна быть не менее 4 символов!");
                        Thread.Sleep(2000);
                        continue;
                    }
                    listOfPasswords[userData.Keys.ToList().IndexOf(login)] = newPassword;
                    File.WriteAllText($"passwords.txt", string.Join("\n", listOfPasswords));
                    Console.WriteLine("\nНовый пароль установлен!\nДанные обновяться при следующем входе в систему.\n\nНажмите ENTER для продолжения.");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    Menu confirmation = new Menu("Неверный пароль", new List<string> { "Новый ввод", "Назад" });
                    if (confirmation.Run() == 0)
                        continue;
                    else
                        break;
                }
            }
        }
        private void Top()
        {

            List<string> listOfThemes = themes.Keys.ToList();
            listOfThemes.Add("Смешанная");
            listOfThemes.Add("Назад");
            Menu menu = new Menu("    ТЕМЫ", listOfThemes);
            int selectedIndex;
            while (true)
            {
                selectedIndex = menu.Run();
                if (selectedIndex != listOfThemes.Count - 1 && selectedIndex != listOfThemes.Count - 2)
                {
                    List<string> usersData = new List<string>();
                    using (var sr = new StreamReader($"results.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            usersData.Add(sr.ReadLine());
                        }
                    }
                    List<List<string>> results = new List<List<string>>();
                    for (int i = 0; i < usersData.Count; i++)
                    {
                        results.Add(usersData[i].Split(' ').ToList());
                    }
                    Dictionary<string, int> userResults = new Dictionary<string, int>();
                    for (int i = 0; i < results.Count; i++)
                    {
                        userResults.Add(userData.ElementAt(i).Key, int.Parse(results[i][selectedIndex]));
                    }
                    int index = 0;
                    Console.Clear();
                    Console.WriteLine("{0,20}", "ТОП-20");
                    foreach (var item in userResults.OrderByDescending(p => p.Value))
                    {
                        Console.WriteLine("{0,-10} {1,-20} {2,0}", $"{++index}.", $"{item.Key}", $"{item.Value}");
                        if (index == 20)
                            break;
                    }
                    Console.WriteLine("\nНажмите ENTER для продолжения.");
                    Console.ReadLine();
                }
                if (selectedIndex == listOfThemes.Count - 2)
                {
                    List<string> usersData = new List<string>();
                    using (var sr = new StreamReader($"results_mixed_quiz.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            usersData.Add(sr.ReadLine());
                        }
                    }
                    Dictionary<string, int> userResults = new Dictionary<string, int>();
                    for (int i = 0; i < userData.Count; i++)
                    {
                        userResults.Add(userData.ElementAt(i).Key, int.Parse(usersData[i]));
                    }
                    int index = 0;
                    Console.Clear();
                    Console.WriteLine("{0,20}", "ТОП-20");
                    foreach (var item in userResults.OrderByDescending(p => p.Value))
                    {
                        Console.WriteLine("{0,-10} {1,-20} {2,0}", $"{++index}.", $"{item.Key}", $"{item.Value}");
                        if (index == 20)
                            break;
                    }
                    Console.WriteLine("\nНажмите ENTER для продолжения.");
                    Console.ReadLine();
                }
                if (selectedIndex == listOfThemes.Count - 1)
                    break;
            }


        }
        private void Statistics()
        {
            Console.Clear();
            Console.WriteLine("{0,20}","СТАТИСТИКА");
            List<string> usersData = new List<string>();
            using (var sr = new StreamReader($"results.txt"))
            {
                while (!sr.EndOfStream)
                {
                    usersData.Add(sr.ReadLine());
                }
            }
            string userStr = usersData[userData.Keys.ToList().IndexOf(login)];
            List<string> points = new List<string>();
            points = userStr.Split(' ').ToList();
            int index = 0;
            for (int i = 0; i < themes.Count; i++)
            {
                Console.WriteLine("{0,-10} {1,-20} {2,0}", $"{++index}.", $"{themes.ElementAt(i).Key}", $"{points[i]}");
            }
            points.Clear();
            using (var sr = new StreamReader($"results_mixed_quiz.txt"))
            {
                while (!sr.EndOfStream)
                {
                    points.Add(sr.ReadLine());
                }
            }
            Console.WriteLine("{0,-10} {1,-20} {2,0}", $"{++index}.", "Смешанная", $"{points[userData.Keys.ToList().IndexOf(login)]}");
            Console.WriteLine("\nНажмите ENTER для продолжения.");
            Console.ReadLine();
        }
        public void UpdateResult(int countCorrectsAnswers, int themeNumber)
        {
            List<string> usersResults = new List<string>();
            using (var sr = new StreamReader($"results.txt"))
            {
                while (!sr.EndOfStream)
                {
                    usersResults.Add(sr.ReadLine());
                }
            }
            int index = userData.Keys.ToList().IndexOf(login);
            List<string> points = new List<string>();
            points = usersResults[index].Split(' ').ToList();
            if (countCorrectsAnswers > int.Parse(points[themeNumber]))
            {
                points[themeNumber] = countCorrectsAnswers.ToString();
                usersResults[index] = string.Join(" ", points);
                File.WriteAllText($"results.txt", string.Join("\n", usersResults));
            }
        }
        public void UpdateResult(int countCorrectsAnswers)
        {
            List<string> usersResults = new List<string>();
            using (var sr = new StreamReader($"results_mixed_quiz.txt"))
            {
                while (!sr.EndOfStream)
                {
                    usersResults.Add(sr.ReadLine());
                }
            }
            int index = userData.Keys.ToList().IndexOf(login);
            if (countCorrectsAnswers > int.Parse(usersResults[index]))
            {
                usersResults[index] = countCorrectsAnswers.ToString();
                File.WriteAllText($"results_mixed_quiz.txt", string.Join("\n", usersResults));
            }
        }
    }
}
