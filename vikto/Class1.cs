using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vikto
{
    public class Answer
    {
        public string Text { get; set; }
        public bool IsCorect { get; set; }

        public Answer()
        {
        }

        public Answer(string text, bool isCorect)
        {
            Text = text;
            IsCorect = isCorect;
        }
    }
    public class Question
    {
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }

        public Question()
        {
        }

        public Question(string text, List<Answer> answers)
        {
            Text = text;
            Answers = answers;
        }
    }
    public enum QuizType
    {
        History,
        Physics,
        Mixed
    }

    public class Quiz
    {

        public QuizType Type { get; set; }
        public string Title { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz()
        {
        }

        public Quiz(QuizType type, string title, List<Question> questions)
        {
            Type = type;
            Title = title;
            Questions = questions;
        }
    }
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }

        public User()
        {
        }

        public User(string login, string password, DateTime birthday)
        {
            Login = login;
            Password = password;
            Birthday = birthday;
        }
    }

    public class Users : List<User>
    {
        public bool SignUp(string login, string password, string birthday)
        {
            if (CheckUserExists(login))
                return false;
            this.Add(new User(login, password, DateTime.Parse(birthday)));
            return true;
        }
        public bool SignIn(string login, string password)
        {
            User user = FindUser(login);
            if (!CheckUserExists(login))
                return false;
            return user.Password == password;
        }
        public void ChangeUserPassword(string login, string newPassword)
        {
            User user = FindUser(login);
            this.Add(new User(login, newPassword, user.Birthday));
            this.Remove(user);
        }

        public void ChangeUserBirthday(string login, string newBirthday)
        {
            User user = FindUser(login);
            this.Add(new User(login, user.Password, DateTime.Parse(newBirthday)));
            this.Remove(user);
        }
        public bool CheckPassword(string login, string password)
        {
            return FindUser(login).Password == password;
        }
        private bool CheckUserExists(string login)
        {
            return FindUser(login) != null;
        }
        public User FindUser(string login)
        {
            return this.FirstOrDefault(u => u.Login == login);
        }
    }
    public class Score
    {
        public string UserLogin { get; set; }
        public string QuizTitle { get; set; }
        public int RightAnswers { get; set; }
        public int Max { get; set; }
        public Score()
        {
        }
        public Score(string userLogin, Quiz quiz, int rightAnswers)
        {
            UserLogin = userLogin;
            QuizTitle = quiz.Title;
            RightAnswers = rightAnswers;
            Max = CountMax(quiz);
        }
        public int CountMax(Quiz quiz)
        {
            int max = 0;
            foreach (var question in quiz.Questions)
            {
                foreach (var answer in question.Answers)
                {
                    if (answer.IsCorect)
                        max++;
                }
            }
            return max;
        }

        public override string ToString()
        {
            return $"{RightAnswers} из {Max} ({RightAnswers * 100 / Max} %)";
        }
    }
    public class Scores : List<Score>
    {
        public Scores()
        {

        }

        public bool CheckScoreExists(string userLogin, string quizTitle)
        {
            return FindScore(userLogin, quizTitle) != null;
        }
        public Score FindScore(string userLogin, string quizTitle)
        {
            return this.FirstOrDefault(s => s.UserLogin == userLogin && s.QuizTitle == quizTitle);
        }
    }
}
