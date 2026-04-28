using SmartUniversity.Domain.Entities;

public class QuizSubmission
{
    public int Id { get; private set; }

    public int QuizId { get; private set; }
    public Quiz Quiz { get; private set; }

    public int StudentId { get; private set; }
    public User Student { get; private set; }

    public double Score { get; private set; }

    public ICollection<QuizAnswer> Answers { get; private set; }
        = new List<QuizAnswer>();

    public DateTime SubmittedAt { get; private set; }

    public DateTime? StartTime { get; private set; } // 🔥 nullable

    private QuizSubmission() { }

    public QuizSubmission(int quizId, int studentId, double score)
    {
        QuizId = quizId;
        StudentId = studentId;
        Score = score;

        SubmittedAt = DateTime.UtcNow;
        StartTime = null; // ❌ لسه مبدأش
    }

    public void Start()
    {
        if (StartTime != null)
            throw new Exception("Quiz already started");

        StartTime = DateTime.UtcNow; 
    }

    public void SetScore(double score)
    {
        Score = score;
    }
}