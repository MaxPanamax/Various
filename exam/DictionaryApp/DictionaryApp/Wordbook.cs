using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DictionaryApp
{
    class Wordbook
    {
        private List<string> listOfDictionaries;
        private Dictionary<string, string> dictionary;
        private bool flag; 
        public Wordbook()
        {
            listOfDictionaries = new List<string>();
            dictionary = new Dictionary<string, string>();
            flag = false;
        }
        public void ReadListOfDictionaries()
        {
            using (var sr = new StreamReader("listOfDictionaries.txt"))
            {
                while (!sr.EndOfStream)
                {
                    listOfDictionaries.Add(sr.ReadLine());
                }
            }
            listOfDictionaries.Add("Назад");
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                List<string> options = new List<string> { "Список словарей", "Создать новый словарь", "Удалить словарь", "Выход" };

                Menu mainMenu = new Menu("    СЛОВАРИ", options);

                int selectedIndex = mainMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        DictionariesMenu();
                        break;
                    case 1:
                        CreateDictionary();
                        break;
                    case 2:
                        DeleteDictionary();
                        break;
                    case 3:
                        Menu exit = new Menu("    ХОТИТЕ ВЫЙТИ?", new List<string> { "Да", "Нет" });
                        if (exit.Run() == 0)
                            Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }
        public void DictionariesMenu()
        {
            while (true)
            {
                Console.Clear();
                Menu dict = new Menu("    СПИСОК СЛОВАРЕЙ", listOfDictionaries);
                int selectedIndex = dict.Run();
                if (selectedIndex == listOfDictionaries.Count - 1)
                {
                    break;
                }
                else
                {
                    Console.Clear();
                    Menu confirmation = new Menu("    ОТКРЫТЬ СЛОВАРЬ? ", new List<string> { "Подтвердить", "Назад" });
                    if (confirmation.Run() == 0)
                        OpenDictionary(selectedIndex);
                    else
                        continue;
                }
            }
        }
        public void OpenDictionary(int index)
        {
            flag = false;
            Console.Clear();
            using (var sr = new StreamReader($"words{index}.txt"))
            {
                while (!sr.EndOfStream)
                {
                    dictionary.Add(sr.ReadLine(), "");
                }
            }

            using (var sr = new StreamReader($"translation{index}.txt"))
            {
                for (int i = 0; i < dictionary.Count; i++)
                {
                    dictionary[dictionary.ElementAt(i).Key] = sr.ReadLine();
                }
            }
            DictionaryOptions options = new DictionaryOptions(dictionary);
            while (flag == false)
            {
                List<string> dictionaryOptions = new List<string> { "Поиск", "Добавить слово", "Добавить перевод", "Редактировать слово", 
                    "Редактировать перевод", "Удалить слово", "Удалить перевод", "Назад" };
                Menu actions = new Menu($"    ОТКРЫТ {listOfDictionaries[index]} СЛОВАРЬ", dictionaryOptions);
                switch (actions.Run())
                {
                    case 0:
                        while (true)
                        {
                            Menu confirmation = new Menu($"{options.Search()}\n\n    ПРОДОЛЖИТЬ ПОИСК?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                                continue;
                            else
                                break;
                        }
                        break;
                    case 1:
                        while (true)
                        {
                            string temp = options.AddWord();
                            if (temp == null)
                            {
                                Console.WriteLine("Такое слово уже есть. \nНажмите ENTER для продолжения.");
                                Console.ReadLine();
                                break;
                            }
                            Menu confirmation = new Menu($"{temp}\n\n    ДОБАВИТЬ СЛОВО?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                            {
                                options.AddWord(index);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 2:
                        while (true)
                        {
                            string temp = options.AddTranslation();
                            if (temp == null)
                            {
                                Console.WriteLine("Слово не найдено. \nНажмите ENTER для продолжения.");
                                Console.ReadLine();
                                break;
                            }
                            Menu confirmation = new Menu($"{temp}\n\n    ДОБАВИТЬ ПЕРЕВОД?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                            {
                                options.AddTranslation(index);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 3:
                        while (true)
                        {
                            string temp = options.RenameWord();
                            if (temp == null)
                            {
                                Console.WriteLine("Слово не найдено. \nНажмите ENTER для продолжения.");
                                Console.ReadLine();
                                break;
                            }
                            Menu confirmation = new Menu($"{temp}\n\n    РЕДАКТИРОВАТЬ СЛОВО?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                            {
                                options.RenameWord(index);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 4:
                        while (true)
                        {
                            List<string> temp = options.RenameTranslation();
                            if (temp == null)
                            {
                                Console.WriteLine("Слово не найдено. \nНажмите ENTER для продолжения.");
                                Console.ReadLine();
                                break;
                            }
                            Menu confirmation = new Menu($"    РЕДАКТИРОВАТЬ ПЕРЕВОД?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                            {
                                options.EditFile(index, temp);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 5:
                        while (true)
                        {
                            string temp = options.DeleteWord();
                            if (temp == null)
                            {
                                Console.WriteLine("Слово не найдено. \nНажмите ENTER для продолжения.");
                                Console.ReadLine();
                                break;
                            }
                            Menu confirmation = new Menu($"{temp}\n\n    УДАЛИТЬ СЛОВО?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                            {
                                options.DeleteTranslation(index);
                                options.DeleteWord(index);
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 6:
                        while (true)
                        {
                            List<string> temp = options.DeleteTranslation();
                            if (temp == null)
                            {
                                Console.WriteLine("Нельзя удалить перевод. \nНажмите ENTER для продолжения.");
                                Console.ReadLine();
                                break;
                            }
                            Menu confirmation = new Menu($"    УДАЛИТЬ ПЕРЕВОД?", new List<string> { "Подтвердить", "Назад" });
                            if (confirmation.Run() == 0)
                            {
                                if (temp == null)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\nОперация невозможна. У слова только один перевод.\nНажмите ENTER для продолжения.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    options.EditFile(index, temp);
                                }
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    case 7:
                        dictionary.Clear();
                        flag = true;
                        break;
                    default:
                        break;
                }
            }

        }
        public void 0()
        {
            Console.Clear();
            Console.Write("Введите название словаря: ");
            string nameOfDictionary = Console.ReadLine();
            Menu confirmation = new Menu("    СОЗДАТЬ СЛОВАРЬ? ", new List<string> { "Подтвердить", "Назад" });
            switch (confirmation.Run())
            {
                case 0:
                    List<string> temp = new List<string>();
                    using (var sr = new StreamReader($"listOfDictionaries.txt"))
                    {
                        while (!sr.EndOfStream)
                        {
                            temp.Add(sr.ReadLine());
                        }
                    }
                    temp.Add(nameOfDictionary);
                    File.WriteAllText($"listOfDictionaries.txt", string.Join("\n", temp));
                    using (FileStream fs = File.Create($"words{listOfDictionaries.Count - 1}.txt")) { }
                    using (FileStream fs = File.Create($"translation{listOfDictionaries.Count - 1}.txt")) { }
                    listOfDictionaries.Insert(listOfDictionaries.Count - 1, nameOfDictionary);
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
        void DeleteDictionary()
        {
            while (true)
            {
                Console.Clear();
                Menu dict = new Menu("    СПИСОК СЛОВАРЕЙ", listOfDictionaries);
                int selectedIndex = dict.Run();
                if (selectedIndex != listOfDictionaries.Count - 1)
                {
                    Menu confirmation = new Menu($"    УДАЛИТЬ {listOfDictionaries[selectedIndex]} СЛОВАРЬ? ", new List<string> { "Подтвердить", "Назад" });
                    if (confirmation.Run() == 0)
                    {
                        var tempFile = Path.GetTempFileName();
                        var linesToKeep = File.ReadLines("listOfDictionaries.txt").Where(l => l != listOfDictionaries[selectedIndex]);
                        File.WriteAllLines(tempFile, linesToKeep);
                        File.Delete("listOfDictionaries.txt");
                        File.Move(tempFile, "listOfDictionaries.txt");
                        File.Delete($"words{selectedIndex}.txt");
                        File.Delete($"translation{selectedIndex}.txt");
                        if (selectedIndex != listOfDictionaries.Count - 2)
                        {
                            File.Move($"words{selectedIndex + 1}.txt", $"words{selectedIndex}.txt");
                            File.Move($"translation{selectedIndex + 1}.txt", $"translation{selectedIndex}.txt");
                        }
                        listOfDictionaries.RemoveAt(selectedIndex);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
