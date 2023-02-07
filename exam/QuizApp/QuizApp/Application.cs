using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    class Application
    {
        private Authentication authentication;
        private Quiz quiz;
        private User user;
        private bool isAdmin;
        public Application()
        {
            authentication = new Authentication();
            quiz = new Quiz();
            user = new User(quiz.GetThemes(), authentication);
            isAdmin = false;
        }
        public void Run()
        {
            quiz.ReadThemes();
            isAdmin = authentication.AuthenticationMenu(quiz.NumberOfThemes);
            if (isAdmin)
            {
                QuizUtility utility = new QuizUtility(quiz.GetThemes());
                utility.Run();
            }
            while (!isAdmin)
            {
                user.Run(authentication.LoginKey, authentication.GetUserData(), quiz);
                if (quiz.IsMixed)
                {
                    user.UpdateResult(quiz.CountingCorrectAnswers());
                    quiz.Statistics(authentication.LoginKey, quiz.CountingCorrectAnswers(), authentication.GetUserData());
                }
                else
                {
                    user.UpdateResult(quiz.CountingCorrectAnswers(), quiz.ThemeNumber);
                    quiz.Statistics(quiz.CountingCorrectAnswers(), authentication.LoginKey, authentication.GetUserData());
                }
            }
        }
    }
}
