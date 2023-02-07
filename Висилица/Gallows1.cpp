#include "Gallows1.h"

//������� ������
Gallows1::steps& operator++(Gallows1::steps& st)
{
    size_t tmp = st;
    return st = static_cast<Gallows1::steps>(++tmp);
}

const vector<string> Gallows1::m_images
{
    {   },
    {
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    },
    {
        "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    },
    {
        "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    },
    {
        "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        O             $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    },
    {
        "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        O             $\n"
        "\t\t\t\t\t      /   \\           $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    },
    {
        "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        O             $\n"
        "\t\t\t\t\t      / | \\           $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    },
    {
        "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t        O             $\n"
        "\t\t\t\t\t      / | \\           $\n"
        "\t\t\t\t\t        |             $\n"
        "\t\t\t\t\t       / \\            $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t                      $\n"
        "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
        "\t\t\t\t\t $                          $\n"
    }
};

void Gallows1::FillMap()
{
    size_t pos{};
    for (char ch : m_word)
        m_posMap[ch].emplace_back(pos++);
}

char Gallows1::getChar()
{
    cout << "\t\t\t\t\t������� ���� �����: ";
    char ch{}; cin >> ch;

    return m_posMap.find(ch) != m_posMap.cend() ? ch : '\0';
}


void Gallows1::Play()
{
    clock_t start, end;

    start = clock();
    while (m_step != END)
    {
        system("cls");
        cout << "\t\t\t\t\t���������� �����: " << m_searchWord << '\n' << endl;
        cout << m_images[m_step] << endl;

        if (char ch{ getChar() })
        {
            const vector<size_t>& vec{ m_posMap[ch] };
            for (size_t i{}, end{ vec.size() }; i < end; ++i)
                m_searchWord[vec[i]] = ch;

            m_posMap.erase(ch);
            if (m_searchWord.find('*') == string::npos)
            {
                m_step = END, m_res = WIN;
            }
            PlaySound(TEXT("Correct.wav"), NULL, SND_FILENAME | SND_ASYNC);
        }
        else
            ++m_step;
    }
    system("cls");
    SetColor(2, 0);
    cout << "\n\t\t\t\t\t\t\t���������� �����: " << m_word << endl;
    cout << "\t\t\t\t\t\t\t�� " << (m_res == WIN ? "***�������***\n" : "***��������***\n") << endl;
    if (m_res == WIN)
    {
        PlaySound(TEXT("Prize.wav"), NULL, SND_FILENAME | SND_ASYNC);
        SetColor(4, 0);
        cout << "\t\t\t\t\t   *   *   *   *   *   *   *   *   *   *   *\n"
            "\t\t\t\t\t    *   *   *   *   *  *  *   *   *   *   *\n"
            "\t\t\t\t\t     *   *   *   *   * * *   *   *   *   *\n"
            "\t\t\t\t\t      *   *   *   *   ***   *   *   *   *\n"
            "\t\t\t\t\t       *   *   *   *  ***  *   *   *   *\n"
            "\t\t\t\t\t        *   *   *   * *** *   *   *   *\n"
            "\t\t\t\t\t         *   *   *   *****   *   *   *\n"
            "\t\t\t\t\t          *   *   *  *****  *   *   *\n"
            "\t\t\t\t\t           *   *   * ***** *   *   *\n"
            "\t\t\t\t\t            *   *   *******   *   *\n"
            "\t\t\t\t\t             *   *  *******  *   *\n"
            "\t\t\t\t\t              *   * ******* *   *\n"
            "\t\t\t\t\t               *   *********   *\n"
            "\t\t\t\t\t                *  *********  *\n"
            "\t\t\t\t\t                 * ********* *\n"
            "\t\t\t\t\t                  ***********\n" << endl;
    }
    else
    {
        PlaySound(TEXT("GameOver.wav"), NULL, SND_FILENAME | SND_ASYNC);
        SetColor(4, 0);
        cout <<
            "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
            "\t\t\t\t\t        |             $\n"
            "\t\t\t\t\t        |             $\n"
            "\t\t\t\t\t        |             $\n"
            "\t\t\t\t\t        |             $\n"
            "\t\t\t\t\t        O             $\n"
            "\t\t\t\t\t      / | \\           $\n"
            "\t\t\t\t\t        |             $\n"
            "\t\t\t\t\t       / \\            $\n"
            "\t\t\t\t\t                      $\n"
            "\t\t\t\t\t                      $\n"
            "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
            "\t\t\t\t\t $                          $\n"
            << endl;
    }

    end = clock();
    SetColor(2, 0);
    cout << "\t\t\t\t\t\t\t***���� �����***:  " << ((double)end - start) / ((double)CLOCKS_PER_SEC) << endl;
    system("pause");
}
//���������� �������
bool win(string str)// ���� "*" � �����
{
    bool a = 1;
    for (int i = 0; i < str.length(); i++)
    {
        if (str[i] == '*')
        {
            a = 0;
            break;
        }
    }
    return a;
}

void game(string str)
{
    clock_t start, end;
    start = clock();
    int counter_try = 0;// ��� �������� ���������� �������
    string mask;// ����� ��������� "*", ������� �������� �� ����� � ���� ����
    vector<char> player_letter;// ������ ����� ��������� �������
    system("cls");
    for (int i = 0; i < str.length(); i++)
    {
        mask.push_back('*');
    }
    while (counter_try < 8)
    {
        cout << "\t\t\t\t\t���� �����:" << endl;
        cout << "\t\t\t\t\t" << mask << endl;
        cout << "\t\t\t\t\t������� �����" << endl;
        cout << "\t\t\t\t\t�������: " << 8 - counter_try << endl;
        char letter;// ����� �������� �������
        cin >> letter;
        player_letter.push_back(letter);
        for (int i = 0; i < str.length(); i++)
        {
            if (str[i] == letter)
            {
                mask[i] = str[i];
                PlaySound(TEXT("Correct.wav"), NULL, SND_FILENAME | SND_ASYNC);
            }
        }
        counter_try++;
        system("cls");
        if (win(mask))
        {
            SetColor(4, 0);
            end = clock();
            cout << "\t\t\t\t\t   *   *   *   *   *   *   *   *   *   *   *\n"
                "\t\t\t\t\t    *   *   *   *   *  *  *   *   *   *   *\n"
                "\t\t\t\t\t     *   *   *   *   * * *   *   *   *   *\n"
                "\t\t\t\t\t      *   *   *   *   ***   *   *   *   *\n"
                "\t\t\t\t\t       *   *   *   *  ***  *   *   *   *\n"
                "\t\t\t\t\t        *   *   *   * *** *   *   *   *\n"
                "\t\t\t\t\t         *   *   *   *****   *   *   *\n"
                "\t\t\t\t\t          *   *   *  *****  *   *   *\n"
                "\t\t\t\t\t           *   *   * ***** *   *   *\n"
                "\t\t\t\t\t            *   *   *******   *   *\n"
                "\t\t\t\t\t             *   *  *******  *   *\n"
                "\t\t\t\t\t              *   * ******* *   *\n"
                "\t\t\t\t\t               *   *********   *\n"
                "\t\t\t\t\t                *  *********  *\n"
                "\t\t\t\t\t                 * ********* *\n"
                "\t\t\t\t\t                  ***********\n" << endl;
            SetColor(2, 0);
            cout << "\t\t\t\t\t�� ��������!" << endl;
            PlaySound(TEXT("Prize.wav"), NULL, SND_FILENAME | SND_ASYNC);
            cout << "\t\t\t\t\t�����: " << str << endl;
            cout << "\t\t\t\t\t�������: " << counter_try << endl;
            cout << "\t\t\t\t\t���� �����: " << ((double)end - start) / ((double)CLOCKS_PER_SEC) << " ������" << endl;
            cout << "\t\t\t\t\t����� ������: " << endl;
            cout << "\t\t\t\t\t";
            for (char a : player_letter)
            {
                cout << a << " ";
            }
            cout << endl;
            system("pause");
            break;
        }
        if (counter_try == 8 && !(win(mask)))
        {
            SetColor(4, 0);
            end = clock();
            cout <<
                "\t\t\t\t\t        $$$$$$$$$$$$$$$\n"
                "\t\t\t\t\t        |             $\n"
                "\t\t\t\t\t        |             $\n"
                "\t\t\t\t\t        |             $\n"
                "\t\t\t\t\t        |             $\n"
                "\t\t\t\t\t        O             $\n"
                "\t\t\t\t\t      / | \\           $\n"
                "\t\t\t\t\t        |             $\n"
                "\t\t\t\t\t       / \\            $\n"
                "\t\t\t\t\t                      $\n"
                "\t\t\t\t\t                      $\n"
                "\t\t\t\t\t $$$$$$$$$$$$$$$$$$$$$$$$$$$$\n"
                "\t\t\t\t\t $                          $\n"
                << endl;
            SetColor(2, 0);
            cout << "\t\t\t\t\t��� ��������!" << endl;
            PlaySound(TEXT("GameOver.wav"), NULL, SND_FILENAME | SND_ASYNC);
            cout << "\t\t\t\t\t�����: " << str << endl;
            cout << "\t\t\t\t\t�������: " << counter_try << endl;
            cout << "\t\t\t\t\t���� �����: " << ((double)end - start) / ((double)CLOCKS_PER_SEC) << " ������" << endl;
            cout << "\t\t\t\t\t����� ������: " << endl;
            cout << "\t\t\t\t\t";
            for (char a : player_letter)
            {
                cout << a << " ";
            }
            cout << endl;
            system("pause");
        }
    }
}
int game2()
{
    string input;
    int input1;
    int input2;
    int input3;
    bool retry = 1;// ��� ������ ���������� ���� ��� ���
    ofstream m_input1;
    while (retry)
    {
    menu2:
        m_input1.open("word.txt");
        if (!m_input1.is_open())
        {
            cout << "���� ����" << endl;
        }
        else
        {
        menu1:
            PlaySound(TEXT("menu.wav"), NULL, SND_FILENAME | SND_ASYNC);
            SetColor(6, 0);
            cout << "\t\t\t\t\t**********���� ��������**********" << endl << endl;
            cout << "\t\t���������� �������� ���������� ����� ����������, ����������� 8 ������������ �������." << endl << endl;
            cout << "\t\t\t\t\t  ���� �� �� ��������� ��� ����  " << endl << endl;
            cout <<
                "\t\t\t\t\t               $$$$$$$$$$$$$$$$  \n"
                "\t\t\t\t\t               |              $  \n"
                "\t\t\t\t\t               |              $  \n"
                "\t\t\t\t\t               |              $  \n"
                "\t\t\t\t\t               |              $  \n"
                "\t\t\t\t\t               O              $  \n"
                "\t\t\t\t\t             / | \\            $  \n"
                "\t\t\t\t\t               |              $  \n"
                "\t\t\t\t\t              / \\             $  \n"
                "\t\t\t\t\t                              $  \n"
                "\t\t\t\t\t                              $  \n"
                "\t\t\t\t\t  $$$$$$$$$$$$$$$$$$$$$$$$$$$$$  \n"
                "\t\t\t\t\t  $                           $  \n"
                << endl;
            cout << "\t\t\t\t*************************�����*************************" << endl << endl << endl;
            cout << "\t\t\t��������� � ��� �� ������� ������ � ����������� ��� � ����������???" << endl;
            cout << "\t\t\t\t���� � ����������� �� ������� <1>, ���� � ������� �� <2>" << endl << endl;
            cin >> input1;
            system("cls");
            if (input1 == 1)
            {
                SetColor(6, 0);
                cout << fixed;
                cout.precision(1);
                ifstream myFile("gallows1.txt");
                string str;// �������� ������: ����� ��� �������� �����, � ��� �� ��������� ��������� ����� �� �������, ���� ���� ���� � ���  
                vector<string> words7;// ������� ��� �������� ���� �� 7 ���� � �.�.
                vector<string> words6;
                vector<string> words5;
                vector<string> words4;
                vector<string> words3;
                int index = 0;// ��� ���������� ������ ����� �� �������

                int number_of_letters;// ��� ����� ����� ����� �������
                cout << "\t\t\t\t\t******�� ������� � �����������******" << endl;
                while (!myFile.eof())
                {
                    str = " ";
                    getline(myFile, str);
                    if (str.length() == 7) words7.push_back(str);
                    else if (str.length() == 6) words6.push_back(str);
                    else if (str.length() == 5) words5.push_back(str);
                    else if (str.length() == 4) words4.push_back(str);
                    else if (str.length() == 3) words3.push_back(str);
                }
                while (retry)
                {
                    cout << "\t\t\t\t\t������� ���������� ���� �� 3 �� 7 ";
                    cin >> number_of_letters;
                    PlaySound(TEXT("start.wav"), NULL, SND_FILENAME | SND_ASYNC);
                    switch (number_of_letters)
                    {
                    case 7:
                    {
                        index = rand() % words7.size();
                        str = words7[index];
                        game(str);
                        break;
                    }
                    case 6:
                    {
                        index = rand() % words6.size();
                        cout << str << endl;
                        game(str);
                        break;
                    }
                    case 5:
                    {
                        index = rand() % words5.size();
                        str = words5[index];
                        game(str);
                        break;
                    }
                    case 4:
                    {
                        index = rand() % words4.size();
                        str = words4[index];
                        game(str);
                        break;
                    }
                    case 3:
                    {
                        index = rand() % words3.size();
                        str = words3[index];
                        game(str);
                        break;
                    }
                    }
                    system("cls");
                    SetColor(6, 0);
                    cout << "\t\t\t\t\t��� ����������� ���� ���� ������� 1 � ����, ��� ������ ����� � ����" << endl;
                    cin >> input2;
                    if (input2 == 1)
                    {
                        goto menu1;
                    }
                    else
                    {
                        return 0;
                    }
                    system("cls");
                }
                myFile.close();

            }
            SetColor(6, 0);
            cout << "\t\t\t\t\t*********************�� ������� � ������ �������*************************" << endl;
            cout << "\t\t\t\t\t*******************************************************" << endl << endl << endl;
            cout << "\t\t\t\t\t  ������ ����������� � ����� ��������� �������� �����." << endl;
            cout << "\t\t\t\t\t  ����� �� ��� ������ � ������ ���� ���� ��������." << endl;
            cout << "\t\t\t\t\t  ���������� ��������� ��� ���������� ���� � �����" << endl;
            system("pause");
            system("cls");
            cout << "\t\t\t\t\t������� ����� ��� ��������� � ������� ����:" << endl;
            cin >> input;
            m_input1 << input << endl;
        }
        m_input1.close();
        PlaySound(TEXT("start.wav"), NULL, SND_FILENAME | SND_ASYNC);
        Gallows1 game;
        game.Play();
        system("cls");
        SetColor(6, 0);
        cout << "\t\t\t\t\t��� ����������� ���� ���� ������� 1 � ����, ��� ������ ����� � ����" << endl;
        cin >> input3;
        if (input3 == 1)
        {
            goto menu2;
        }
        else
        {
            return 0;
        }
        system("cls");
    }
}

void SetColor(int text, int background)
{
    HANDLE hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
    SetConsoleTextAttribute(hStdOut, (WORD)((background << 4) | text));
}