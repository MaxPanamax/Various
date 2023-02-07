#pragma once
#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <iterator>
#include <map>
#include <Windows.h>
#include <time.h>
# include<Mmsystem.h> //��� �������������� �����������
#pragma comment(lib, "Winmm.lib")
using namespace std;
class Gallows1
{
    ifstream m_input;
    const string m_word;
    map<char, vector<size_t>> m_posMap;//������ ����� � �����
    string m_searchWord;

    static const vector<string> m_images;
    //���������� ������������
    enum steps : size_t
    {
        BEGIN,
        ONE, TWO, THREE, FOR, FIVE, SIX, SEVEN,
        END
    }
    m_step = BEGIN;
    //���������� ������������
    enum result : size_t
    {
        LOSE, WIN
    }
    m_res = LOSE;
    void FillMap();
    char getChar();
    //�������� ++
    friend steps& operator++(steps&);
public:
    //���������� � ����������� ��� ������ �� ����� �����
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

//��������� ��������� �������
void SetColor(int text, int background);
bool win(string str);// ���� "*" � �����
void game(string str);
int game2();
