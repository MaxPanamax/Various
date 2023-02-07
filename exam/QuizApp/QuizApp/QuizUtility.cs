using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace QuizApp
{
    class QuizUtility
    {
        private Dictionary<string, string> themes;
        private Quiz quiz;
        List<string> questions;
        List<string> answers;
        List<string> t_answers;
        private int themeIndex;
        private int questionIndex;
        private bool flag;
        public QuizUtility(Dictionary<string, string> themes)
        {
            quiz = new Quiz();
            this.themes = themes;
            questions = quiz.questions;
            answers = quiz.answers;
            t_answers = quiz.t_answers;
            flag = true;
        }
        public void Run()
        {
            Menu menu = new Menu("    МЕНЮ", new List<string>() { "Создать викторину", "Редактировать викторину", "Удалить викторину", "Выход" });
            while (true)
            {
                switch (menu.Run())
                {
                    case 0:
                        CreateQuiz();
                        break;
                    case 1:
                        EditQuiz();
                        break;
                    case 2:
                        DeleteQuiz();
                        break;
                    case 3:
                        Exit();
                        break;
                    default:
                        break;
                }
            }
        }

        public void CreateQuiz()
        {
            List<string> listOfQuestions = new List<string>();
            List<string> listOfAnswers = new List<string>();
            List<string> listOfTAnswers = new List<string>();
            List<string> temp = new List<string>();
            string question;
            string answer;
            string tAnswer;
            string nameThemeRus;
            string nameThemeEn;
            Console.Clear();
            Console.Write("Введите название темы викторины на русском (Например: 'История'): ");
            nameThemeRus = Console.ReadLine();
            if (nameThemeRus == "")
            {
                Console.WriteLine("Путой ввод");
                Thread.Sleep(2000);
                return;
            }
            if (themes.ContainsKey(nameThemeRus))
            {
                Console.WriteLine("Такое название уже есть!");
                Thread.Sleep(2000);
                return;
            }

            Console.Write("Введите название темы викторины на английском (Например: 'history'): ");
            nameThemeEn = Console.ReadLine();
            if (nameThemeEn == "")
            {
                Console.WriteLine("Путой ввод");
                Thread.Sleep(2000);
                return;
            }
            if (themes.ContainsValue(nameThemeEn))
            {
                Console.WriteLine("Такое название уже есть!");
                Thread.Sleep(2000);
                return;
            }

            for (int i = 0; i < 20; i++)
            {
                Console.Clear();
                Console.WriteLine($"Введите {i + 1} вопрос: ");
                question = Console.ReadLine();
                if (question == "")
                {
                    i--;
                    Console.WriteLine("\nПустой ввод");
                    Thread.Sleep(2000);
                    continue;
                }
                listOfQuestions.Add(question);
                while (true)
                {
                    Console.Clear();
                    Console.Write("Введите количество вариантов ответа: ");
                    if (!int.TryParse(Console.ReadLine(), out int value))
                    {
                        Console.WriteLine("Ошибка ввода.");
                        Thread.Sleep(2000);
                        continue;
                    }
                    else
                    {
                        for (int j = 0; j < value; j++)
                        {
                            Console.Clear();
                            Console.Write($"Введите {j + 1} ответ: ");
                            answer = Console.ReadLine();
                            if (answer == "")
                            {
                                j--;
                                Console.WriteLine("\nПустой ввод");
                                Thread.Sleep(2000);
                                continue;
                            }
                            temp.Add(answer);
                        }
                        TransformList(temp);
                        break;
                    }
                }
                listOfAnswers.Add(string.Join("", temp));
                temp.Clear();
                Console.Clear();
                Console.Write("Введите правильный ответ: ");
                tAnswer = Console.ReadLine();
                if (tAnswer == "")
                {
                    i--;
                    Console.WriteLine("\nПустой ввод");
                    Thread.Sleep(2000);
                    continue;
                }
                listOfTAnswers.Add(tAnswer);
            }
            Menu menu = new Menu("    СОЗДАТЬ ВИКТОРИНУ?", new List<string>() { "Да", "Нет" });
            if (menu.Run() == 0)
            {
                List<string> tempList = new List<string>();
                using (var sr = new StreamReader("themes_rus.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        tempList.Add(sr.ReadLine());
                    }
                }
                tempList.Add(nameThemeRus);
                File.WriteAllText("themes_rus.txt", string.Join("\n", tempList));
                tempList.Clear();

                using (var sr = new StreamReader("themes_en.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        tempList.Add(sr.ReadLine());
                    }
                }
                tempList.Add(nameThemeEn);
                File.WriteAllText("themes_en.txt", string.Join("\n", tempList));
                Directory.CreateDirectory($"{nameThemeEn}");
                File.WriteAllText($"{nameThemeEn}\\questions.txt", string.Join("\n", listOfQuestions));
                File.WriteAllText($"{nameThemeEn}\\answers.txt", string.Join("\n", listOfAnswers));
                File.WriteAllText($"{nameThemeEn}\\t_answers.txt", string.Join("\n", listOfTAnswers));

                tempList.Clear();
                using (var sr = new StreamReader("results.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        tempList.Add(sr.ReadLine());
                    }
                }
                for (int i = 0; i < tempList.Count; i++)
                {
                    tempList[i] += " 0";
                }
                File.WriteAllText("results.txt", string.Join("\n", tempList));
            }
        }
        public void TransformList(List<string> temp)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                temp[i] = temp[i].Insert(0, $"{i + 1}) ");
                temp[i] += "_";
            }
        }

        public void EditQuiz()
        {
            flag = true;
            Menu menu = new Menu("    ТЕМЫ", themes.Keys.ToList());
            themeIndex = menu.Run();
            quiz.ReadThemes();
            quiz.ReadQuestions(themeIndex);
            Menu edit = new Menu("", new List<string>() { "Редактировать вопрос", "Редактировать варианты ответов",
                                                          "Редактировать правильный ответ", "Добавить вариант ответа","Назад"});
            while (flag)
            {
                flag = true;
                switch (edit.Run())
                {
                    case 0:
                        EditQuestion();
                        break;
                    case 1:
                        EditAnswer();
                        break;
                    case 2:
                        EditTAnswer();
                        break;
                    case 3:
                        AddAnswer();
                        break;
                    case 4:
                        flag = false;
                        break;
                    default:
                        break;
                }
            }
        }
        public void AddAnswer()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Введите номер вопроса: ");
                if (int.TryParse(Console.ReadLine(), out questionIndex) && questionIndex > 0 && questionIndex < 21)
                {
                    questionIndex -= 1;
                    ShowQuestion();
                    List<string> listOfAnswers = answers[questionIndex].Split('_').ToList();
                    for (int i = 0; i < listOfAnswers.Count - 1; i++)
                    {
                        listOfAnswers[i] += "_";
                    }
                    Console.Write("Введите новый ответ: ");
                    listOfAnswers.Add($"{listOfAnswers.Count}) {Console.ReadLine()}_");
                    Menu edit = new Menu("    ПОДТВЕРДИТЬ ИЗМЕНЕНИЯ?", new List<string>() { "Да", "Нет" });
                    if (edit.Run() == 0)
                    {
                        answers[questionIndex] = string.Join("", listOfAnswers);
                        File.WriteAllText($"{themes[themes.ElementAt(themeIndex).Key]}\\answers.txt", string.Join("\n", answers));
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                    Thread.Sleep(2000);
                    continue;
                }
            }
        }

        public void EditAnswer()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Введите номер вопроса: ");
                if (int.TryParse(Console.ReadLine(), out questionIndex) && questionIndex > 0 && questionIndex < 21)
                {
                    questionIndex -= 1;
                    ShowQuestion();
                    List<string> listOfAnswers = answers[questionIndex].Split('_').ToList();
                    Console.Write("Введите номер ответа: ");
                    if (int.TryParse(Console.ReadLine(), out int answerIndex) && answerIndex > 0 && answerIndex < listOfAnswers.Count)
                    {
                        answerIndex -= 1;
                        Console.WriteLine("Введите новый вариант ответа: ");
                        string temp = Console.ReadLine();
                        listOfAnswers[answerIndex] = $"{answerIndex + 1}) " + temp;
                        for (int i = 0; i < listOfAnswers.Count - 1; i++)
                        {
                            listOfAnswers[i] += "_";
                        }
                        Menu edit = new Menu("    ПОДТВЕРДИТЬ ИЗМЕНЕНИЯ?", new List<string>() { "Да", "Нет" });
                        if (edit.Run() == 0)
                        {
                            answers[questionIndex] = string.Join("", listOfAnswers);
                            File.WriteAllText($"{themes[themes.ElementAt(themeIndex).Key]}\\answers.txt", string.Join("\n", answers));
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода");
                        Thread.Sleep(2000);
                        continue;
                    }

                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                    Thread.Sleep(2000);
                    continue;
                }
            }
        }
        public void EditQuestion()
        {

            while (true)
            {
                Console.Clear();
                Console.Write("Введите номер вопроса: ");
                if (int.TryParse(Console.ReadLine(), out questionIndex) && questionIndex > 0 && questionIndex < 21)
                {
                    questionIndex -= 1;
                    ShowQuestion();
                    string saveQuestion = questions[questionIndex];
                    Console.WriteLine("Введите новый вопрос: ");
                    questions[questionIndex] = Console.ReadLine();
                    Menu edit = new Menu("    ПОДТВЕРДИТЬ ИЗМЕНЕНИЯ?", new List<string>() { "Да", "Нет" });
                    if (edit.Run() == 0)
                    {
                        File.WriteAllText($"{themes[themes.ElementAt(themeIndex).Key]}\\questions.txt", string.Join("\n", questions));
                        break;
                    }
                    else
                    {
                        questions[questionIndex] = saveQuestion;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                    Thread.Sleep(2000);
                    continue;
                }
            }
        }
        public void EditTAnswer()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Введите номер вопроса: ");
                if (int.TryParse(Console.ReadLine(), out questionIndex) && questionIndex > 0 && questionIndex < 21)
                {
                    questionIndex -= 1;
                    ShowQuestion();
                    string saveAnswer = t_answers[questionIndex];
                    Console.Write("Введите ответ: ");
                    t_answers[questionIndex] = Console.ReadLine();
                    Menu edit = new Menu("    ПОДТВЕРДИТЬ ИЗМЕНЕНИЯ?", new List<string>() { "Да", "Нет" });
                    if (edit.Run() == 0)
                    {
                        File.WriteAllText($"{themes[themes.ElementAt(themeIndex).Key]}\\t_answers.txt", string.Join("\n", t_answers));
                        break;
                    }
                    else
                    {
                        t_answers[questionIndex] = saveAnswer;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода");
                    Thread.Sleep(2000);
                    continue;
                }
            }
        }
        public void ShowQuestion()
        {
            Console.Clear();
            Console.WriteLine($"{questionIndex + 1}. {questions[questionIndex]}\n");
            List<string> listOfAnswers = answers[questionIndex].Split('_').ToList();
            foreach (var answer in listOfAnswers)
            {
                Console.WriteLine(answer);
            }
            Console.WriteLine($"Правильный ответ: {t_answers[questionIndex]}");
            listOfAnswers.Clear();
        }
        public void DeleteQuiz()
        {
            Menu menu = new Menu("    ТЕМЫ", themes.Keys.ToList());
            themeIndex = menu.Run();
            Menu actions = new Menu("    УДАЛИТЬ ВИКТОРИНУ?", new List<string> { "Да", "Нет" });
            if (actions.Run() == 0)
            {
                Directory.Delete($"{themes[themes.ElementAt(themeIndex).Key]}", true);
                themes.Remove(themes.ElementAt(themeIndex).Key);
                File.WriteAllText("themes_rus.txt", string.Join("\n", themes.Keys));
                File.WriteAllText("themes_en.txt", string.Join("\n", themes.Values));
                List<string> tempList = new List<string>();
                using (var sr = new StreamReader("results.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        tempList.Add(sr.ReadLine());
                    }
                }
                List<List<string>> points = new List<List<string>>();
                for (int i = 0; i < tempList.Count; i++)
                {
                    points.Add(tempList[i].Split(' ').ToList());
                }
                for (int i = 0; i < points.Count; i++)
                {
                    points[i].Remove(points[i][points.Count-1]);
                }
                tempList.Clear();
                for (int i = 0; i < points.Count; i++)
                {
                    tempList.Add(string.Join(" ", points[i]));
                }
                File.WriteAllText("results.txt", string.Join("\n", tempList));
            }
        }
        public void Exit()
        {
            Menu exit = new Menu("    ХОТИТЕ ВЫЙТИ?", new List<string> { "Да", "Нет" });
            if (exit.Run() == 0)
                Environment.Exit(0);
        }
    }
}

