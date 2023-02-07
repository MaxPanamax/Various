using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DictionaryApp
{
 
    class Program
    {
        static void Main(string[] args)
        {
            Wordbook wordbook = new Wordbook();
            wordbook.ReadListOfDictionaries();
            wordbook.MainMenu();


        }
    }
}
