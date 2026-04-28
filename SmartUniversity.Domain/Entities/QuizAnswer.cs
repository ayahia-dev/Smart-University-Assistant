using System;
using System.Collections.Generic;
using System.Text;

namespace SmartUniversity.Domain.Entities
{
    public class QuizAnswer
    {
        public int Id { get; private set; }

        public int SubmissionId { get; private set; }

        public int QuestionId { get; private set; }

        public int SelectedOptionId { get; private set; }

        public QuizSubmission Submission { get; private set; }

        private QuizAnswer() { }

        public QuizAnswer(int submissionId, int questionId, int selectedOptionId)
        {
            SubmissionId = submissionId;
            QuestionId = questionId;
            SelectedOptionId = selectedOptionId;
        }
    }
}
