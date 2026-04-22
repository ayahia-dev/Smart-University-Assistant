using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class Question
    {
        public int Id { get; private set; }

        public int QuizId { get; private set; }
        public Quiz Quiz { get; private set; }

        public string Text { get; private set; }

        public double Grade { get; private set; }

        public ICollection<QuestionOption> Options { get; private set; }
            = new List<QuestionOption>();

        private Question() { }

        public Question(int quizId, string text, double grade)
        {
            if (grade <= 0)
                throw new ArgumentException("Invalid grade");

            QuizId = quizId;
            Text = text;
            Grade = grade;
        }

        public void AddOption(QuestionOption option)
        {
            Options.Add(option);
        }
    }
}
