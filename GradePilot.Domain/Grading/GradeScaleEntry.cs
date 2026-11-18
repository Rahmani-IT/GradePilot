namespace GradePilot.Domain.Grading;

public sealed record GradeScaleEntry(
    string LetterGrade,
    decimal GradePoint,
    int MinimumPercentage,
    int MaximumPercentage,
    string Description
);