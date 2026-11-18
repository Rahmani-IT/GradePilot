namespace GradePilot.Domain.Entities;

public sealed record GradedCourse(
    string CourseName,
    decimal Credits,
    decimal? Midterm,
    decimal? Endterm,
    decimal? FinalExam
)
{
    public decimal? Percentage
    {
        get
        {
            if (Midterm is null || Endterm is null || FinalExam is null)
                return null;

            return (Midterm.Value * 0.30m)
                 + (Endterm.Value * 0.30m)
                 + (FinalExam.Value * 0.40m);
        }
    }

    public bool IsCompleted =>
        Midterm.HasValue &&
        Endterm.HasValue &&
        FinalExam.HasValue;
}