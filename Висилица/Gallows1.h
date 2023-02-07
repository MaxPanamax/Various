#pragma once
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
class Gallows1
{
    ifstream m_input;
    const string m_word;
    map<char, vector<size_t>> m_posMap;//поиска буквы в слове
    string m_searchWord;

    static const vector<string> m_images;
    //определяем перечисление
    enum steps : size_t
    {
        BEGIN,
        ONE, TWO, THREE, FOR, FIVE, SIX, SEVEN,
        END
    }
    m_step = BEGIN;
    //определяем перечисление
    enum result : size_t
    {
        LOSE, WIN
    }
    m_res = LOSE;
    void FillMap();
    char getChar();
    //оператор ++
    friend steps& operator++(steps&);
public:
    //коструктор с параметрами для выимки из файла слова
    Gallows1(const string& filename = "word.txt") :
        m_input(filename),
        m_word(
            istream_iterator<char>(m_input),
            istream_iterator<char>()
        ),
        m_searchWord(m_word.size(), '*') {
        FillMap();
    };
    void Play();
};

//прототипы глобалных фунуций
void SetColor(int text, int background);
bool win(string str);// ищет "*" в маске
void game(string str);
int game2();
