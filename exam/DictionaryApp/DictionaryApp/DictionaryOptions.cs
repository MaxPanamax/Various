using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DictionaryApp
{
    class DictionaryOptions
    {
        private string word;
        private string translation;
        private string newWord;
        private Dictionary<string, string> dictionary;
        public DictionaryOptions(Dictionary<string, string> dictionary)
        {
            this.dictionary = dictionary;
        }
        public string Search()
        {
            Console.Clear();
            ConsoleKey keyPressed;
            string value = "";
            while (true)
            {
                Console.Clear();
                int index = 3;
                Console.SetCursorPosition(4, 2);
                Console.WriteLine("Возможно вы ищете?");
                foreach (var item in dictionary.OrderBy(x => x.Key))
                {
                    if (item.Key.IndexOf(value) == 0)
                    {
                        Console.SetCursorPosition(4, index++);
                        Console.WriteLine(item.Key);
                    }
                }
                Console.SetCursorPosition(0, 0);
                Console.Write("    ПОИСК: " + value);
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyPressed == ConsoleKey.Backspace)
                {
                    if (value.Length == 0)
                        continue;
                    else
                        value = value.Remove(value.Length - 1);
                }
                else
                {
                    value += keyInfo.KeyChar;
                }
            }
            Console.Clear();

            string result = "";
            if (dictionary.TryGetValue(value, out string translate))
                result = $"    Слово: {value}\n    Перевод: {translate}";
            else
                result = "    Перевод не найден";

            return result;
        }
        public string AddWord()
        {
            Console.Clear();
            Console.Write("Введите слово: ");
            word = Console.ReadLine();
            if (dictionary.TryGetValue(word, out string translate))
                return null;
            Console.Write("Введите перевод: ");
            translation = Console.ReadLine();
            return $"    Слово: {word}\n    Перевод: {translation}";
        }
        public void AddWord(int index)
        {
            dictionary.Add(word, translation);
            List<string> temp = new List<string>();
            using (var sr = new StreamReader($"words{index}.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp.Add(word);
            File.WriteAllText($"words{index}.txt", string.Join("\n", temp));
            temp.Clear();

            using (var sr = new StreamReader($"translation{index}.txt"))
            {
                while (!sr.EndOfStream)
                {
                    temp.Add(sr.ReadLine());
                }
            }
            temp.Add(translation);
            File.WriteAllText($"translation{index}.txt", string.Join("\n", temp));
        }
        public string DeleteWord()
        {
            Console.Clear();
            Console.Write("Введите слово: ");
            word = Console.ReadLine();
            if (!dictionary.TryGetValue(word, out string translate))
                return null;
            return $"    Слово: {word}";
        }
        public void DeleteWord(int index)
        {
            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines($"words{index}.txt").Where(l => l != word);
            File.WriteAllLines(tempFile, linesToKeep);
            File.Delete($"words{index}.txt");
            File.Move(tempFile, $"words{index}.txt");
            dictionary.Remove(word);
        }
        public void DeleteTranslation(int index)
        {
            var tempFile = Path.GetTempFileName();
            var linesToKeep = File.ReadLines($"translation{index}.txt").Where(l => l != dictionary[word]);
            File.WriteAllLines(tempFile, linesToKeep);
            File.Delete($"translation{index}.txt");
            File.Move(tempFile, $"translation{index}.txt");
        }
        public string AddTranslation()
        {
            Console.Clear();
            Console.Write("Введите слово: ");
            word = Console.ReadLine();
            if (!dictionary.TryGetValue(word, out string translate))
                return null;
            Console.Clear();
            Console.WriteLine($"Слово: {word}");
            Console.WriteLine($"Перевод: {dictionary[word]}");
            Console.Write("Введите новый вариант перевода: ");
            translation = Console.ReadLine();
            return $"    Слово: {word}\n    Перевод: {dictionary[word]}\n    Новый вариант перевода: {translation}";
        }
        public void AddTranslation(int index)
        {
            dictionary[word] = dictionary[word] + "; " + translation;
            File.WriteAllText($"translation{index}.txt", string.Join("\n", dictionary.Values));
        }
        public string RenameWord()
        {
            Console.Clear();
            Console.Write("Введите слово: ");
            word = Console.ReadLine();
            if (!dictionary.TryGetValue(word, out string translate))
                return null;
            Console.Write("Введите новый вариант слова: ");
            newWord = Console.ReadLine();
            return $"    Слово: {word}\n    Перевод: {dictionary[word]}\n    Новый вариант слова: {newWord}";
        }
        public void RenameWord(int index)
        {
            string value = dictionary[word];
            dictionary.Remove(word);
            dictionary[newWord] = value;
            File.WriteAllText($"words{index}.txt", string.Join("\n", dictionary.Keys));
        }
        public List<string> RenameTranslation()
        {
            Console.Clear();
            Console.Write("Введите слово: ");
            word = Console.ReadLine();
            if (!dictionary.TryGetValue(word, out string translate))
                return null;
            Console.Clear();
            Console.WriteLine($"Слово: {word}");
            Console.WriteLine($"Перевод: {dictionary[word]}");
            List<string> temp = new List<string>();
            temp = dictionary[word].Split(';').ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i][0] == ' ')
                    temp[i] = temp[i].Substring(1);
            }
            Menu translations = new Menu("    ПЕРЕВОДЫ", temp);
            int selectedIndex = translations.Run();
            Console.Write("\nВведите новый перевод: ");
            temp[selectedIndex] = Console.ReadLine();
            return temp;
        }
        public void EditFile(int index, List<string> temp)
        {
            for (int i = 1; i < temp.Count; i++)
            {
                temp[i] = temp[i].Insert(0, " ");
            }
            dictionary[word] = string.Join(";", temp);
            File.WriteAllText($"translation{index}.txt", string.Join("\n", dictionary.Values));
        }
        public List<string> DeleteTranslation()
        {
            Console.Clear();
            Console.Write("Введите слово: ");
            word = Console.ReadLine();
            if (!dictionary.TryGetValue(word, out string translate))
                return null;
            Console.Clear();
            Console.WriteLine($"Слово: {word}");
            Console.WriteLine($"Перевод: {dictionary[word]}");
            List<string> temp = new List<string>();
            temp = dictionary[word].Split(';').ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i][0] == ' ')
                    temp[i] = temp[i].Substring(1);
            }
            Menu translations = new Menu("    ПЕРЕВОДЫ", temp);
            int selectedIndex = translations.Run();
            if (temp.Count < 2)
                return null;
            temp.RemoveAt(selectedIndex);
            return temp;
        }
    }
}
