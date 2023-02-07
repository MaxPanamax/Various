using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QuizApp
{
    class Quiz
    {
        private Dictionary<string, string> themes;
        public List<string> questions;
        public List<string> answers;
        public List<string> t_answers;
        private List<string> userAnswers;
        public int NumberOfThemes { get; private set; }
        public int ThemeNumber { get; private set; }
        public bool IsMixed { get; private set; }
        public Quiz()
        {
            themes = new Dictionary<string, string>();
            questions = new List<string>();
            answers = new List<string>();
            t_answers = new List<string>();
            userAnswers = new List<string>();
        }
        public Dictionary<string, string> GetThemes()
        {
            return themes;
        }
        public void ReadThemes()
        {
            themes.Clear();
            using (var sr = new StreamReader("themes_rus.txt"))
            {
                while (!sr.EndOfStream)
                {
                    themes.Add(sr.ReadLine(), "");
                }
            }
            using (var sr = new StreamReader("themes_en.txt"))
            {
                for (int i = 0; i < themes.Count; i++)
                {
                    themes[themes.ElementAt(i).Key] = sr.ReadLine();
                }
            }
            NumberOfThemes = themes.Count;
        }
        public void ReadQuestions(int selectedIndex)
        {
            questions.Clear();
            answers.Clear();
            t_answers.Clear();

            using (var sr = new StreamReader($"{themes[themes.ElementAt(selectedIndex).Key]}\\t_answers.txt"))
            {
                while (!sr.EndOfStream)
                {
                    t_answers.Add(sr.ReadLine());
                }
            }
            using (var sr = new StreamReader($"{themes[themes.ElementAt(selectedIndex).Key]}\\questions.txt"))
            {
                while (!sr.EndOfStream)
                {
                    questions.Add(sr.ReadLine());
                }
            }
            using (var sr = new StreamReader($"{themes[themes.ElementAt(selectedIndex).Key]}\\answers.txt"))
            {
                while (!sr.EndOfStream)
                {
                    answers.Add(sr.ReadLine());
                }
            }
        }
        public bool MixedQuiz()
        {
            IsMixed = true;
            Console.Clear();
            ReadAllQuizData();
            RunQuiz();
            return false;
        }
        public void ReadAllQuizData()
        {
            questions.Clear();
            answers.Clear();
            t_answers.Clear();

            List<int> list = new List<int>();
            Random random = new Random();
            list = Enumerable.Range(0, NumberOfThemes * 20 - 1).OrderBy(n => random.Next()).ToList();
            list.RemoveRange(20, list.Count - 20);
            List<string> questionsList = new List<string>();
            List<string> answersList = new List<string>();
            List<string> t_answersList = new List<string>();
            for (int i = 0; i < themes.Count; i++)
            {
                using (var sr = new StreamReader($"{themes.ElementAt(i).Value}\\questions.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        questionsList.Add(sr.ReadLine());
                    }
                }
            }
            for (int i = 0; i < themes.Count; i++)
            {
                using (var sr = new StreamReader($"{themes.ElementAt(i).Value}\\answers.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        answersList.Add(sr.ReadLine());
                    }
                }
            }
            for (int i = 0; i < themes.Count; i++)
            {
                using (var sr = new StreamReader($"{themes.ElementAt(i).Value}\\t_answers.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        t_answersList.Add(sr.ReadLine());
                    }
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                questions.Add(questionsList[list[i]]);
            }
            for (int i = 0; i < list.Count; i++)
            {
                answers.Add(answersList[list[i]]);
            }
            for (int i = 0; i < list.Count; i++)
            {
                t_answers.Add(t_answersList[list[i]]);
            }

        }
        public bool Menu()
        {
            Console.Clear();
            List<string> listOfThemes = themes.Keys.ToList();
            listOfThemes.Add("Назад");
            Menu menu = new Menu("    ТЕМЫ", listOfThemes);
            int selectedIndex = menu.Run();
            if (selectedIndex != listOfThemes.Count - 1)
            {
                IsMixed = false;
                ThemeNumber = selectedIndex;
                ReadQuestions(selectedIndex);
                RunQuiz();
                return false;
            }
            else
            {
                return true;
            }
        }
        public void RunQuiz()
        {
            int index = 0;
            foreach (var item in questions)
            {
                Console.Clear();
                Console.WriteLine($"{++index}. {item}\n");
                List<string> listOfAnswers = answers[index - 1].Split('_').ToList();
                foreach (var answer in listOfAnswers)
                {
                    Console.WriteLine(answer);
                }
                Console.Write("Ввод: ");
                userAnswers.Add(Console.ReadLine());
                listOfAnswers.Clear();
            }
            Console.Clear();
        }
        public int CountingCorrectAnswers()
        {
            int countCorrectAnswers = 0;
            for (int i = 0; i < t_answers.Count; i++)
            {
                if (userAnswers[i] == t_answers[i])
                    countCorrectAnswers++;
            }
            return countCorrectAnswers;
        }
        public void Statistics(int countCorrectAnswers, string login, Dictionary<string, string> userData)
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
                userResults.Add(userData.ElementAt(i).Key, int.Parse(results[i][ThemeNumber]));
            }
            int index = 0;
            Console.Clear();
            Console.WriteLine($"Правильно отвеченные вопросы: {countCorrectAnswers}");
            Console.Write("Место в таблице результатов: ");
            foreach (var item in userResults.OrderByDescending(p => p.Value))
            {
                ++index;
                if (item.Key == login)
                {
                    Console.WriteLine(index);
                    break;
                }
            }
            Console.WriteLine("\nНажмите ENTER для продолжения.");
            Console.ReadLine();
        }
        public void Statistics(string login, int countCorrectAnswers, Dictionary<string, string> userData)
        {
            List<int> usersData = new List<int>();
            using (var sr = new StreamReader($"results_mixed_quiz.txt"))
            {
                while (!sr.EndOfStream)
                {
                    usersData.Add(int.Parse(sr.ReadLine()));
                }
            }
            Dictionary<string, int> userResults = new Dictionary<string, int>();
            for (int i = 0; i < userData.Count; i++)
            {
                userResults.Add(userData.ElementAt(i).Key, usersData[i]);
            }
            int index = 0;
            Console.Clear();
            Console.WriteLine($"Правильно отвеченные вопросы: {countCorrectAnswers}");
            Console.Write("Место в таблице результатов: ");
            foreach (var item in userResults.OrderByDescending(p => p.Value))
            {
                ++index;
                if (item.Key == login)
                {
                    Console.WriteLine(index);
                    break;
                }
            }
            Console.WriteLine("\nНажмите ENTER для продолжения.");
            Console.ReadLine();
        }
    }
}
