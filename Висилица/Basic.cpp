//Задание
//Создайте консольную версию игры «Виселица».
//Важно :
//■ Соперник пишет слова в файл.
//■ Слово находится в файле.
//■ По завершенииигрына экран выводится статистика игры :
//• количество времени;
//• количество попыток;
//• искомое слово;
//• буквы игрока.
#include"Gallows1.h"
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iterator>
#include <map>
#include <Windows.h>
#include <time.h>
# include<Mmsystem.h> //для воиспровидения аудиофайлов
#pragma comment(lib, "Winmm.lib")

using namespace std;

int main()
{
    system("chcp 1251");
    system("cls");
    srand(time(0));
    game2();
    return 0;
}


